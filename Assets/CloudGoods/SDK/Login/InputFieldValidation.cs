using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

namespace CloudGoods.SDK.Login
{
    public abstract class InputFieldValidation : MonoBehaviour
    {
        public static Action<string> OnValidationFailedEvent;

        InputField uiInput;

        void Start()
        {
            if (this.GetComponent<InputField>())
            {
                uiInput = this.GetComponent<InputField>();
            }
        }

        public bool IsValidCheck(bool isSecondcheck = false)
        {
            if (Validate(uiInput.text, isSecondcheck))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void OnValidationFailed(string message)
        {
            if (OnValidationFailedEvent != null)
                OnValidationFailedEvent(message);
        }


        protected abstract bool Validate(string currentInput, bool isSecondcheck = false);
    }
}
