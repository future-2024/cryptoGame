using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Shoot : MonoBehaviour
{
    public GameObject laser;
    public float laserDelay;
    public float bulletDelay;
    void Start()
    {
        InvokeRepeating("Laser", 0, laserDelay);
        InvokeRepeating("bullet", 0, bulletDelay);
    }
    private void Laser()
    {
        StartCoroutine(LaserShoot());
    }

    IEnumerator LaserShoot() {
        Instantiate(laser, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        
    }
}