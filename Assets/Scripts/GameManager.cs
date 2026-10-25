using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject pausePanel;

    private bool isPaused = false;
    private bool isGameOver = false;
    private bool hasWon = false;
    private int score = 0;

    #region pause
    private void Update()
    {
        if (Keyboard.current != null &&
            Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            TogglePause();
        }
    }
    public void TogglePause()
    {
        // Não permite pausar ou retomar após o fim do jogo.
        if (isGameOver || hasWon)
            return;

        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }
    public void ResumeGame()
    {
        if (!isPaused)
            return;

        isPaused = false;
        Time.timeScale = 1f;

        if (pausePanel != null)
            pausePanel.SetActive(false);
    }
    public void PauseGame()
    {
        if (isGameOver || hasWon || isPaused)
            return;

        isPaused = true;
        Time.timeScale = 0f;

        if (pausePanel != null)
            pausePanel.SetActive(true);
    }
    #endregion

    #region game over / restart
    public void GameOver()
    {
        if (isGameOver || hasWon)
            return;
        isGameOver = true;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    #endregion

    #region points
    public void PointUP()
    {
        if (isGameOver || hasWon)
            return;

        score++;
        UpdateScoreUI();
    }
    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = score.ToString();
    }
    #endregion

    
}
