using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainAudio : MonoBehaviour
{
    AudioSource mainMusic;
    public GameObject player;
    public GameObject score;
    public GameObject volumeBut;
    private PlayerHP playScript;
    private Score scoreScript;
    private GameObject ToggleSlider;
    public GameObject volumeSlider;
    // Start is called before the first frame update
    void Start()
    {        
        mainMusic = GetComponent<AudioSource>();
        ToggleSound();
        ToggleSlider = GameObject.Find("ToggleSlider");
        ToggleSlider.GetComponent<Toggle>().enabled = true;

        Button volumeButC = volumeBut.GetComponent<Button>();
        volumeButC.onClick.AddListener(volumeFun);
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
        ToggleSound();
    }
    public void ToggleSound()
    {
        float volumeValue = volumeSlider.GetComponent<Slider>().value;
        AudioListener.volume = volumeValue;
    }
    public void volumeFun()
    {
        if(ToggleSlider.GetComponent<Toggle>().enabled == true)
        {
            ToggleSlider.GetComponent<Toggle>().enabled = false;
        }
        else
        {
            ToggleSlider.GetComponent<Toggle>().enabled = true;
        }
    }
}
