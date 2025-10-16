using System.Collections.Generic;
using System.IO;

public class HighScoreHandler
{
    private string SCORE_FILE_PATH;


    public class ScoreEntry
    {
        public string PlayerName { get; set; }
        public int Score { get; set; }

        public ScoreEntry(string name, int score)
        {
            this.PlayerName = name;
            this.Score = score;
        }
    }

    public List<ScoreEntry> ScoreEntries { get => scoreEntries == null ? scoreEntries = LoadScoresFromFile() : scoreEntries; }


    private List<ScoreEntry> LoadScoresFromFile()
    {
        List<ScoreEntry> scores = new List<ScoreEntry>();
        var lines = File.ReadAllLines(SCORE_FILE_PATH);
        foreach (var line in lines)
        {
            var parts = line.Split(',');
            if (parts.Length == 2 && int.TryParse(parts[0], out int score))
            {
                scores.Add(new ScoreEntry(parts[1], score));
            }
        }
        return scores;
    }


    private List<ScoreEntry> scoreEntries;


    public HighScoreHandler()
    {
        if (!Directory.Exists(UnityEngine.Application.dataPath + "appdata"))
        {
            Directory.CreateDirectory(UnityEngine.Application.dataPath + "appdata");
        }
        if (!File.Exists(UnityEngine.Application.dataPath + "appdata/scores.txt"))
        {
            File.Create(UnityEngine.Application.dataPath + "appdata/scores.txt").Dispose();
        }
        SCORE_FILE_PATH = UnityEngine.Application.dataPath + "appdata/scores.txt";
    }


    public void AddScore(string playerName, int score)
    {
        using (StreamWriter sw = File.AppendText(SCORE_FILE_PATH))
        {
            sw.WriteLine($"{score},{playerName}");
        }
        scoreEntries = null; // Invalidate cache to reload scores next time
    }
}