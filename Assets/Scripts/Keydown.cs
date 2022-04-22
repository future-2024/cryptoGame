using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keydown : MonoBehaviour
{
    public AudioClip mySound;

    // Use this for initialization
    void Start()
    { 
    }

    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("100");
            AudioSource.PlayClipAtPoint(mySound, transform.position);
        */
        foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(vKey))
            {
                Debug.Log("keydown");
                AudioSource.PlayClipAtPoint(mySound, transform.position);
            }
        }
    }
}
