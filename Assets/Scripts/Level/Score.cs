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
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI powerText;
    public TextMeshProUGUI maxScoreText;
    public int maxScore;
    public int score;
    public int power;
    int i = 0;
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

    void Start()
    {
        itemScript = GameObject.Find("itemmanager").GetComponent<ItemManager>();
        hpScript = GameObject.Find("SpaceShip").GetComponent<PlayerHP>();
        score = 0;
        i = 0;
        if (scoreText)
        {
            scoreText.text = "score: 0";
        }
        if (powerText)
        {
            powerText.text = hpScript.hp.ToString() + "FT";
        }
        
        items = new itemdata()
        {
            land = PlayerPrefs.GetString("land"),
            level = PlayerPrefs.GetString("level"),
        };

        StartCoroutine(loading());

        maxScore = 20;
        hpScript.maxHp = 20;
        StartCoroutine(Server(items));
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreText)
        {
            scoreText.text = "score: " + score.ToString();
        }
        if (powerText)
        {
            powerText.text = hpScript.hp.ToString() + "FT";
        }
        if (score > maxScore)
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
        if (i == 0)
        {
            Instantiate(win, ship.transform.position, Quaternion.identity);
            StartCoroutine(winParticle());
            i = 2;
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
                    var result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    var tempArray = JObject.Parse(www.downloadHandler.text);
                    hpScript.maxHp = (int)tempArray["maxHp"];                  
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
        ship.SetActive(true);

    }
    IEnumerator winParticle()
    {
        winBool = true;
        winText.text = "Mission Completed!";
        yield return new WaitForSeconds(5);
        particle = true;
        winText.text = " ";
        winGame.SetActive(true);
    }
}
