using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keydown : MonoBehaviour
{
    public AudioClip mySound;

    // Use this for initialization
    void Start()
    {
        //var audioClip = Resources.Load<AudioClip>("05 - Listen to the Man_[plixid.com]");  //Load the AudioClip from the Resources Folder
        //mySound.clip = audioClip;  //Assign it as AudioSource's clip
        //audio.PlayOneShot(mySound);        
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("100");
            AudioSource.PlayClipAtPoint(mySound, transform.position);
            //print("space key was pressed");          
        }  //Also, I removed the extra semi-colon you had here
    }
}
