using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainAudio : MonoBehaviour
{
    AudioSource mainMusic;
    private PlayerHP playScript;
    private Score scoreScript;
    // Start is called before the first frame update
    void Start()
    {        
        playScript = GameObject.Find("SpaceShip").GetComponent<PlayerHP>();
        scoreScript = GameObject.Find("ScoreManger").GetComponent<Score>();
        mainMusic = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       if(playScript) { 
           if (playScript.gameOver == true)
            {
                mainMusic.Stop();
            }
            else if(scoreScript.particle == true)
            {
                mainMusic.Stop();
            }
        }       
    }
}
