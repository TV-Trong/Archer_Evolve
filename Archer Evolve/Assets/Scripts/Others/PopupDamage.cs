using UnityEngine;
using TMPro;

public class PopupDamage : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float fadeDuration = 1f;
    private TMP_Text popupText;
    private Color textColor;
    private float baseFontSize;

    private void Awake()
    {
        popupText = GetComponent<TMP_Text>();
        textColor = popupText.color;
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.sortingLayerName = "UI";
        meshRenderer.sortingOrder = -1;
        baseFontSize = popupText.fontSize;
    }

    private void Update()
    {
        transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
        textColor.a -= Time.deltaTime / fadeDuration;
        popupText.color = textColor;
        if (textColor.a <= 0)
        {
            popupText.fontSize = baseFontSize;
            gameObject.SetActive(false);
        }
    }

    public void Setup(float damageAmount)
    {
        popupText.text = damageAmount.ToString();
        textColor.a = 1f;
    }

    public void SetDamageColor(Color newColor)
    {
        textColor = newColor;
    }
    public void SetCriticalDamage()
    {
        textColor = Color.yellow;
        popupText.fontSize += 1;
    }
}
