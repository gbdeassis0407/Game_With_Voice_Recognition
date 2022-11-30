using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckpointController : MonoBehaviour {

    public Transform player;
    public Animator fade;

    public UnityEvent OnRestart;

    private Vector3 respawPosition;

    public void SetPos(Vector3 pos) {

        respawPosition = pos;

    }

    public void GameOver() {

        Invoke("Restart", 3f);
        fade.Play("Fade");

    }

    public void Restart() {

        player.position = respawPosition;
        OnRestart.Invoke();

    }


}
