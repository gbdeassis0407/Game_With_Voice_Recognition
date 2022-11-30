using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.Events;


public class VoiceMovementCase : MonoBehaviour {

    public UnityEvent TaAbaixado;
    public UnityEvent TaLevantado;

    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    public PlayerController playerController;
    public PlayerAnimation playerAnimation;
    public PlayerAttack playerAttack;
    private AudioManager audioManager;

    public AudioClip meleeSfx;

    public int caseSwitch = 1;
    private int action = 0;

    private void Awake()
    {

        playerController = GetComponent<PlayerController>();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerAttack = GetComponent<PlayerAttack>();
        audioManager = GetComponent<AudioManager>();
    }

    private void Start()
    {
        actions.Add("para", () => { caseSwitch = 1; });
        actions.Add("direita", () => { caseSwitch = 1; });
        actions.Add("esquerda", () => { caseSwitch = 2; });
        actions.Add("pula", () => { caseSwitch = 3; });
        actions.Add("ataca", () => { caseSwitch = 4; });
        actions.Add("atira", () => { caseSwitch = 5; });
        actions.Add("abaixa", () => { caseSwitch = 6; });
        actions.Add("levanta", () => { caseSwitch = 7; });
        //actions.Add("pula direita", () => { action = 8; });


        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }

    void Update() {

    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech) {
    
        Debug.Log(speech.text);
        actions[speech.text].Invoke();

    }

    private void check() {

        switch (caseSwitch)
        {
            case 1:
                Debug.Log("Parado");
                playerAnimation.SetIdle(true);
                playerAnimation.SetIdle(false);
                break;

            case 2:
                Debug.Log("Andando");
                playerController.Move(1);
                break;

            case 3:
                Debug.Log("Voltando");
                playerController.Move(-1);
                break;

            case 4:
                Debug.Log("Pulando");
                playerController.Jump();
                break;

            case 5:
                Debug.Log("Atacando");

                if (!PlayerSkills.instance.skills.Contains(Skills.Melee))
                    return;

                audioManager.PlayAudio(meleeSfx);

                playerAnimation.setMeleeAttack();
                break;

            case 6:

                if (!PlayerSkills.instance.skills.Contains(Skills.Gun))
                    return;

                Debug.Log("Atirando");
                playerAttack.Fire();
                break;

            case 7:
                TaAbaixado.Invoke();
                Debug.Log("Agaichando");
                break;

            case 8:
                Debug.Log("Levantando");
                TaLevantado.Invoke();
                break;

            case 9:
                Debug.Log("pulo direita");
                playerController.Move(1);
                playerController.Jump();
                break;

        }

    }
}
