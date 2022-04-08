using UnityEngine;
using System.Collections;
//we need the namespace for access on Unity UI
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HpPlayer : MonoBehaviour
{

    //reference to gameobject HealthBar
    public GameObject HealthBar2;
    //public bool gameOver = false;
    //reference variable to Image component in HealthBar
    Image img2;
    //current HP
    public int hp2;
    //maximum HP value, will be used for % count
    float maxHp2;

    //will be executed once
    void Start()
    {
        //reference to Image component in PlayerHP
        img2 = HealthBar2.GetComponent<Image>();
        //set maximum HP as current HP
        maxHp2 = hp2;
        //change fill amount between 0 and 1 (here will be 1 or 100%)
        img2.fillAmount = hp2 / maxHp2;
    }

    //will be called from the scripts on other gameobjects (like Bullet)
    void MakeDamage(int damage)
    {
        //decrease hp value
        hp2 = hp2 - damage;
        //check if hp is zero or negative
        if (hp2 <= 0)
        {
            
            //load current scene (reload)
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        //change fill amount between 0 and 1
        img2.fillAmount = hp2 / maxHp2;
    }
}
