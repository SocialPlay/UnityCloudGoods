using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

namespace CloudGoods.SDK.Example.Menu
{
    public class SceneMenu : MonoBehaviour
    {

        [System.Serializable]
        public class MenuInfo
        {
            public string Name;
            public Object SceneObject;
            public string Description;
        }

        [System.Serializable]
        public class SceneInfoPanel{
           public GameObject Panel;
           public Text Title;
           public Text Body;
        }


        public List<MenuInfo> ActiveMenuInfos = new List<MenuInfo>();
        public MonoBehaviour MenuItemPrefab;
        public Transform Anchor;

        public SceneInfoPanel InfoPanel;

        void Start()
        {
            foreach (MenuInfo info in ActiveMenuInfos)
            {
                MonoBehaviour go = GameObject.Instantiate(MenuItemPrefab);
                go.transform.SetParent(Anchor);
                go.GetComponent<MenuItem>().Setup(info);

            }
            SetSceneInfo();
        }


       void OnLevelWasLoaded(int level) {
           SetSceneInfo();           
        }

       void SetSceneInfo()
       {
           MenuInfo current = ActiveMenuInfos.FirstOrDefault(m => m.SceneObject.name == Application.loadedLevelName);
           Debug.Log("currenct " + current.Name);
           if (current != null && !string.IsNullOrEmpty(current.Description))
           {
               InfoPanel.Title.text = current.Name;
               InfoPanel.Body.text = current.Description;
               InfoPanel.Panel.SetActive(true);
           }
           else
           {
               InfoPanel.Panel.SetActive(false);
           }
          

       }
    }
}
