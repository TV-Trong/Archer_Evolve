using UnityEngine;

public class LevelUp : MonoBehaviour
{
    private AttributeManager attributeManager;
    [SerializeField] private GameObject attributeCanvas;

    private void Awake()
    {
        attributeManager = FindObjectOfType<AttributeManager>();
    }

    public void OpenAttributeCanvas()
    {
        attributeCanvas.SetActive(true);
        GameManager.instance.PauseGame();
    }
}
