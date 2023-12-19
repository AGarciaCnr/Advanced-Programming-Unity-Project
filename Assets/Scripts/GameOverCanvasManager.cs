using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverCanvasManager : Singleton<GameOverCanvasManager>
{
    private TextMeshProUGUI scoreText;

    private void Awake()
    {
        scoreText = GetComponentInChildren<TextMeshProUGUI>();
        CursorManager.ShowCursor();
    }

    public void SetScore(int score)
    {
        scoreText.text = "Score: " + score.ToString();
    }
    
    public void OnRestartButtonClicked()
    {
        CursorManager.HideCursor();
        SceneManager.LoadGame();
    }
}
