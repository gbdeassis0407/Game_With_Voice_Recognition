using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePlataform : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D other) {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if(player != null) {
            player.SetOnIce(true);
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.SetOnIce(false);
        }
    }


}
