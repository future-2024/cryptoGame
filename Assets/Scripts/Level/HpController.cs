using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HpController : MonoBehaviour
{

    //variable for health points
    public int hp;    
    //maximum HP value, will be used for % count
    float maxHp1;
    //variable for sound clip
    public AudioClip ExplosionsSound;
    //variable for explosion prefab
    public GameObject Explosion;
    
    private Score scoreScript;
    public GameObject HealthBar_enemy;
    Image img_enemy;

    //funkton for damage calculation (we will get damage from other functions)
    private void Start()
    {
        scoreScript = GameObject.Find("ScoreManger").GetComponent<Score>();
        //reference to Image component in PlayerHP
        img_enemy = HealthBar_enemy.GetComponent<Image>();
        //set maximum HP as current HP
        maxHp1 = hp;
        //change fill amount between 0 and 1 (here will be 1 or 100%)
        img_enemy.fillAmount = hp / maxHp1;
    }
    void MakeDamage(int damage)
    {
        //decrease hp variable
        hp = hp - damage;
        img_enemy.fillAmount = hp / maxHp1;
        //check if hp is negative or zero
        if (hp <= 0)
        {
            //play sound (gameobject will be created at the position, which will play the sound and then destroy itself)
            AudioSource.PlayClipAtPoint(ExplosionsSound, transform.position);
            //place explosion on gameobject position
            Instantiate(Explosion, transform.position, Quaternion.identity);
            //delete gameobject
            Destroy(gameObject);
            scoreScript.score++;            
        }
    }
}