using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public int healthAmount = 1;

    public void GainHealth() {

        FindObjectOfType<PlayerController>().GetComponent<Damageable>().SetHealth(healthAmount);

    }

}
