using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoseAttributeButton : MonoBehaviour
{
    [SerializeField] private GameObject attributeCanvas;
    [SerializeField] private GameObject playerInfoCanvas;
    private Button choseAttributeButton;
    private void Awake()
    {
        choseAttributeButton = GetComponent<Button>();
    }
    private void OnEnable()
    {
        choseAttributeButton.onClick.AddListener(CloseAttributeCanvas);
    }
    private void OnDisable()
    {
        GameManager.instance.ResumeGame();
        choseAttributeButton.onClick.RemoveAllListeners();
    }
    private void CloseAttributeCanvas()
    {
        playerInfoCanvas.SetActive(false);
        attributeCanvas.SetActive(false);
    }
}
