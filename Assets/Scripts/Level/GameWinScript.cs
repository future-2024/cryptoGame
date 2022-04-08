using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameWinScript : MonoBehaviour
{
    public Button nLevelBut;
    public Button eixtBut;
    public Button restartBut;
    public GameObject gamewinObject;
    private Score scoreScript;
    public GameObject Brilliant;
    public TextMeshProUGUI ScoreText;
    public Text nextLevelText;

    int cnt;
    // Start is called before the first frame update
    void Start()
    {
        scoreScript = GameObject.Find("ScoreManger").GetComponent<Score>();


        Button restartButC = restartBut.GetComponent<Button>();
        restartButC.onClick.AddListener(restartApp);

        Button exitButC = eixtBut.GetComponent<Button>();
        exitButC.onClick.AddListener(exitApp);

        Button nextButC = nLevelBut.GetComponent<Button>();
        nextButC.onClick.AddListener(nextApp);

        cnt = 0;
    }
    void restartApp()
    {
        if (scoreScript) {
            Debug.Log("score" + scoreScript.score);
        }
        Time.timeScale = 1;
        gamewinObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void exitApp()
    {
        Application.LoadLevel(GlobalConstant.EndScene);
    }
    void countUp()
    {
        Debug.Log("max score" + scoreScript.maxScore);
        cnt++;
        if(cnt <= scoreScript.maxScore) 
        { 
            if(ScoreText)
            {
                ScoreText.text = cnt.ToString();
            }
        }
        else
        {
            Time.timeScale = 0;
        }
    }
    void nextApp()
    {
        if(nextLevelText)
            Application.LoadLevel(nextLevelText.text);
    }
}
