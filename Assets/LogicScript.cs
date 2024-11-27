using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public int playerScore; // Score tracker
    public Text ScoreText; // UI text for displaying score
    public GameObject gameOverScreen; // Reference to the Game Over screen
    public bool isGameOver = false; // Flag to prevent updates after game over
    public MusicManager musicManager; // Reference to the MusicManager

    void Start()
    {
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false); // Hide game over screen
        }
    }

    public void AddScore()
    {
        playerScore++;
        ScoreText.text = playerScore.ToString();
        Debug.Log($"Score updated to {playerScore}");
    }

    public void GameOver()
    {
        if (isGameOver) return; // Prevent multiple game over triggers
        isGameOver = true;
        Time.timeScale = 0f; // Freeze game updates

        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);

            // Update the points text
            GameObject pointsTextObject = GameObject.FindGameObjectWithTag("finalPoints");
            if (pointsTextObject != null)
            {
                TMPro.TMP_Text pointsText = pointsTextObject.GetComponent<TMPro.TMP_Text>();
                if (pointsText != null)
                {
                    pointsText.text = playerScore.ToString() + " POINTS";
                }
            }
        }
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;

        // Clear all dynamically spawned objects
        FishMovement fishMovement = GameObject.FindObjectOfType<FishMovement>();
        if (fishMovement != null)
        {
            fishMovement.ResetFishSpawning();
        }

        rockspawn rockSpawn = GameObject.FindObjectOfType<rockspawn>();
        if (rockSpawn != null)
        {
            rockSpawn.ResetRockSpawning();
        }

        // Reload the scene to reset everything
        //SceneManager.LoadScene("Game");
        ResetManager.Instance.ResetGame();

        isGameOver = false;
    }

}
