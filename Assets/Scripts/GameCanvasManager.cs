using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameCanvasManager : Singleton<GameCanvasManager>
{
    [SerializeField]
    private TextMeshProUGUI _wave, _score;

    private void Awake()
    {
        UpdateWave(0);
        UpdateScore(0);
    }

    public void UpdateWave(int wave)
    {
        _wave.text = "Wave: " + wave.ToString();
    }

    public void UpdateScore(int score)
    {
        _score.text = "Score: " + score.ToString();
    }
}
