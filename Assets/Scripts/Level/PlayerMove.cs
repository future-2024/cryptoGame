using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

    //variable for clicked position
    Vector3 clickPos;
    //we will use this vector as velocity for Rigidbody2D (direction and speed)
    Vector3 move;
    //variable for players speed
    private float speed = 10;

    private float width;
    private double widthd;
    private PlayerHP script;

    private float height;
    private float heightd;
    
    Collider2D m_ObjectCollider;

    //we will need reference to Players Rigidbody2D
    Rigidbody2D rb;

    //will be executed once at scripts start
    void Start() {
        script = GameObject.Find("SpaceShip").GetComponent<PlayerHP>();
        rb = GetComponent<Rigidbody2D>();
        m_ObjectCollider = GetComponent<Collider2D>();
    }

    //will be executed every frame
    void OnCollisionEnter2D(Collision2D collid)
    {

        //check Tag of touched gameobject
        if (collid.gameObject.tag == "speedUp")
        {
            speed = speed + 5;
            StartCoroutine(speedup());
        }
        if (collid.gameObject.tag == "fly")
        {
            m_ObjectCollider.isTrigger = true;
            StartCoroutine(fly());
        }

    }
    IEnumerator speedup()
    {
        yield return new WaitForSeconds(10);
        speed = 10;
    }
    IEnumerator fly()
    {
        yield return new WaitForSeconds(10);
        m_ObjectCollider.isTrigger = false;
        Debug.Log("here");
    }
    
    void Update () {    
            widthd = Screen.width / Screen.dpi;
            width = (float)widthd;

            heightd = Screen.height / Screen.dpi;
            height = (float)heightd;

            move.x = Input.GetAxis("Horizontal");
            move.y = Input.GetAxis("Vertical");
    }
    void FixedUpdate () {
        rb.velocity = new Vector2 (move.x, move.y) * speed;
        if (move.x != 0)
        {
            StartCoroutine(rotatex());
        }
        if (transform.position.x >width)
        {
            transform.position = new Vector2(width, transform.position.y);
            
        }
        if ( transform.position.x < -width)
        {
            transform.position = new Vector2(-width, transform.position.y);
        }
        if(transform.position.y > height)
        {
            transform.position = new Vector2(transform.position.x, height);
            
        }
        if ( transform.position.y < -height)
        {
            transform.position = new Vector2(transform.position.x, -height);
        }
    }
    IEnumerator rotatex()
    {
        transform.Rotate(0, 1.5f, 0, Space.Self);
        yield return new WaitForSeconds(0.5f);
        
        transform.Rotate(0, -1.5f, 0, Space.Self);
    }
}