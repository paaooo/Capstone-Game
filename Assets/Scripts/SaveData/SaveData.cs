using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public static SaveData instance { get; private set; }
    GameObject player;

    string unityPath;
    string persistentPath;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        SetPaths();
        Load();
    }
    public void SavePosition(Vector3 position)
    {
        string json = JsonUtility.ToJson(position);

        using StreamWriter writer = new StreamWriter(persistentPath);
        writer.Write(json);
    }
    public void LoadPosition() {
        using StreamReader reader = new StreamReader(persistentPath);
        string json = reader.ReadToEnd();

        Vector3 position = JsonUtility.FromJson<Vector3>(json);
        player.transform.position = position;
    }
    void Load()
    {
        try
        {
            LoadPosition();
        } catch
        {
            SavePosition(Vector3.zero);
            LoadPosition();
        }
    }
    void SetPaths()
    {
        unityPath = Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json"; // for unity editor
        persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SaveData.json"; // for application
    }
}
