using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using CloudGoods.SDK.Item;

namespace CloudGoods.Services
{
    [System.Serializable]
    public class CloudGoodsSettings : ScriptableObject
    {
        public enum ScreenType
        {
            Settings,
            About,
            _LastDoNotUse,
        }

        public enum BuildPlatformType
        {
            Automatic = 0,
            Facebook = 1,
            Kongergate = 2,
            Android = 3,
            IOS = 4,
            CloudGoodsStandAlone = 6,
            Unknown = 7,
            Editor = 8,
            Steam = 9
        }


        static public string VERSION = "1.0";

        static public string mainPath = "Assets/CloudGoods/Textures/";

        public string appID;
        public string appSecret;
        public string expSceneUserName;
        public string expScenePass;
        public ScreenType screen;
        public string url = "http://webservice.socialplay.com/cloudgoods/cloudgoodsservice.svc/";
        public string bundlesUrl = "https://socialplay.blob.core.windows.net/unityassetbundles/";
        public string androidKey = "";
        public Texture2D defaultTexture;
        public GameObject defaultItemDrop;
        public GameObject defaultUIItem;
        public BuildPlatformType platformType;
        private string domainURL = "";

        public List<ItemPrefabInitilizer.DropPrefab> itemInitializerPrefabs = new List<ItemPrefabInitilizer.DropPrefab>();

        static CloudGoodsSettings mInst;

        static public Action<BuildPlatformType> OnBuildPlatformFound;

        static public CloudGoodsSettings instance
        {
            get
            {
                if (mInst == null) mInst = (CloudGoodsSettings)Resources.Load("CloudGoodsSettings", typeof(CloudGoodsSettings));
                return mInst;
            }
        }

        static public GameObject DefaultItemDrop
        {
            get
            {
                return instance.defaultItemDrop;
            }
        }

        static public GameObject DefaultUIItem
        {
            get
            {
                return instance.defaultUIItem;
            }
        }

        static public string AppSecret
        {
            get
            {
                return instance.appSecret.Trim();
            }
        }

        static public string AppID
        {
            get
            {
                if (string.IsNullOrEmpty(instance.appID))
                {
                    Debug.Log("Cloud Goods App Id is not set correctly");
                }

                return instance.appID.Trim();
            }
        }

        static public string Url
        {
            get
            {
                return instance.url.Trim();
            }
        }

        static public string ExpSceneUserName
        {
            get{
                if (!string.IsNullOrEmpty(instance.expSceneUserName))
                    return instance.expSceneUserName;
                else
                    throw new Exception("User Name must be set in the Cloud Goods Settings in order to use the example scenes");
            }
        }

        static public string ExpScenePassword
        {
            get
            {
                if (!string.IsNullOrEmpty(instance.expScenePass))
                    return instance.expScenePass;
                else
                    throw new Exception("Password must be set in the Cloud Goods Settings in order to use the example scenes");
            }
        }

        static public Texture2D DefaultTexture
        {
            get
            {
                return instance.defaultTexture;
            }
        }

        static public string BundlesUrl
        {
            get
            {
                return instance.bundlesUrl;
            }
        }

        static public string AndroidKey
        {
            get
            {
                return instance.androidKey.Trim();
            }
        }
        static public List<ItemPrefabInitilizer.DropPrefab> ExtraItemPrefabs
        {
            get
            {
                return instance.itemInitializerPrefabs;
            }
        }

        static public BuildPlatformType PlatformType
        {
            get
            {
                if (instance.platformType == BuildPlatformType.Automatic)
                {
#if UNITY_WEBPLAYER
                return BuildPlatformType.Facebook;
#endif
#if UNITY_IPHONE
                return BuildPlatformType.IOS;
#elif UNITY_ANDROID
                return BuildPlatformType.Android;
#endif
#if UNITY_EDITOR
                return  BuildPlatformType.Editor;
#endif
                }
                else
                {
                     return instance.platformType;
                }
            }
        }


    }
}
