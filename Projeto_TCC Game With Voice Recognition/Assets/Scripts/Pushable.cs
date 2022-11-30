using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : MonoBehaviour {

    public float defaultMass = 25;

    private Rigidbody2D rb;

    private AudioSource audioSource;

    private void Awake() {

        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        
    }

    private void FixedUpdate() {
        
        if(Mathf.Abs(rb.velocity.x) > 0) {

            if (!audioSource.isPlaying) {

                audioSource.Play();

            }

        }
        else {

            audioSource.Stop();

        }


    }

    private void OnTriggerEnter2D(Collider2D other) {

        if (other.CompareTag("Player")) {

            other.GetComponent<PlayerController>().SetPushing(true);
            other.GetComponent<PlayerAnimation>().SetPush(true);
            rb.mass = 1;

        }

    }

    private void OnTriggerExit2D(Collider2D other) {

        if (other.CompareTag("Player"))
        {

            other.GetComponent<PlayerController>().SetPushing(false);
            other.GetComponent<PlayerAnimation>().SetPush(false);
            rb.mass = defaultMass;

        }

    }


}
