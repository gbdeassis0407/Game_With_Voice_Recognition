using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneGoal : MonoBehaviour {

    public int unlockStage;

    private void OnTriggerEnter2D(Collider2D other) {

        if (other.CompareTag("Player")) {

            UIManager.instance.LoadScene();
            if(GameManager.instance != null) {

                if(GameManager.instance.stageIndex < unlockStage) {

                    GameManager.instance.stageIndex = unlockStage;

                }

                GameManager.instance.Save();

            }

        }

    }

}
