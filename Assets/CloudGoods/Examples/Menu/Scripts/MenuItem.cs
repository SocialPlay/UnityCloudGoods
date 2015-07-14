using UnityEngine;
using System.Collections;
using UnityEngine.UI;


namespace CloudGoods.SDK.Example.Menu
{
    public class MenuItem : MonoBehaviour
    {

        public Text buttonText;
        public Button button;

        public void Setup(SceneMenu.MenuInfo info)
        {
            buttonText.text = info.Name;
            if (SceneAndURLLoader.Instance != null)
                button.onClick.AddListener(() => { SceneAndURLLoader.Instance.SceneLoad(info.SceneObject.name); });

        }


    }
}
