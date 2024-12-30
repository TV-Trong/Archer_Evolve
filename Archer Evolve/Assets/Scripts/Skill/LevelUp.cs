using UnityEngine;

public class LevelUp : MonoBehaviour
{
    private AttributeManager attributeManager;
    private PlayerBehaviour playerInstance;

    private void Awake()
    {
        attributeManager = FindObjectOfType<AttributeManager>();
        playerInstance = PlayerBehaviour.instance;
    }


}
