using System.IO;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager Instance;

    public HighScore CurrentHighScore { get; private set; }
    public string CurrentUser { get; set; }

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadScore();
    }

    public void UpdateIfHighScore(int highScore)
    {
        if(highScore >= CurrentHighScore.score)
        {
            CurrentHighScore = new HighScore { score = highScore, name = CurrentUser };
        }
        SaveScore();
    }

    public void SaveScore()
    {
        HighScore data = new HighScore
        {
            name = CurrentHighScore.name,
            score = CurrentHighScore.score
        };

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/score.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/score.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            HighScore data = JsonUtility.FromJson<HighScore>(json);

            CurrentHighScore = data;
        }
    }

    [System.Serializable]
    public class HighScore
    {
        public string name;
        public int score;
    }
}
