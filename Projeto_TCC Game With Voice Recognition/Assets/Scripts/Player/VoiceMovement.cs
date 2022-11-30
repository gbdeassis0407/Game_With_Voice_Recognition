using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.Events;

public class VoiceMovement : MonoBehaviour {

    public UnityEvent Abaixado;
    public UnityEvent Levantado;

    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    public PlayerController playerController;
    public PlayerAnimation playerAnimation;
    public PlayerAttack playerAttack;
    private AudioManager audioManager;

    public AudioClip meleeSfx;

    private int action = 0;

    private void Awake() {

        playerController = GetComponent<PlayerController>();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerAttack = GetComponent<PlayerAttack>();
        audioManager = GetComponent<AudioManager>();
    }

    private void Start()
    {
        actions.Add("para", () => { action = 0; });
        actions.Add("direita", () => { action = 1; });
        actions.Add("esquerda", () => { action = 2; });
        actions.Add("pula", () => { action = 3; });
        actions.Add("ataca", () => { action = 4; });
        actions.Add("atira", () => { action = 5; });
        actions.Add("abaixa", () => { action = 6; });
        actions.Add("levanta", () => { action = 7; });

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }

    void Update() {
        check();
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech) {

        Debug.Log(speech.text);
        actions[speech.text].Invoke();


    }


private void check() {


        if (action == 0) {
            Debug.Log("Parado");
            playerAnimation.SetIdle(true);
            playerAnimation.SetIdle(false);

        }

        else if (action == 1) {
            Debug.Log("Andando");
            playerController.Move(1);
          
        }


        else if (action == 2) {
            Debug.Log("Voltando");
            playerController.Move(-1);

        }

        else if (action == 3) {
            Debug.Log("Pulando");
            playerController.Jump();
            action = 0;

        }

        else if (action == 4) {
            Debug.Log("Atacando");

            if (!PlayerSkills.instance.skills.Contains(Skills.Melee))
                return;

            audioManager.PlayAudio(meleeSfx);

            playerAnimation.setMeleeAttack();
            action = 0;

        }

        else if (action == 5) {

            if (!PlayerSkills.instance.skills.Contains(Skills.Gun))
                return;

            Debug.Log("Atirando");
            playerAttack.Fire();
            action = 0;

        }

        else if (action == 6) {
            Abaixado.Invoke();
            Debug.Log("Agaichando");

        }

        else if (action == 7) {
            Debug.Log("Levantando");
            Levantado.Invoke();

        }

    }
     
}

