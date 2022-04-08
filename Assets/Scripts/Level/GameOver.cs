using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    public Button Exit;
    public Button Restart;
    public GameObject gameoverObject;

    void Start()
    {
        Button btnExit = Exit.GetComponent<Button>();
        btnExit.onClick.AddListener(ExitApp);

        Button btnRestart = Restart.GetComponent<Button>();
        btnRestart.onClick.AddListener(RestartApp);
    }
    void ExitApp()
    {
        Application.LoadLevel(GlobalConstant.EndScene);
    }
    void RestartApp()
    {
        Time.timeScale = 1;
        gameoverObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
