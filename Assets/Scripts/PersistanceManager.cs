using UnityEngine;
using System.IO;

public class PersistanceManager : MonoBehaviour
{
    public static PersistanceManager Instance;

    public string TeamName;
    public int BestScore;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadData();
    }

    [System.Serializable]
    class SaveableData
    {
        public string TeamName;
        public int BestScore;
    }

    public void SaveData()
    {
        SaveableData data = new SaveableData();
        data.TeamName = TeamName;
        data.BestScore = BestScore;
        string json = JsonUtility.ToJson(data);
        string path = Path.Combine(Application.persistentDataPath, "savefile.json");
        File.WriteAllText(path, json);
    }

    public void LoadData()
    {
        string path = Path.Combine(Application.persistentDataPath, "savefile.json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveableData data = JsonUtility.FromJson<SaveableData>(json);

            if (data != null)
            {
                TeamName = data.TeamName;
                BestScore = data.BestScore;
            }
        }
    }

    // Helper to set the team name and persist immediately
    public void SetTeamName(string name)
    {
        TeamName = name ?? string.Empty;
        SaveData();
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

}
