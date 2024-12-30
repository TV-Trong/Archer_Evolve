using UnityEngine;

public class CheatCode : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            Debug.Log("Gain 30 EXP");
            PlayerBehaviour.instance.GainExp(30);
        }
    }
}
