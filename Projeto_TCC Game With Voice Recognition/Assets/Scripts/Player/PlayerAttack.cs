using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAttack : MonoBehaviour {

    public UnityEvent OnAttack;
    public UnityEvent ReleaseAttack;

    public AudioClip meleeSfx;
    public AudioClip shotSfx;

    public float attackRate = 0.5f;

    public Rigidbody2D bulletPrefab;
    public Transform shotSpawn;
    public float fireRate = 0.25f;
    public float shotImpulse = 10f;
    private float nextFire;

    private bool canAttack = true;

    private PlayerAnimation playerAnimation;
    private PlayerController playerController;
    private AudioManager audioManager;

    private void Awake() {

        playerAnimation = GetComponent<PlayerAnimation>();
        playerController = GetComponent<PlayerController>();
        audioManager = GetComponent<AudioManager>();

    }

    public void Fire() {

        if (!PlayerSkills.instance.skills.Contains(Skills.Gun))
            return;

        if(Time.time > nextFire) {

            audioManager.PlayAudio(shotSfx);

            nextFire = Time.time + fireRate;
            Rigidbody2D newBullet = Instantiate(bulletPrefab, shotSpawn.position, shotSpawn.rotation);
            newBullet.AddForce(transform.right * shotImpulse, ForceMode2D.Impulse);
        }
    }

    public void MeleeAttack() {

        if (!PlayerSkills.instance.skills.Contains(Skills.Melee))
            return;

        if (canAttack) {

            audioManager.PlayAudio(meleeSfx);

            canAttack = false;
            OnAttack.Invoke();
            playerAnimation.setMeleeAttack();
            if (!playerController.IsOnIce())
            {

               playerController.DisableControls();

            }
            Invoke("FinishAttack", attackRate);
        }
    }

    void FinishAttack() {

        canAttack = true;
        ReleaseAttack.Invoke();

    }


}
