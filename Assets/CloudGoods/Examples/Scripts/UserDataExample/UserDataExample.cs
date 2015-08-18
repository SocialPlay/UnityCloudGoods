using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using CloudGoods.SDK.Models;
using CloudGoods.Services;
using System.Collections.Generic;

public class UserDataExample : MonoBehaviour {

    public Text LevelText;
    public Text ExperienceText;
    public Slider ExperienceSlider;

    public GameObject LevelDisplay;
    public GameObject ResetButton;
    public ItemSpawner spawner;

    int ExperienceValue = 20;
    int MaxExperienceValue = 1;
    int Level = 1;

    public const float LEVEL_FACTOR = 0.04f;

    void OnEnable()
    {
        ExampleSceneLogin.OnUserLogin += OnUserLogin;
    }

    void OnDisable()
    {
        ExampleSceneLogin.OnUserLogin -= OnUserLogin;
    }

    void OnUserLogin(CloudGoodsUser user)
    {
        LevelDisplay.SetActive(true);
        ResetButton.SetActive(true);

        CloudDataServices.UserDataAll(OnReceivedCloudData);
    }

    void OnReceivedCloudData(List<CloudData> data)
    {
        foreach(CloudData newData in data)
        {
            if (newData.Key == "Level")
                Level = int.Parse(newData.Value);

            if(newData.Key == "Experience")
                ExperienceValue = int.Parse(newData.Value);
        }

        MaxExperienceValue = GetMaxExperience(Level);

        UpdateVisuals();

        spawner.StartSpawning();
    }

    void Update()
    {
        if ( Input.GetMouseButtonDown(0)){
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

            if (Physics.Raycast (ray, out hit, 200.0f)){
                if(hit.transform.tag == "Target")
                {
                    hit.transform.GetComponent<Target>().Hit();
                }
            }
        }
    }

    public void ButtonAddExperience()
    {
        AddExperience(10);
    }

	public void AddExperience(int addExperienceValue)
    {
        ExperienceValue += addExperienceValue;
        
        if(ExperienceValue < MaxExperienceValue)
            UpdateVisuals();
        else
        {
            ExperienceValue -= MaxExperienceValue;
            AddLevel(1);
        }

        UserDataUpdateRequest request = new UserDataUpdateRequest("Level", Level.ToString());
        CloudDataServices.UserDataUpdate(request, OnUpdateUserData);

        UserDataUpdateRequest expRequest = new UserDataUpdateRequest("Experience", ExperienceValue.ToString());
        CloudDataServices.UserDataUpdate(expRequest, OnUpdateUserData);
    }

    void OnUpdateUserData(CloudData data)
    {
        Debug.Log("Successful Update data");
    }

    public void AddLevel(int addLevelValue)
    {
        Level += addLevelValue;
        MaxExperienceValue = GetMaxExperience(Level);

        UpdateVisuals();
    }

    int GetMaxExperience(int level)
    {
        return (int)(((float)level + 2.0f) / LEVEL_FACTOR);
    }

    void UpdateVisuals()
    {
        LevelText.text = "Level " + Level;
        ExperienceText.text = ExperienceValue + " / " + MaxExperienceValue;
        ExperienceSlider.value = (float)ExperienceValue / (float)MaxExperienceValue;
    }

    public void ResetUser()
    {
        UserDataUpdateRequest request = new UserDataUpdateRequest("Level", "1");
        CloudDataServices.UserDataUpdate(request, OnUpdateUserData);

        UserDataUpdateRequest expRequest = new UserDataUpdateRequest("Experience", "0");
        CloudDataServices.UserDataUpdate(expRequest, OnUpdateUserData);

        Level = 1;
        ExperienceValue = 0;
        MaxExperienceValue = GetMaxExperience(Level);

        UpdateVisuals();
    }
}
