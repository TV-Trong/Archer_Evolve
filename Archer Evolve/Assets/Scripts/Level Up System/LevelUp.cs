using UnityEngine;

public class LevelUp : MonoBehaviour
{
    private AttributeManager attributeManager;
    [SerializeField] private GameObject attributeCanvas;
    [SerializeField] private GameObject playerInfoCanvas;

    private void Awake()
    {
        attributeManager = FindObjectOfType<AttributeManager>();
    }

    public void OpenAttributeCanvas()
    {
        attributeCanvas.SetActive(true);
        playerInfoCanvas.SetActive(true);
        GameManager.instance.PauseGame();
    }
}
