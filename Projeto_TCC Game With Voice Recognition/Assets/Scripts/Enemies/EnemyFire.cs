using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour {

    public float fireRate;
    public Rigidbody2D bulletPrefab;
    public Transform shotSpawn;
    public Vector2 shotImpulse;
    public AudioClip shotSfx;
    public AudioClip openMouthSfx;

    private Animator anim;
    private AudioManager audioManager;


    private void Awake() {

        anim = GetComponent<Animator>();
        audioManager = GetComponent<AudioManager>();

    }

    // Start is called before the first frame update
    void Start() {

        InvokeRepeating("SetFire", fireRate, fireRate);

    }

    void SetFire() {

        audioManager.PlayAudio(openMouthSfx);
        anim.SetTrigger("Fire");

    }

   void Fire() {

        audioManager.PlayAudio(shotSfx);

        Rigidbody2D newBullet = Instantiate(bulletPrefab, shotSpawn.position, transform.rotation);
        newBullet.AddForce(shotImpulse, ForceMode2D.Impulse);
        //Destroy(newBullet.gameObject, 5);

    }




}
