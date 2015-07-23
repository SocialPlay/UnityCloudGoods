using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System;

namespace CloudGoods.SDK.Login
{
    public class InputFieldEmailValidation : InputFieldValidation
    {

        protected override bool Validate(string currentInput, bool isSecondcheck = false)
        {
            Debug.Log("Validate string for email: " + currentInput);

            if (string.IsNullOrEmpty(currentInput))
            {
                OnValidationFailed("You did not enter a valid email");
                return false;
            }
            bool isEmail = Regex.IsMatch(currentInput, @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$");
            //@"\A(?:[A-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[A-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[A-Z0-9](?:[A-Z0-9-]*[A-Z0-9])?\.)+[A-Z0-9](?:[A-Z0-9-]*[A-Z0-9])?)\Z");

            if (isEmail == false)
                OnValidationFailed("You did not enter a valid email");

            return isEmail;
        }
    }
}
