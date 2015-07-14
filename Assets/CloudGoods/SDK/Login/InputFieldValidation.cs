using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace CloudGoods.SDK.Login
{
    public abstract class InputFieldValidation : MonoBehaviour
    {
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



        protected abstract bool Validate(string currentInput, bool isSecondcheck = false);
    }
}
