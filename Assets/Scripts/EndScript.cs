using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EndScript : MonoBehaviour
{
    public Button quit;
    // Start is called before the first frame update
    void Start()
    {
        Button btnExit = quit.GetComponent<Button>();
        btnExit.onClick.AddListener(ExitApp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator ChangeAfter2SecondsCoroutine()
    {

        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);
        //yield on a new YieldInstruction that waits for 2 seconds.
        yield return new WaitForSeconds(1);

        //After we have waited 2 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        //And load the scene
        Application.Quit();
    }
    void ExitApp()
    {
        Debug.Log("exit");
        Application.Quit();
    }
}
