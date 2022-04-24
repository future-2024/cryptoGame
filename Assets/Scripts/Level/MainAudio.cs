using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainAudio : MonoBehaviour
{
    AudioSource mainMusic;
    public GameObject player;
    public GameObject score;
    private PlayerHP playScript;
    private Score scoreScript;
    // Start is called before the first frame update
    void Start()
    {        
        mainMusic = GetComponent<AudioSource>();
        //ToggleSound();
    }

    // Update is called once per frame
    void Update()
    {
        playScript = player.GetComponent<PlayerHP>();
        scoreScript = score.GetComponent<Score>();
        if (playScript) { 
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
    public void ToggleSound()
    {
            AudioListener.volume = 1f;
    }
}
