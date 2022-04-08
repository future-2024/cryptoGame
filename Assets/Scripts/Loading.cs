using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Loading : MonoBehaviour
{
    public GameObject LoadBar;
    public Text text;
    public Text powerText;

    Image LoadBarImage;
    private int cnt = 0;
    void Start()
    {
        LoadBarImage = LoadBar.GetComponent<Image>();
        LoadBarImage.fillAmount = 0;
        //StartCoroutine(LoadNextLevel());
        //Start the coroutine we define below named ChangeAfter2SecondsCoroutine().
        StartCoroutine(ChangeAfter2SecondsCoroutine());
        InvokeRepeating("LoadStatus", 1.0f, 0.01f);
        if (powerText)
        {
            StartCoroutine(FadeTextToFullAlpha(3f, powerText));
        }
    }


    IEnumerator ChangeAfter2SecondsCoroutine()
    {
        
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);
        //yield on a new YieldInstruction that waits for 2 seconds.
        yield return new WaitForSeconds(5);

        //After we have waited 2 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        //And load the scene
        Application.LoadLevel("LandList");
    }
    void Update()
    {
        
    }
    void LoadStatus()
    {
        cnt++;
        LoadBarImage.fillAmount = cnt / 400.0f;
        text.text = (cnt / 4).ToString() + "%";
    }
    public IEnumerator FadeTextToFullAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
