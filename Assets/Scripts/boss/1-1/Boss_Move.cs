using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Move : MonoBehaviour
{
    public float speed;
    private PlayerHP script;
    Rigidbody2D rb;
    private Score score;
    void Start()
    {
        score = GameObject.Find("ScoreManger").GetComponent<Score>();
        rb = GetComponent < Rigidbody2D > ();
        script = GameObject.Find("SpaceShip").GetComponent<PlayerHP>();
        InvokeRepeating("Move", 0, 2);
    }

    //will be executed if gameobject is not rendered anymore on screen
    void OnBecameInvisible () {
        //delete gameobject from scene
        Destroy(gameObject);
    }
    void OnCollisionEnter2D (Collision2D something) {
        if (something.gameObject.tag == "Player") {
            something.gameObject.SendMessage("MakeDamage", 5, SendMessageOptions.DontRequireReceiver);
        }
    }
    private void Move()
    {
        if (script.gameOver == false) { 
            Vector3 move = new Vector3(Random.Range(-2, 2), 0, 0);
            rb.velocity = move * speed;
        }
    }
}