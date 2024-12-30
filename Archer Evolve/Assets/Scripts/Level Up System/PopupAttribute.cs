using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupAttribute : MonoBehaviour
{
    private AttributeManager attributeManager;
    public Button[] choseAttributeButtons;
    public TextMeshProUGUI[] attributeNames;
    public TextMeshProUGUI[] attributeDescriptions;

    private void Awake()
    {
        attributeManager = PlayerBehaviour.instance.GetComponentInChildren<AttributeManager>();
    }

    private void OnEnable()
    {
        for (int i = 0; i < choseAttributeButtons.Length; i++)
        {
            if (choseAttributeButtons[i].gameObject.activeInHierarchy)
            {
                int randomAttribute = Random.Range(0, attributeManager.attributes.Count);
                attributeNames[i].text = attributeManager.attributes[randomAttribute].name;
                attributeDescriptions[i].text = attributeManager.attributes[randomAttribute].description;
            }
        }
        
    }
}
