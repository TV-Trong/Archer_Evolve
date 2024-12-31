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
                LevelUpAttribute chosenAttribute = attributeManager.attributes[randomAttribute];
                attributeNames[i].text = chosenAttribute.name;
                attributeDescriptions[i].text = chosenAttribute.description;
                choseAttributeButtons[i].onClick.AddListener(() => ActivateAttribute(chosenAttribute.type.ToString(), chosenAttribute.value));
            }
        }
    }

    public void ActivateAttribute(string type, float value)
    {
        switch (type)
        {
            case "Strength":
                PlayerBehaviour.instance.strength += value;
                break;
            case "Speed":
                PlayerBehaviour.instance.moveSpeed += value;
                break;
            case "FireRate":
                PlayerBehaviour.instance.attackSpeed -= value;
                break;
            case "CriticalChance":
                PlayerBehaviour.instance.critChance += value;
                break;
            case "CriticalDamage":
                PlayerBehaviour.instance.critDamage += value;
                break;
            default:
                Debug.LogWarning("Attribute Type not found!");
                break;
        }
    }
}
