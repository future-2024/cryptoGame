using UnityEngine;
using System.Collections;
//namespace with different scene controlls
using UnityEngine.SceneManagement;

public class ItemsMove : MonoBehaviour
{

    //variable for flying itemSpeed
    public float itemSpeed;
    public bool once = true;
    public PlayerHP hpScript;
    //reference for Rigidbody2D
    Rigidbody2D rigidBody;

    //will be executed once at PlayerHP start
    void Start()
    {
        //reference to Rigidbody2D
        rigidBody = GetComponent<Rigidbody2D>();

        hpScript = GameObject.Find("SpaceShip").GetComponent<PlayerHP>();
        InvokeRepeating("ItemMove", 0, 10);
        //declare direction vector for moving (this will be along the Y-axe)
    }
    //will be executed if gameobject is not rendered anymore on screen
    void OnBecameInvisible()
    {
        //delete gameobject from scene
        Destroy(gameObject);
    }

    //will be executed if different Collider2D have touched each other
    void OnCollisionEnter2D(Collision2D collid)
    {

        //check Tag of touched gameobject
        if (collid.gameObject.tag == "Player")
        {
            Destroy (gameObject);
        }
    }
    private void ItemMove()
    {
        if (hpScript.gameOver == false)
        {
            Vector3 ItemMove = new Vector3(Random.Range(-1, 1), Random.Range(0, -5), 0);
            rigidBody.velocity = ItemMove * itemSpeed;
        }
    }
}