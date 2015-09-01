using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetItemPickUpLocation : MonoBehaviour {

    public static int PickUpLocation;

    public InputField locationInput;

    public void SetPickUpLocation()
    {

        if (string.IsNullOrEmpty(locationInput.text))
            PickUpLocation = 0;
        else
            PickUpLocation = int.Parse(locationInput.text);

        Debug.Log("Pickup lcoation: " + PickUpLocation);
    }
}
