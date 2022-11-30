using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    public static UIManager instance;

    public GameObject[] lives;

    public GameObject[] keys;

    public Text dialogueText;
    public Animator dialoguePanel;

    public GameObject loadPanel;
    public GameObject pausePanel;

    private bool paused;
    

    private void Awake() {

        instance = this;

    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.Escape)) {

            Pause();

        }

    }

    public void SetText (string text) {

        CancelInvoke();
        dialogueText.text = text;
        dialoguePanel.gameObject.SetActive(true);

    }

    public void SetTextOut() {

        Invoke("TextOut", 1f);

    }

    void TextOut() {

        dialoguePanel.Play("Dialogue Exit");
        Invoke("DisableDialoguePainel", 0.5f);

    }

    void DisableDialoguePainel() {

        dialoguePanel.gameObject.SetActive(false);


    }

    public void SetLives(int amount) {

        for (int i = 0; i < lives.Length; i++) {

            lives[i].SetActive(false);

        }

        for (int i = 0; i < amount; i++) {

            lives[i].SetActive(true);

        }

    }

    public void SetKeys(int amount) {

        for (int i = 0; i < keys.Length; i++) {

            keys[i].SetActive(false);

        }

        for (int i = 0; i < amount; i++) {

            keys[i].SetActive(true);

        }

    }

    public void LoadScene() {

        Time.timeScale = 1;
        loadPanel.SetActive(true);
        Invoke("Loading", 0.5f);

    }

    void Loading()
    {

        SceneManager.LoadSceneAsync("Menu");

    }

    public void Pause() {

        if(!paused && Time.timeScale == 0) {

            return;

        }

        paused = !paused;
        pausePanel.SetActive(paused);

        if (paused) {

            Time.timeScale = 0;

        }
        else {

            Time.timeScale = 1;

        }

    }

    public bool IsPaused() {

        return paused;

    }

    public void Quit() {

        Application.Quit();

    }


}
