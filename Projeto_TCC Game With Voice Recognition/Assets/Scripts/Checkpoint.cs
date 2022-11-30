using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    public Sprite checkPointLighted;
    public GameObject lights;

    private bool isActive;

    private SpriteRenderer spriterRenderer;
    private CheckpointController checkPointController;

    private void Awake() {

        spriterRenderer = GetComponent<SpriteRenderer>();

    }

    private void Start() {

        checkPointController = FindObjectOfType<CheckpointController>();

    }

    private void OnTriggerEnter2D(Collider2D other) {

        if (isActive)
            return;

        if (other.CompareTag("Player")) {

            checkPointController.SetPos(transform.position);
            spriterRenderer.sprite = checkPointLighted;
            lights.SetActive(true);
            isActive = true;

        }


    }



}
