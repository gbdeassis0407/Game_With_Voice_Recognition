using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlataform : MonoBehaviour {

    public float speed;
    public Transform[] wayPoints;
    public float waitTime;

    private int dir = 1;
    private int index;
    private bool wait;
    private float timer;

    // Update is called once per frame
    void Update() {

        if(wait) {
            CoutingWaitTime();
            return;
        }

        ChangeWaypoints();

        Moving();

    }


    void CoutingWaitTime() {
        timer += Time.deltaTime;
        if (timer >= waitTime)
        {
            wait = false;
            timer = 0;
        }
    }

    void ChangeWaypoints() {
        float distance = Vector2.Distance(transform.position, wayPoints[index].position);
        if (dir > 0 && distance <= 0)
        {
            index++;
            if (index >= wayPoints.Length)
            {
                index = wayPoints.Length - 1;
                dir = -1;
                wait = true;
            }
        }
        else if (dir < 0 && distance <= 0)
        {
            index--;
            if (index < 0)
            {
                index = 0;
                dir = 1;
                wait = true;
            }
        }

    }

    void Moving() {
        transform.position = Vector2.MoveTowards(transform.position, wayPoints[index].position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other) {

        if (other.gameObject.CompareTag("Player")) {

            other.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Player")) {

            other.transform.SetParent(null);
        }
    }


}