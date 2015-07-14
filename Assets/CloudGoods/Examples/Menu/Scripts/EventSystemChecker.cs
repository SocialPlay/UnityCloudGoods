using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;


namespace CloudGoods.SDK.Example.Menu
{
    public class EventSystemChecker : MonoBehaviour
    {
        public GameObject eventSystem;

        // Use this for initialization
        IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();

            if (!FindObjectOfType<EventSystem>())
            {
                Instantiate(eventSystem);
            }
        }
    }
}
