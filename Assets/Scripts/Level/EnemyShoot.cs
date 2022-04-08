using UnityEngine;
using System.Collections;

public class EnemyShoot : MonoBehaviour
{
    // variable for Bullet prefab
    public GameObject bullet;
    // delay between shots
    public float fireDelay;

    void Start()
    {
        InvokeRepeating("Bullet", 0, fireDelay);
    }
    private void Bullet()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
    }
}