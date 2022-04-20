using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Globalization;

public class GameWinScript : MonoBehaviour
{
    public Button nLevelBut;
    public Button eixtBut;
    public Button restartBut;
    public GameObject gamewinObject;
    public GameObject ScoreBar;

    private Score scoreScript;
    private TimeScript timeScript;
    private PlayerHP hpScript;

    public GameObject Brilliant;

    public Text ScoreRecordText;
    public Text ScoreText;
    public Text TimeText;
    public Text enemyNum;

    public string nextLevelText;
    public string nextLevel;

    private GameObject window;

    private string url;

    List<GameObject> starArray;
    Slider scoreBarImg;
    // int cnt;

    // Start is called before the first frame update
    void Start()
    {
        starArray = new List<GameObject>();
        scoreScript = GameObject.Find("ScoreManger").GetComponent<Score>();
        timeScript = GameObject.Find("Border").GetComponent<TimeScript>();
        hpScript = GameObject.Find("SpaceShip").GetComponent<PlayerHP>();

        Button restartButC = restartBut.GetComponent<Button>();
        restartButC.onClick.AddListener(restartApp);

        Button exitButC = eixtBut.GetComponent<Button>();
        exitButC.onClick.AddListener(exitApp);

        Button nextButC = nLevelBut.GetComponent<Button>();
        nextButC.onClick.AddListener(nextApp);
        ScoreText.text = "Score:" + scoreScript.score;
        TimeText.text = "Delay Time:" + (int)timeScript.miliSecond;
        // cnt = 0;
        StartCoroutine(getPlayHistory());

        window = GameObject.Find("gameWin/BG").gameObject;
        scoreBarImg = ScoreBar.GetComponent<Slider>();

        for(int cnt = 1; cnt < 4; cnt++)
        {
            starArray.Add(window.transform.GetChild(cnt).gameObject);
        }

        url = GlobalConstant.apiURL;
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
    /*void countUp()
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
    }*/
    void nextApp()
    {
        PlayerPrefs.SetString("level", nextLevel);
        Application.LoadLevel(nextLevelText);
    }
    public IEnumerator getPlayHistory()
    {
        var sendData = new HistoryItem()
        {
            land = PlayerPrefs.GetString("land"),
            level = PlayerPrefs.GetString("level"),
            userName = PlayerPrefs.GetString("userName"),
            time = (int)timeScript.miliSecond,
            hp = hpScript.hp,
            itemCnt = hpScript.itemCnt,
        };
        var jsonData = JsonUtility.ToJson(sendData);
        url = GlobalConstant.apiURL + "/playHistory/getLevelHistory";
        using (UnityWebRequest www = UnityWebRequest.Post(url, jsonData))

        {
            www.SetRequestHeader("content-type", "application/json");
            www.uploadHandler.contentType = "application/json";
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));
            yield return www.SendWebRequest();
            Debug.Log(url);
            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    // handle the result
                    var result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    result = "{\"events\":" + result + "}";
                    LevelEvent winHistory = JsonUtility.FromJson<LevelEvent>(result);
                    for (int cnt = 0; cnt < winHistory.events[0].starCnt; cnt++)
                    {
                        starArray[cnt].GetComponent<Toggle>().isOn = true;
                    }
                    if (winHistory.events[0].levelScore < winHistory.events[0].maxScore)
                        scoreBarImg.value = winHistory.events[0].levelScore / winHistory.events[0].maxScore;
                    else 
                        scoreBarImg.value = 1;
                    ScoreRecordText.text = winHistory.events[0].levelScore.ToString("F2", CultureInfo.InvariantCulture) + "/" + winHistory.events[0].maxScore.ToString("F2", CultureInfo.InvariantCulture);
                    enemyNum.text = "Kills: " + scoreScript.killNumber;
                    Debug.Log(winHistory);
                }
                else
                {
                    // handle the problem
                    Debug.Log("Error! data couldn't get.");
                }
            }
        }
    }
}
