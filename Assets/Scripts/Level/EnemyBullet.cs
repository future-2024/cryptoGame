using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour
{

    //reference variable for Players gameobject
    GameObject player;
    //reference variable for Rigidbody2D
    Rigidbody2D rb;
    //variable for force power
    public float force;
    //variable for damage value
    public int damage;
    //variable for sound clip
    public AudioClip BulletSound;
    private PlayerHP hpScript;
    private Score score;

    //will be executed once at start
    void Start()
    {
        score = GameObject.Find("ScoreManger").GetComponent<Score>();
        hpScript = GameObject.Find("SpaceShip").GetComponent<PlayerHP>();
        //reference to Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
        //search for gameobject with tag Player and reference to them
        player = GameObject.FindWithTag("Player");
        //check if Player is in the scene
        if (player != null)
        {
            //calculate direction vector to Player
            Vector3 dir = player.transform.position - transform.position;
            //calculate angle between X-axe and direction vector
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg+Random.Range(0, 1);
            //rotate the bullets gameobject
            transform.Rotate(0, 0, angle);
            //push bullet on its locale X-axe (it will be forward for this image)
            rb.velocity = new Vector2(dir.x, dir.y) * force;
            //play sound (gameobject will be created at the position, which will play the sound and then destroy itself)
            AudioSource.PlayClipAtPoint(BulletSound, transform.position);
            //if Player isn't in the scene
        }
        else
            //delete bullet from scene
            Destroy(gameObject);
    }

    //will be executed if Collider2D went into Trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        //check if collided gameobject has tag Player
        if (other.gameObject.tag == "Player" && hpScript.shieldDetected == false)
        {
            //try to execute function "MakeDamage" with parameter "damage" in scripts connected to collided gameobject
            other.gameObject.SendMessage("MakeDamage", damage, SendMessageOptions.DontRequireReceiver);
            //delete bullet
            Destroy(gameObject);
        }
    }

    //will be executed if bullet is not rendered anymore (out of screen)
    void OnBecameInvisible()
    {
        //delete bullet
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
            Destroy(gameObject);
        }
    }
}