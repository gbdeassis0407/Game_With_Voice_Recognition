using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour {

    public int power = 1;

    [Header("Shaker Values")]
    public float powerValue = 60;
    public float duration = 0.4f;

    private void OnTriggerEnter2D(Collider2D other) {

        Damageable damageable = other.GetComponent<Damageable>();
        if(damageable != null) {

            damageable.TakeDamage(power, transform.position.x);
            Shaker.instance.SetValues(powerValue, duration);

        }

    }
}
