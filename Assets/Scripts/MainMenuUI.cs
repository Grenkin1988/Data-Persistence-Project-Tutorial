using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField]
    private Text playerName;
    [SerializeField]
    private Text highScore;

    private void Start()
    {
        var score = HighScoreManager.Instance.CurrentHighScore;
        playerName.text = score?.name;

        var scoreText = score is null ? "None" : $"{score.name} : {score.score}";
        highScore.text = $"Best score: {scoreText}";
    }

    public void StartNew()
    {
        HighScoreManager.Instance.CurrentUser = playerName.text;
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        HighScoreManager.Instance.SaveScore();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}
