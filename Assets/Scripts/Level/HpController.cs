using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HpController : MonoBehaviour
{

    public int hp;    
    int maxHp1;
    public AudioClip ExplosionsSound;
    public GameObject Explosion;
    
    private Score scoreScript;
    public GameObject HealthBar_enemy;
    Image img_enemy;

    private void Start()
    {
        scoreScript = GameObject.Find("ScoreManger").GetComponent<Score>();
        if (gameObject.tag == "boss") {
            img_enemy = GameObject.Find("boss").GetComponent<Image>();
        }
        else {
            img_enemy = HealthBar_enemy.GetComponent<Image>();
        }
        maxHp1 = hp;
        img_enemy.fillAmount = 1;
    }
    void MakeDamage(int damage)
    {
        hp = hp - damage;
        img_enemy.fillAmount = (float)hp / maxHp1;
        if (hp <= 0)
        {
            if (gameObject.tag == "boss") {
                scoreScript.winBool = true;
            }
            AudioSource.PlayClipAtPoint(ExplosionsSound, transform.position);
            Instantiate(Explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
            scoreScript.score += maxHp1;
            scoreScript.killNumber++;
        }
    }
}