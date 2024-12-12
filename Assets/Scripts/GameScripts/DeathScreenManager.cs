using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathScreenManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    void Start()
    {
        scoreText.text = "SCORE: " + ScoreManager.score;
    }
}
