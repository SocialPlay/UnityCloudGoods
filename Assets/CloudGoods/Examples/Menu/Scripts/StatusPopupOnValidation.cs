using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using CloudGoods.SDK.Login;

public class StatusPopupOnValidation : MonoBehaviour {

    public GameObject StatusPanel;
    public Text StatusText;

	// Use this for initialization
	void OnEnable () {
        InputFieldValidation.OnValidationFailedEvent += OnValidationFailed;
	}

    void OnDisable()
    {
        InputFieldValidation.OnValidationFailedEvent -= OnValidationFailed;
    }
	
	// Update is called once per frame
	void OnValidationFailed (string errorMessage) {
        StatusText.text = errorMessage;
        StatusPanel.SetActive(true);

	}
}
