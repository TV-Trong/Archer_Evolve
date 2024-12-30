using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isGameStop;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        isGameStop = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isGameStop = false;
    }
}
