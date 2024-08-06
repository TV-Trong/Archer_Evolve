using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float fadeDuration = 1f;
    private TMP_Text textMesh;
    private Color textColor;

    private void Awake()
    {
        textMesh = GetComponent<TMP_Text>();
        textColor = textMesh.color;
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.sortingLayerName = "UI";
        meshRenderer.sortingOrder = -1;
    }

    private void Update()
    {
        transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
        textColor.a -= Time.deltaTime / fadeDuration;
        textMesh.color = textColor;
        if (textColor.a <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Setup(float damageAmount)
    {
        textMesh.text = damageAmount.ToString();
        textColor.a = 1f;
    }

    public void SetDamageColor(Color newColor)
    {
        textColor = newColor;
    }
}
