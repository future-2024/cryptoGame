using UnityEngine;
using System.Collections;
//namespace with different scene controlls
using UnityEngine.SceneManagement;

public class AsteroidMove : MonoBehaviour {

    //variable for flying speed
    public float speed;
    private PlayerHP script;
    Rigidbody2D rb;
    private Score score;
    //will be executed once at start

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
    private void Update()
    {
        OnBecameVisible();
    }

    void OnBecameVisible()
    {        
        if (score.winBool == true)
        {
            Debug.Log("enemy");
            Destroy(gameObject);
        }
    }

    //will be executed if different Collider2D have touched each other
    void OnCollisionEnter2D (Collision2D something) {

        //check Tag of touched gameobject
        if (something.gameObject.tag == "Player") {
            something.gameObject.SendMessage("MakeDamage", 3, SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }
    }
    private void Move()
    {
        if (script.gameOver == false) { 
            Vector3 move = new Vector3(Random.Range(-1, 1), Random.Range(0, -5), 0);
            rb.velocity = move * speed;
        }
    }
}