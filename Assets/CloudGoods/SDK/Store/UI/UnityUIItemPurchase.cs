using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using CloudGoods.SDK.Container;
using CloudGoods.Enums;
using CloudGoods.SDK.Models;
using CloudGoods.Services;
using System.Collections.Generic;

namespace CloudGoods.SDK.Store.UI
{
    public class UnityUIItemPurchase : MonoBehaviour
    {
        public static event Action<SimpleItemInfo> OnPurchasedItem;

        UnityUIStoreItem itemInfo;

        public Text itemNameDisplay;
        public Text itemDetailsDisplay;

        public Text itemQuantityAmount;

        public GameObject increaseQuantityButton;
        public GameObject decreaseQuantityButton;

        public GameObject PremiumCurrencyHalfWindow;
        public GameObject StandardCurrencyHalfWindow;
        public GameObject PremiumCurrencyFullWindow;
        public GameObject StandardCurrencyFullWindow;

        public RawImage itemTexture;

        public List<int> StandardCurrencyAccessLocations = new List<int>();
        public int purchaseLocation = 0;

        int premiumCurrencyCost = 0;
        int standardCurrencyCost = 0;

        public void IncreaseQuantityAmount()
        {
            int quantityAmount = int.Parse(itemQuantityAmount.text);

            quantityAmount++;

            ChangeAmountDisplay(quantityAmount);
        }

        public void DecreaseQuantityAmount()
        {
            int quantityAmount = int.Parse(itemQuantityAmount.text);

            if (quantityAmount > 1)
                quantityAmount--;

            ChangeAmountDisplay(quantityAmount);
        }

        private void ChangeAmountDisplay(int quantityAmount)
        {
            int tmpPremiumCost = premiumCurrencyCost;
            int tmpStandardCost = standardCurrencyCost;

            if (tmpPremiumCost >= 0)
                tmpPremiumCost = tmpPremiumCost * quantityAmount;
            else
                tmpPremiumCost = -1;

            if (tmpStandardCost >= 0)
                tmpStandardCost = tmpStandardCost * quantityAmount;
            else
                tmpStandardCost = -1;

            itemQuantityAmount.text = quantityAmount.ToString();

            ChangePurchaseButtonDisplay(tmpPremiumCost, tmpStandardCost);
        }

        private void ChangePurchaseButtonDisplay(int itemCreditCost, int itemCoinCost)
        {
            if (itemCreditCost > 0 && itemCoinCost > 0)
            {
                StandardCurrencyHalfWindow.SetActive(true);
                PremiumCurrencyHalfWindow.SetActive(true);
                StandardCurrencyFullWindow.SetActive(false);
                PremiumCurrencyFullWindow.SetActive(false);

                UnityUIPurchaseButtonDisplay freeButtonDisplay = StandardCurrencyHalfWindow.GetComponent<UnityUIPurchaseButtonDisplay>();
                freeButtonDisplay.SetState(itemCoinCost);
                UnityUIPurchaseButtonDisplay premiumButtonDisplay = PremiumCurrencyHalfWindow.GetComponent<UnityUIPurchaseButtonDisplay>();
                premiumButtonDisplay.SetState(itemCreditCost);
            }
            else if (itemCreditCost < 0 && itemCoinCost < 0)
            {
                StandardCurrencyFullWindow.SetActive(true);
                StandardCurrencyHalfWindow.SetActive(false);
                PremiumCurrencyFullWindow.SetActive(false);
                PremiumCurrencyHalfWindow.SetActive(false);

                UnityUIPurchaseButtonDisplay StandardOnlyButtonDisplay = StandardCurrencyFullWindow.GetComponent<UnityUIPurchaseButtonDisplay>();
                StandardOnlyButtonDisplay.SetState(0);
            }
            else if (itemCreditCost < 0 && itemCoinCost > 0)
            {
                StandardCurrencyFullWindow.SetActive(true);
                StandardCurrencyHalfWindow.SetActive(false);
                PremiumCurrencyFullWindow.SetActive(false);
                PremiumCurrencyHalfWindow.SetActive(false);

                UnityUIPurchaseButtonDisplay StandardOnlyButtonDisplay = StandardCurrencyFullWindow.GetComponent<UnityUIPurchaseButtonDisplay>();
                StandardOnlyButtonDisplay.SetState(itemCoinCost);
            }
            else if (itemCoinCost < 0 && itemCreditCost > 0)
            {
                PremiumCurrencyFullWindow.SetActive(true);
                StandardCurrencyFullWindow.SetActive(false);
                StandardCurrencyHalfWindow.SetActive(false);
                PremiumCurrencyHalfWindow.SetActive(false);

                UnityUIPurchaseButtonDisplay PremiumOnlyButtonDisplay = PremiumCurrencyFullWindow.GetComponent<UnityUIPurchaseButtonDisplay>();
                PremiumOnlyButtonDisplay.SetState(itemCreditCost);
            }
            else
            {
                PremiumCurrencyHalfWindow.SetActive(true);
                StandardCurrencyHalfWindow.SetActive(true);
                StandardCurrencyFullWindow.SetActive(false);
                PremiumCurrencyFullWindow.SetActive(false);

                UnityUIPurchaseButtonDisplay PremiumButtonDisplay = PremiumCurrencyHalfWindow.GetComponent<UnityUIPurchaseButtonDisplay>();
                UnityUIPurchaseButtonDisplay StandardButtonDisplay = StandardCurrencyHalfWindow.GetComponent<UnityUIPurchaseButtonDisplay>();
                PremiumButtonDisplay.SetState(itemCreditCost);
                StandardButtonDisplay.SetState(itemCoinCost);
            }
        }

