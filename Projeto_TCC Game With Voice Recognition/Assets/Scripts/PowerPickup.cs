using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPickup : MonoBehaviour {

    public Skills skill;
    public Sprite pickedSprite;
    public string text;

    private bool picked;
    public SpriteRenderer spriteRenderer;

    private void Awake() {

        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void OnTriggerEnter2D(Collider2D other) {

        if (picked)
            return;

        PlayerAnimation player = other.GetComponent<PlayerAnimation>();
        if(player != null) {

            UIManager.instance.SetText(text);
            picked = true;
            spriteRenderer.sprite = pickedSprite;
            PlayerSkills.instance.skills.Add(skill);
            if(skill == Skills.Gun) 
                player.SetGun();

            GameManager.instance.skills.Add(skill);

        }

    }

    private void OnTriggerExit2D(Collider2D other) {

        if (other.CompareTag("Player")) {

            UIManager.instance.SetTextOut();

        }

    }


}
