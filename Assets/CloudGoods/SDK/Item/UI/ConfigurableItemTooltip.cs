using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CloudGoods.SDK.Models;


namespace CloudGoods.SDK.Item.UI
{
    public class ConfigurableItemTooltip : MonoBehaviour, ITooltipSetup
    {

        public List<DisaplyOption> displayOptions = new List<DisaplyOption>();
        public bool isQualityColorUsed = true;
        public bool ShowBlankLineForEmptySelection = false;

        public enum DisaplyOption
        {
            name, stats, quantity, description, tag, behaviour, behaviourWithDescription, space, varianceID, itemID, energy, classID, stackID
        }

        OwnedItemInformation item;


        public string Setup()
        {
            item = GetComponent<ItemDataComponent>().itemData;
            string formated = "";


            foreach (DisaplyOption selectedOption in displayOptions)
            {
                switch (selectedOption)
                {
                    case DisaplyOption.name:
                        if (isQualityColorUsed)
                        {
                            formated += item.Information.Name;//.ToRichColor(ItemQuailityColorSelector.GetColorForItem(item));
                        }
                        else
                        {
                            formated += item.Information.Name;
                        }
                        break;
                    case DisaplyOption.stats:
                        //foreach (KeyValuePair<string, string> pair in item.Information.)
                        //{
                        //    if (pair.Key == "Not Available") continue;

                        //    formated = string.Format("{0}\n{1}: {2}", formated, pair.Key, pair.Value.ToString()/*.ToRichColor(Color.yellow)*/);
                        //}
                        //break;
                    case DisaplyOption.quantity:
                        formated = string.Format("{0}\n{1}", formated, item.Amount);
                        break;
                    case DisaplyOption.description:
                        if (!string.IsNullOrEmpty(item.Information.Description))
                        {
                            formated = string.Format("{0}\n{1}", formated, item.Information.Description);
                        }
                        break;

                    case DisaplyOption.tag:

                        string tags = "";
                        foreach (ItemInformation.Tag tg in item.Information.Tags)
                        {
                            if (string.IsNullOrEmpty(tags))
                            {
                                tags = tg.Name;
                            }
                            else
                            {
                                tags = string.Format("{0}, {1}", tags, tg);
                            }
                        }
                        if (item.Information.Tags.Count == 0)
                        {
                            if (ShowBlankLineForEmptySelection)
                                formated = string.Format("{0}\n");
                        }
                        else
                        {
                            formated = string.Format("{0}\n{1}", formated, tags);
                        }
                        break;
                    case DisaplyOption.behaviour:

                        foreach (ItemInformation.Behaviour behaviour in item.Information.Behaviours)
                        {
                            formated = string.Format("{0}\n{1}", formated, behaviour.Name);
                        }

                        if (item.Information.Behaviours.Count == 0 && ShowBlankLineForEmptySelection)
                        {
                            formated = string.Format("{0}\n", formated);
                        }
                        break;
                    case DisaplyOption.behaviourWithDescription:
                        foreach (ItemInformation.Behaviour behaviour in item.Information.Behaviours)
                        {
                            formated = string.Format("{0}\n{1}: {2}", formated, behaviour.Name/*.ToRichColor(Color.white)*/, behaviour.Id/*.ToRichColor(Color.grey)*/);
                        }
                        if (item.Information.Behaviours.Count == 0 && ShowBlankLineForEmptySelection)
                        {
                            formated = string.Format("{0}\n", formated);
                        }
                        break;
                    case DisaplyOption.space:
                        formated = string.Format("{0}\n", formated);
                        break;
                    case DisaplyOption.varianceID:
                        formated = string.Format("{0}\n{1}", formated, item.Information.Id);
                        break;
                    case DisaplyOption.itemID:
                        formated = string.Format("{0}\n{1}", formated, item.Information.CollectionId);
                        break;
                    case DisaplyOption.classID:
                        formated = string.Format("{0}\n{1}", formated, item.Information.ClassId);
                        break;
                    case DisaplyOption.energy:
                        formated = string.Format("{0}\n{1}", formated, item.Information.Energy);
                        break;
                    case DisaplyOption.stackID:
                        formated = string.Format("{0}\n{1}", formated, item.StackLocationId.ToString());
                        break;

                    default: break;
                }
            }
            return formated;
        }
    }

}
