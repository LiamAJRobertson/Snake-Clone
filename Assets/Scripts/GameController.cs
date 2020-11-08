using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject scorePanel;
    [SerializeField] private TextMeshProUGUI scoreText;

    [HideInInspector] public int score = 0;

    //Restarts the game by reloading the current scene
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    //Reveals the game over screen
    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        scorePanel.SetActive(false);
    }

    //Updates the score and UI when the player gets a collectable 
    public void AddToScore()
    {
        score++;

        string scoreStr = score.ToString();

        scoreText.SetText("Score: " + scoreStr);
    }
}
