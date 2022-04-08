using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ModalScript : MonoBehaviour
{
    public Button Back;
    public Button exit;
    public Button restart;
    public Button smallBack;
    public Button mainMenu;
    public GameObject modal;
    public bool ModalStatus = false;
    private AudioSource mainSource;
    
    // Start is called before the first frame update
    void Start()
    {
        Button btnBack = Back.GetComponent<Button>();
        btnBack.onClick.AddListener(backApp);
        
        Button btnSmallBack = smallBack.GetComponent<Button>();
        btnSmallBack.onClick.AddListener(backApp);

        Button btnExit = exit.GetComponent<Button>();
        btnExit.onClick.AddListener(exitApp);

        Button btnRestart = restart.GetComponent<Button>();
        btnRestart.onClick.AddListener(restartApp);

        Button btnMainMenu = mainMenu.GetComponent<Button>();
        btnMainMenu.onClick.AddListener(menuApp);

        mainSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        if (mainSource) 
        {
            mainSource.Pause();
        }
    }

    // Update is called once per frame
    void Update()
    {
        ModalStatus = false;
    }
    // Back is called when i am going to back.
    void backApp()
    {
        ModalStatus = true;
        modal.SetActive(false);
        Time.timeScale = 1;
        mainSource.Play();
    }
    void exitApp()
    {
        Application.LoadLevel(GlobalConstant.EndScene);
    }
    void restartApp()
    {
        //winScript
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void menuApp()
    {
        // Let's discuss about it.
        Application.LoadLevel(GlobalConstant.land1Name);
    }
}
