using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupPlayerInfo : MonoBehaviour
{
    private PlayerBehaviour playerInstance;
    public List<PlayerStatsDisplay> playerStatsDisplay;
    private void Awake()
    {
        playerInstance = PlayerBehaviour.instance;
    }
    private void OnEnable()
    {
        UpdatePlayerInfo();
    }

    private void UpdatePlayerInfo()
    {
        for (int i = 0; i < playerStatsDisplay.Count; i++)
        {
            switch (playerStatsDisplay[i].playerStatsName.text)
            {
                case "Level":
                    playerStatsDisplay[i].playerStatsValue.text = playerInstance.level.ToString();
                    break;
                case "Strength":
                    playerStatsDisplay[i].playerStatsValue.text = playerInstance.strength.ToString();
                    break;
                case "Speed":
                    playerStatsDisplay[i].playerStatsValue.text = playerInstance.moveSpeed.ToString();
                    break;
                case "Fire Rate":
                    playerStatsDisplay[i].playerStatsValue.text = (1 / playerInstance.attackSpeed).ToString("F1");
                    break;
                case "Crit. Chance":
                    playerStatsDisplay[i].playerStatsValue.text = playerInstance.critChance.ToString() + "%";
                    break;
                case "Crit. Damage":
                    playerStatsDisplay[i].playerStatsValue.text = playerInstance.critDamage.ToString("F1") + "x";
                    break;
                default:
                    break;
            }
        }
    }
}

[System.Serializable]
public class PlayerStatsDisplay
{
    public TextMeshProUGUI playerStatsName;
    public TextMeshProUGUI playerStatsValue;
}