        public void DisplayItemPurchasePanel(UnityUIStoreItem item)
        {
            itemInfo = item;
            itemNameDisplay.text = item.storeItem.ItemInformation.Name;

            Debug.Log("Sale: " + item.storeItem.Sale.Count);

            if (item.storeItem.Sale.Count > 0)
            {
                if (item.storeItem.Sale[0].PremiumCurrencySaleValue > 0)
                    premiumCurrencyCost = item.storeItem.Sale[0].PremiumCurrencySaleValue;
                else
                {
                    premiumCurrencyCost = item.storeItem.CreditValue;
                }

                if (item.storeItem.Sale[0].StandardCurrencySaleValue > 0)
                    standardCurrencyCost = item.storeItem.Sale[0].StandardCurrencySaleValue;
                else
                    standardCurrencyCost = item.storeItem.CoinValue;
            }
            else
            {
                premiumCurrencyCost = item.storeItem.CreditValue;
                standardCurrencyCost = item.storeItem.CoinValue;
            }

            itemQuantityAmount.text = "1";
            SetItemDetailDisplay(item);

            itemTexture.texture = item.gameObject.GetComponentInChildren<RawImage>().texture;

            ChangePurchaseButtonDisplay(premiumCurrencyCost, standardCurrencyCost);
        }

        void SetItemDetailDisplay(UnityUIStoreItem storeItem)
        {
            string statusText = "";

            foreach (StoreItem.StoreItemDetail detail in storeItem.storeItem.ItemDetails)
            {
                statusText += detail.Name + " : " + detail.Value + "\n";
            }

            itemDetailsDisplay.text = statusText;
        }

        public void PurchaseItemWithPremiumCurrency()
        {
            PurchaseItemRequest.PaymentType paymentType;
            if (premiumCurrencyCost > 0)
                paymentType = PurchaseItemRequest.PaymentType.Premium;
            else
                paymentType = PurchaseItemRequest.PaymentType.Free;

            ItemStoreServices.PurchaseItem(new PurchaseItemRequest(itemInfo.storeItem.ItemId, int.Parse(itemQuantityAmount.text), paymentType, purchaseLocation, StandardCurrencyAccessLocations), OnReceivedItemPurchaseConfirmation);
            ClosePanel();
        }

        public void PurchaseItemWithStandardCurrency()
        {
            PurchaseItemRequest.PaymentType paymentType;
            if (standardCurrencyCost > 0)
                paymentType = PurchaseItemRequest.PaymentType.Standard;
            else
                paymentType = PurchaseItemRequest.PaymentType.Free;


            ItemStoreServices.PurchaseItem(new PurchaseItemRequest(itemInfo.storeItem.ItemId, int.Parse(itemQuantityAmount.text), paymentType, purchaseLocation, StandardCurrencyAccessLocations), OnReceivedItemPurchaseConfirmation);
            ClosePanel();
        }

        void OnReceivedItemPurchaseConfirmation(SimpleItemInfo msg)
        {
            ReloadContainerItems();

            if (OnPurchasedItem != null)
                OnPurchasedItem(msg);
        }

        void ReloadContainerItems()
        {
            foreach (PersistentItemContainer loader in GameObject.FindObjectsOfType(typeof(PersistentItemContainer)))
            {
                if (loader.Location == 0)
                {
                    loader.transform.parent.GetComponent<ItemContainer>().Clear();
                    loader.LoadItems();
                }
            }
        }

        public void ClosePanel()
        {
            gameObject.SetActive(false);
        }
    }
}
