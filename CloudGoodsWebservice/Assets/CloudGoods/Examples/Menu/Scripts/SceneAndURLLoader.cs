using System;
using UnityEngine;


namespace CloudGoods.SDK.Example.Menu
{
    public class SceneAndURLLoader : MonoBehaviour
    {
        private PauseMenu m_PauseMenu;

        public static SceneAndURLLoader Instance { get { return _insatnce; } }
        private static SceneAndURLLoader _insatnce;




        private void Awake()
        {
            if (_insatnce == null)
                _insatnce = this;

            m_PauseMenu = GetComponentInChildren<PauseMenu>();
        }


        public void SceneLoad(string sceneName)
        {
            //PauseMenu pauseMenu = (PauseMenu)FindObjectOfType(typeof(PauseMenu));
            m_PauseMenu.MenuOff();
            Application.LoadLevel(sceneName);
        }


        public void LoadURL(string url)
        {
            Application.OpenURL(url);
        }
    }
}

