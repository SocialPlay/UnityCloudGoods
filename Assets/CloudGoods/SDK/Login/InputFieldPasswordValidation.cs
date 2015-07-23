using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

namespace CloudGoods.SDK.Login
{
    public class InputFieldPasswordValidation : InputFieldValidation
    {

        public int passwordRequiredLength = 6;
        public InputField requiredMatchUI = null;

        public static Action<string> OnPasswordLengthInvalid;
        public static Action<string> OnPasswordNotMatching;

        protected override bool Validate(string currentInput, bool isSecondcheck = false)
        {

            if (requiredMatchUI != null)
            {
                if (!isSecondcheck)
                {
                    foreach (InputFieldValidation validation in requiredMatchUI.GetComponentsInChildren<InputFieldValidation>())
                    {
                        if(validation.IsValidCheck(true) == false)
                        {
                            OnValidationFailed("Passwords Do Not Match");
                        }
                    }
                }
                if (requiredMatchUI.text != currentInput)
                {
                    return false;
                }
            }

            if (string.IsNullOrEmpty(currentInput))
            {
                OnValidationFailed("Password has invalid length");
                return false;
            }

            if (currentInput.Length < passwordRequiredLength)
            {
                OnValidationFailed("Password has invalid length");
                return false;
            }

            return true;
        }
    }
}