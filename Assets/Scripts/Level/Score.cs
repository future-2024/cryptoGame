using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json.Linq;

public class Score : MonoBehaviour
{

    // Start is called before the first frame update
    // public static Score Instance { get; private set; }SW
    public string url;
    public string saveHistoryUrl;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI powerText;
    public TextMeshProUGUI maxScoreText;
    public int maxScore;
    public int score;
    public int killNumber;
    public int power;
    public int flagWinParticle = 0;
    public bool winBool = false;
    public bool particle = false;
    public GameObject winGame;
    public GameObject win;
    public GameObject ship;
    private PlayerHP hpScript;
    private ItemManager itemScript;
    public Text startText;
    public Text countText;
    public Text winText;
    private itemdata items;
    private HistoryItem historyItem;
    public bool boss;
    ObjectSpawner objectSpawners;
    private TimeScript timeManage;

    private string player;
    public GameObject spawner;
    private int maxHp;

    void Start()
    {
        url = GlobalConstant.apiURL + "/level";
        saveHistoryUrl = GlobalConstant.apiURL + "/playHistory";
        itemScript = GameObject.Find("itemmanager").GetComponent<ItemManager>();
       
        objectSpawners = GameObject.Find("AsteroidSpawner").GetComponent<ObjectSpawner>();
        timeManage = GameObject.Find("Border").GetComponent<TimeScript>();
        score = 0;
        killNumber = 0;
        flagWinParticle = 0;
        if (scoreText)
        {
            scoreText.text = "Score: 0";
        }
        
        
        items = new itemdata()
        {
            land = PlayerPrefs.GetString("land"),
            level = PlayerPrefs.GetString("level"),
        };
        Debug.Log(items.land);
        StartCoroutine(Server(items));
        StartCoroutine(loading());

        maxScore = 20;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreText)
        {
            scoreText.text = "Score: " + score.ToString();
        }
        if (powerText)
        {
            if (hpScript)
            {
                powerText.text = hpScript.hp.ToString() + "FT";
            }
        }
        if (killNumber > maxScore)
        {
            if (maxScoreText)
            {
                maxScoreText.text = maxScore.ToString();
            }
            afterWin();
        }
    }
    void afterWin()
    {
        if (flagWinParticle == 0 && winBool == true)
        {
            objectSpawners.BGBossBar.SetActive(false);
            Instantiate(win, ship.transform.position, Quaternion.identity);
            StartCoroutine(winParticle());
            flagWinParticle = 2;
        } else {
            boss = true;
        }

    }
    
    public IEnumerator Server(itemdata items)
    {

        var jsonData = JsonUtility.ToJson(items);
        using (UnityWebRequest www = UnityWebRequest.Post(url, jsonData))
        {
            www.SetRequestHeader("content-type", "application/json");
            www.uploadHandler.contentType = "application/json";
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    //var result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    var tempArray = JObject.Parse(www.downloadHandler.text);
                    maxHp = (int)tempArray["maxHp"];
                    maxScore = (int)tempArray["maxScore"];
                    player = (string)tempArray["player"];
                    Debug.Log("player---" + player);
                }
                else
                {
                    Debug.Log("Error! data couldn't get.");
                }
            }
        }
    }

    IEnumerator loading()
    {
        yield return new WaitForSeconds(4);
        Destroy(startText);
        Destroy(countText);
        Instantiate(Resources.Load(player), spawner.transform.position, transform.rotation);
        //StartCoroutine(LoadingRequest());
        //ship.SetActive(true);
        hpScript = GameObject.Find("SpaceShip(Clone)").GetComponent<PlayerHP>();
        hpScript.maxHp = maxHp;
        if (powerText)
        {
            powerText.text = hpScript.hp.ToString() + "FT";
        }
        hpScript.maxHp = 20;
    }
    

        IEnumerator winParticle()
    {
        //winBool = true;
        winText.text = "Mission Completed!";
        yield return new WaitForSeconds(5);
        particle = true;
        winText.text = " ";
        StartCoroutine(SaveData());
        winGame.SetActive(true);
    }
    public IEnumerator SaveData()
    {
        historyItem = new HistoryItem()
        {
            userName = PlayerPrefs.GetString("userName"),
            land = PlayerPrefs.GetString("land"),
            level = PlayerPrefs.GetString("level"),
            time = (int)timeManage.miliSecond,
            hp = hpScript.hp,
            itemCnt = hpScript.itemCnt,
        };
        var jsonData = JsonUtility.ToJson(historyItem);
        using (UnityWebRequest www = UnityWebRequest.Post(saveHistoryUrl, jsonData))
        {
            www.SetRequestHeader("content-type", "application/json");
            www.uploadHandler.contentType = "application/json";
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));

            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    var result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    //var tempArray = JObject.Parse(www.downloadHandler.text);
                    Debug.Log(result);
                }
                else
                {
                    Debug.Log("Error! data couldn't get.");
                }
            }
        }
    }
    
}
