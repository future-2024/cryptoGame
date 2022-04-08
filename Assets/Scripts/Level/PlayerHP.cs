using UnityEngine;
using System.Collections;
//we need the namespace for access on Unity UI
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHP : MonoBehaviour
{
    public GameObject gameoverObject;
    //reference to gameobject HealthBar
    public GameObject HealthBar;
    //reference variable to Image component in HealthBar
    public GameObject HealthBarNav;
    public GameObject ItemBar;

    public AudioClip ExplosionsSound;
    public AudioClip VibrationSound;
    //variable for explosion prefab
    public GameObject Explosion;


    private Score scoreScript;
    private GameObject fires;
    private GameObject bigFires;
    private GameObject spaceShip;
    Image img;
    Image img2;
    Image img3;
    //current HP
    public int hp;
    //maximum HP value, will be used for % count
    public int maxHp;
    float tm;
    bool hhh = true;
    float itemTime = 10;
    float preTime;
    public bool gameOver = false;
    public bool shieldDetected = false;
    int i = 0;
    //private EnemyBullet damageScript;
    //will be executed once

    public GameObject over;
    public GameObject fire;
    public GameObject bigFire;
    public GameObject explosion;
    private SpriteRenderer sprite;
    public GameObject hpbar;
    
    void Start()
    {
        gameObject.SetActive(false);
        sprite = gameObject.GetComponent<SpriteRenderer>();
        i = 0;
        //reference to Image component in PlayerHP
        img = HealthBar.GetComponent<Image>();
        img3 = ItemBar.GetComponent<Image>();
        tm = 0;
        img3.fillAmount = tm / itemTime;

        //set maximum HP as current HP
        //hp = maxHp;
        //change fill amount between 0 and 1 (here will be 1 or 100%)
        img.fillAmount = 1;

        img2 = HealthBarNav.GetComponent<Image>();
        //change fill amount between 0 and 1 (here will be 1 or 100%)
        img2.fillAmount = 1;

        scoreScript = GameObject.Find("ScoreManger").GetComponent<Score>();
        
        scoreScript.power = hp + 1;
    }

    //will be called from the scripts on other gameobjects (like Bullet)
    void OnCollisionEnter2D(Collision2D other)
    {

        //check Tag of touched gameobject
        if (other.gameObject.tag == "hp")
        {
            hp += 5;            
        }
        if (other.gameObject.tag == "shield")
        {
			
            shieldDetected = true;
            StartCoroutine(shield());
            Destroy(other.gameObject);           
            preTime = Time.realtimeSinceStartup;
            tm = itemTime;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "bulletpower")
        {
            preTime = Time.realtimeSinceStartup;
            tm = itemTime;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "parallel")
        {
            preTime = Time.realtimeSinceStartup;
            tm = itemTime;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "speed")
        {
            preTime = Time.realtimeSinceStartup;
            tm = itemTime;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "through")
        {
            preTime = Time.realtimeSinceStartup;
            tm = itemTime;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "fly")
        {
            preTime = Time.realtimeSinceStartup;
            tm = itemTime;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "hp")
        {
            preTime = Time.realtimeSinceStartup;
            tm = itemTime;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "speedUp")
        {
            preTime = Time.realtimeSinceStartup;
            tm = itemTime;
            Destroy(other.gameObject);
        }

    }
    void MakeDamage(int damage)
    {
        
        iTween.ShakePosition(gameObject, iTween.Hash("amount", new Vector3(0.2f, 0.2f, 0.2f), "time", 0.1f, "delay", 0.01f, "easeType",       "easeInBounce"));
        AudioSource.PlayClipAtPoint(ExplosionsSound, transform.position);
        AudioSource.PlayClipAtPoint(VibrationSound, transform.position);
        //place explosion on gameobject position
        Instantiate(Explosion, transform.position, Quaternion.identity);
        scoreScript.power = hp;
        
        hp = hp - damage;
        if( hp < 1)
        {
            hp = 0;
        }
        img.fillAmount = (float)hp / maxHp;
        img2.fillAmount = (float)hp / maxHp;
    }
    private void Update()
    {
        if(hp == 0 && hhh == true)
        {
            hp = maxHp;
            hhh = false;
        }
        if (tm != 0)
        {
            tm = tm - (Time.realtimeSinceStartup - preTime)/500;
            
            img3.fillAmount = tm / itemTime;
        }
        if (hp > 0 && hp < 3)
        {
            particle();
        }
        else if (hp >= 3 && hp < 5)
        {
            critical();
        }
        if (hp <= 0)
        {
            Debug.Log("SpaceShip will be died!!!!");
            afterOver();
        }
    }
    void particle ()
    {
        iTween.PunchPosition(gameObject, iTween.Hash("amount", new Vector3(0, -0.5f, 0), "time", 0.2, "delay", 0.01));
        Instantiate(bigFire, transform.position, Quaternion.identity);
        StartCoroutine(NoFire(bigFire));
    }
    void afterOver()
    {
        if (i == 0)
        {
            hpbar.SetActive(false);
            Instantiate(over, transform.position, Quaternion.identity);
            StartCoroutine(overParticle());
            i = 2;
            sprite.enabled = false;
        }
    }
    void critical ()
    {
        iTween.PunchPosition(gameObject, iTween.Hash("amount", new Vector3(0, -0.5f, 0), "time", 0.2, "delay", 0.01));
        Instantiate(fire, transform.position, Quaternion.identity);
        StartCoroutine(NoFire(fire));
    }
    IEnumerator NoFire(GameObject objects)
    {
        fires = GameObject.Find("Fire(Clone)");
        bigFires = GameObject.Find("BigFire(Clone)");

        //pause function and return to it later in "delayTime" seconds
        yield return new WaitForSeconds(0.001F);

        //enable shooting for next check
        Destroy(fires);
        Destroy(bigFires);
    }
    
	IEnumerator shield()
    {
        yield return new WaitForSeconds(itemTime);
        shieldDetected = false;
    }
   
    IEnumerator overParticle()
    {
        yield return new WaitForSeconds(3);            
        gameOver = true;
        gameoverObject.SetActive(true);
        Time.timeScale = 0;
    }
}
