using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SavesManager
{
    public static void SaveProgress(SaveData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/SaveData";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveData LoadProgress()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/SaveData";

        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);
            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return data;
        } else
        {
            SaveProgress(new SaveData());
            return new SaveData();
        }
    }
}

[System.Serializable]
public class SaveData {
    public int[] levelScores;
    public int[] levelAttempts;
    public bool[] achievements;
    public float timePlayed;
    public float sfxVolume;
    public float musicVolume;

    public SaveData(int[] levelScoresInput = null, int[] levelAttemptsInput = null, bool[] achievementsInput = null, float timePlayedInput = -1f, float sfxVolumeInput = 1f, float musicVolumeInput = 1f)
    {
        levelScores = levelScoresInput != null ? levelScoresInput: new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        levelAttempts = levelAttemptsInput != null ? levelAttemptsInput : new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        achievements = achievementsInput != null ? achievementsInput : new bool[] { false, false, false, false, false, false, false, false, false, false, false, false };
        // achievements = { "OPEN THE GAME", "GO TO SLEEP", "CAN WE GET AN F?", "NICE", "BRUH MOMENT", "EPIC BRUH MOMENT", "PAIN", "TRUE PAIN", "JACK OF ALL TRADES", "PRACTICE MAKES PERFECT", "EPIC GAMER", "100% COMPLETION" };
        timePlayed = timePlayedInput > 0 ? timePlayedInput : Time.unscaledTime;
        sfxVolume = sfxVolumeInput;
        musicVolume = musicVolumeInput;
    }
}