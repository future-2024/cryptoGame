using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{
    public int destroyDelay;
    void OnBecameVisible()
    {        
        StartCoroutine(DestroyLaser());
        
    }
    IEnumerator DestroyLaser () {
        yield return new WaitForSeconds(destroyDelay);
//        Debug.Log("enemy");
        Destroy(gameObject);
    }
    void Update()
    {
        
    }
}
