using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

    public AudioMixer mixer;

    public GameObject loadPanel;

    private string currentScene;

    public Button[] stageButtons;

    public Slider masterVolSlider;
    public Slider musicVolSlider;
    public Slider sfxVolSlider;

    // Start is called before the first frame update
    void Start() {

        UnlockStageButtons();
        SetVolumes();

    }

    void SetVolumes() {

        masterVolSlider.value = GameManager.instance.masterVol;
        musicVolSlider.value = GameManager.instance.musicVol;
        sfxVolSlider.value = GameManager.instance.sfxVol;


    }


    void UnlockStageButtons() {

        for (int i = 0; i < GameManager.instance.stageIndex; i++) {

            stageButtons[i].interactable = true;

        }

    }

    float GetVol(float vol) {

        float newVol = 0;
        newVol = 20 * Mathf.Log10(vol);
        if (vol <= 0)
        {
            newVol = -80;

        }

        return newVol;

    }

   public void SetMasterVol (float vol) {

        mixer.SetFloat("MasterVol", GetVol(vol));

    }

    public void SetMusicVol (float vol) {

        mixer.SetFloat("MusicVol", GetVol(vol));

    }

    public void SetSFXVol(float vol) {

        mixer.SetFloat("SFXVol", GetVol(vol));

    }

    public void LoadScene(string scene) {

        loadPanel.SetActive(true);
        currentScene = scene;
        Invoke("Loading", 0.5f);
        GameManager.instance.SavePlayerPrefs(masterVolSlider.value, musicVolSlider.value, sfxVolSlider.value);

    }

    void Loading() {

        SceneManager.LoadSceneAsync(currentScene);

    }

    public void Quit() {

        Application.Quit();

    }

}
