using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemTag : MonoBehaviour {

    public Text itemTag;
    public ItemVoucherSystemInputValueChanger itemVoucherChanger;

    public enum TagType
    {
        And,
        Or,
        Not
    }

    TagType tagType;

    public void SetUpTag(string name, TagType newTagType, ItemVoucherSystemInputValueChanger changer)
    {
        itemTag.text = name;
        tagType = newTagType;
        itemVoucherChanger = changer;
    }

    public void RemoveTag()
    {
        if (tagType == TagType.And)
            itemVoucherChanger.RemoveAndTag(itemTag.text);
        else if(tagType == TagType.Or)
            itemVoucherChanger.RemoveOrTag(itemTag.text);
        else
            itemVoucherChanger.RemoveNotTag(itemTag.text);

        Destroy(gameObject);
    }
}
