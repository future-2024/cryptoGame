using UnityEngine;
using System.Collections;

public class LaserShot : MonoBehaviour
{

    //reference variable for Rigidbody2D
    Rigidbody2D rb;
    //variable for damage
    public int damage;
    //variable for force power
    public float force;
    //variable for sound clip
    public AudioClip BulletSound;
    public string bulletmode = "default";
    public int count = 1;
    public PlayerShot playerScript;
    //will be executed once
    void Start()
    {
        //reference to Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
        playerScript = GameObject.Find("SpaceShip(Clone)").GetComponent<PlayerShot>();
        //declare Vector3 with force value on Y-axe
        if (playerScript.bulletMode == "speed")
        {
            Vector3 directon = new Vector3(0, force*5, 0);
            //add force push on rigidbody2D for moving on Y-axe
            rb.AddForce(directon, ForceMode2D.Impulse);
        }
        else
        {
            Vector3 directon = new Vector3(0, force*2, 0);
            //add force push on rigidbody2D for moving on Y-axe
            rb.AddForce(directon, ForceMode2D.Impulse);
        }
        
        
        //play sound (gameobject will be created at the position, which will play the sound and then destroy itself)
        AudioSource.PlayClipAtPoint(BulletSound, transform.position);
    }
    //will be executed, if the gameobject is not rendering anymore on the screen
    void OnBecameInvisible()
    {
        //delete this gameobject from the scene
        Destroy(gameObject);
    }
    //will be executed if one other Collider2D went into Trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        //check if other gameobject has tag Enemy
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "boss")
        {
            //try to call MakeDamage on other gameobject, send damage as parameter for it
            if (playerScript.bulletMode == "default" || playerScript.bulletMode == "parallel" || playerScript.bulletMode == "speed")
            {
                other.gameObject.SendMessage("MakeDamage", damage, SendMessageOptions.DontRequireReceiver);
                Destroy(gameObject);
            }
            
            //delete gameobject from scene
            else if (playerScript.bulletMode == "bulletpower")
            {
                //try to call MakeDamage on other gameobject, send damage as parameter for it
                other.gameObject.SendMessage("MakeDamage", damage * 3, SendMessageOptions.DontRequireReceiver);
                Destroy(gameObject);
                //delete gameobject from scene
            }
            else if (playerScript.bulletMode == "through")
            {
                other.gameObject.SendMessage("MakeDamage", damage, SendMessageOptions.DontRequireReceiver);
            }


        }
    }
}