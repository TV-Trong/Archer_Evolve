using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Update : MonoBehaviour
{
    private Slider HP_Slider;
    private Slider EXP_Slider;
    private TextMeshProUGUI HP_Text;
    private TextMeshProUGUI EXP_Text;
    private TextMeshProUGUI level_Text;
    private float baseHPValue;
    private float baseEXPValue;
    private void Awake()
    {
        HP_Slider = GameObject.Find("HP Slider").GetComponent<Slider>();
        EXP_Slider = GameObject.Find("Exp Slider").GetComponent<Slider>();
        HP_Text = GameObject.Find("HP number").GetComponent<TextMeshProUGUI>();
        EXP_Text = GameObject.Find("Exp number").GetComponent<TextMeshProUGUI>();
        level_Text = GameObject.Find("Level Text").GetComponent<TextMeshProUGUI>();
    }
    public void InitialHUD(float baseHP, float HP, float baseEXP, float EXP, int level)
    {
        baseHPValue = baseHP;
        baseEXPValue = baseEXP;
        HP_Slider.value = HP;
        EXP_Slider.value = EXP;
        HP_Text.text = HP + " / " + baseHPValue;
        EXP_Text.text = EXP + " / " + baseEXPValue;
        level_Text.text = "Level: " + level;
    }
    public void SetUpSlider(float baseHP = 0, float baseEXP = 0)
    {
        if (baseHP != 0) baseHPValue = baseHP;
        if (baseEXP != 0) baseEXPValue = baseEXP;
    }
    public void UpdateHPSlider(float HP)
    {
        HP_Slider.value = HP / baseHPValue;
        HP_Text.text = HP + " / " + baseHPValue;
    }
    public void UpdateEXPSlider(float EXP)
    {
        EXP_Slider.value = EXP / baseEXPValue;
        EXP_Text.text = EXP + " / " + baseEXPValue;
    }
    public void UpdateLevelText(int level)
    {
        level_Text.text = "Level: " + level;
    }
}
