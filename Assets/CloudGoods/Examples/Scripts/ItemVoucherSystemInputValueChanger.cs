using UnityEngine;
using System.Collections;
using CloudGoods.SDK.Item;
using UnityEngine.UI;
using System.Collections.Generic;

public class ItemVoucherSystemInputValueChanger : MonoBehaviour {

    public ItemVoucherSystem ItemGenerator;

    public GameObject tagPrefab;

    public GameObject andTagsGrid;
    public GameObject orTagsGrid;
    public GameObject notTagsGrid;

    public InputField andTagsValue;
    public InputField orTagsValue;
    public InputField notTagsValue;

    public void OnMinEnergyChanged(string minEnergy)
    {
        if (string.IsNullOrEmpty(minEnergy))
            return;

        ItemGenerator.minEnergy = int.Parse(minEnergy);
    }

    public void OnMaxEnergyChanged(string maxEnergy)
    {
        if (string.IsNullOrEmpty(maxEnergy))
            return;

        ItemGenerator.maxEnergy = int.Parse(maxEnergy);
    }

    public void OnTotalEnergyChanged(string totalEnergy)
    {
        if (string.IsNullOrEmpty(totalEnergy))
            return;

        ItemGenerator.TotalEnergyToGenerate = int.Parse(totalEnergy);
    }

    public void OnEnterAndTag()
    {
        AddTag(andTagsValue.text, ItemGenerator.AndTags, ItemTag.TagType.And, andTagsGrid);

    }

    public void OnEnterOrTag()
    {
        AddTag(orTagsValue.text, ItemGenerator.OrTags, ItemTag.TagType.Or, orTagsGrid);
    }

    public void OnEnterNotTag()
    {
        AddTag(notTagsValue.text, ItemGenerator.NotTags, ItemTag.TagType.Not, notTagsGrid);
    }

    private void AddTag(string addTag, List<string> ListTags, ItemTag.TagType tagType, GameObject addGridObject)
    {
        GenerateTag(addTag, tagType, addGridObject);
        ListTags.Add(addTag);
    }

    public void RemoveAndTag(string removeTag)
    {
        RemoveTag(removeTag, ItemGenerator.AndTags);
    }

    public void RemoveOrTag(string removeTag)
    {
        RemoveTag(removeTag, ItemGenerator.OrTags);
    }

    public void RemoveNotTag(string removeTag)
    {
        RemoveTag(removeTag, ItemGenerator.NotTags);
    }

    void RemoveTag(string tag, List<string> TagList)
    {
        TagList.Remove(tag);
    }

    
    void GenerateTag(string tagName, ItemTag.TagType tagType, GameObject gridObject)
    {
        GameObject newTagObj = GameObject.Instantiate(tagPrefab);
        ItemTag itemTag = newTagObj.GetComponent<ItemTag>();
        itemTag.SetUpTag(tagName, tagType, this);

        newTagObj.transform.parent = gridObject.transform;
    }

}
