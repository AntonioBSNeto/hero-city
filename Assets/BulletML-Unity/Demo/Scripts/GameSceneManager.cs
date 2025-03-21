using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class GameSceneManager : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "HighScore")
        {
            UpdateHighScore();
        }
    }

    public void LoadDemoFight()
    {
        SceneManager.LoadScene("Demo_Fight");
    }

    public void LoadHighScore()
    {
        SceneManager.LoadScene("HighScore");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadHighScoreAfterDelay(float delay)
    {
        StartCoroutine(LoadSceneAfterDelay("HighScore", delay));
    }

    private IEnumerator LoadSceneAfterDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

    private void UpdateHighScore()
    {
        if (highScoreText != null)
        {
            highScoreText.text = ScoreManager.instance.score.ToString();
        }
    }
}