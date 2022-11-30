using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    private AudioSource audioSource;

    private void Awake() {

        audioSource = GetComponent<AudioSource>();

    }

    public void PlayAudio(AudioClip clip) {

        audioSource.clip = clip;
        audioSource.Play();

    }

    public void PlayAudioAtPoint(Vector2 audioPosition, AudioClip clip) {

        Vector3 newAudioPos = new Vector3(audioPosition.x, audioPosition.y, Camera.main.transform.position.z);
        AudioSource.PlayClipAtPoint(clip, newAudioPos);

    }



}
