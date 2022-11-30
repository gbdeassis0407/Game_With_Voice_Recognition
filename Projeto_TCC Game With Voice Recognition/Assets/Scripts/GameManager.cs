using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class PlayerData {

    public int stageIndex;
    public List<Skills> skills;

}

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public int stageIndex;
    public List<Skills> skills;

    public float masterVol, musicVol, sfxVol;

    private string path;

    private void Awake() {
        
        if(instance == null) {

            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else if (instance != this) {

            Destroy(gameObject);

        }

        path = Application.persistentDataPath + "/playerSave.sav";

        Load();

    }


    // Start is called before the first frame update
    void Start() {
        


    }

    public void SavePlayerPrefs(float masterVolume, float musicVolume, float sfxVolume) {

        PlayerPrefs.SetFloat("masterVol", masterVolume);
        PlayerPrefs.SetFloat("musicVol", musicVolume);
        PlayerPrefs.SetFloat("sfxVol", sfxVolume);
        masterVol = masterVolume;
        musicVol = musicVolume;
        sfxVol = sfxVolume;

    }

    public void Save() {

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(path);
        PlayerData data = new PlayerData();

        data.stageIndex = stageIndex;
        data.skills = skills;

        bf.Serialize(file, data);

        file.Close();

    }

    void Load() {

        masterVol = PlayerPrefs.GetFloat("masterVol");
        musicVol = PlayerPrefs.GetFloat("musicVol");
        sfxVol = PlayerPrefs.GetFloat("sfxVol");

        if (File.Exists(path)) {

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);

            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            stageIndex = data.stageIndex;
            skills = data.skills;

        }

    }

}
