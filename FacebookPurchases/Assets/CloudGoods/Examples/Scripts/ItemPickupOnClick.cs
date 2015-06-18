using UnityEngine;
using System.Collections;
using CloudGoods.SDK.Item;


public class ItemPickupOnClick : MonoBehaviour {

	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (hit.collider.gameObject.GetComponent<InsertToContainerOnClick>() != null)
                    hit.collider.gameObject.GetComponent<InsertToContainerOnClick>().PickUp();
            }
        }
	}
}
