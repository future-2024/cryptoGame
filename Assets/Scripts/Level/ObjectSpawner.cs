using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json.Linq;


public class ObjectSpawner : MonoBehaviour
{

    //variable for position, which will be used for calculating random position between two points
    public Transform RightPosition;
    //delay between spawns
    public Transform LeftPosition;
    public GameObject BGBossBar;
    public float spawnDelay;
    //array for prefabs, which should be spawn
    private Score score;
    //will be executed once at start
    private EnemyData enemies;
    public List<string> enemyName;
    public List<string> bossName;
    GlobalConstant constants;
    public bool on = false;
    HpController bossHpController;
    public GameObject BossObject;
    GameObject miniMap;
    GameObject enemyMap;
    int verify;
    void Start()
    {
        verify = 0;
        miniMap = (GameObject)Resources.Load("Icon_TargetYellow");
        
        score = GameObject.Find("ScoreManger").GetComponent<Score>();
        //"Spawn" function will be called repeatedly
        InvokeRepeating("Spawn", 5, spawnDelay);
        enemies = new EnemyData()
        {
            land = PlayerPrefs.GetString("land"),
            level = PlayerPrefs.GetString("level"),
        };
        StartCoroutine(Enemy(enemies));
        StartCoroutine(Boss(enemies));
    }
    //spawn function
    void Spawn()
    {
        if (score.winBool == false && score.boss == false)
        {
            // calculate random position between AsteroidSpawner and RighPosition
            Vector3 spawnPos = new Vector3(Random.Range(LeftPosition.position.x, RightPosition.position.x), transform.position.y, 0);
            // calculate random variable i between 0 and array length (number of members)
            int i = Random.Range(0, enemyName.Count);
            //Debug.Log(enemyName[i]);
            Instantiate(Resources.Load(enemyName[i]), spawnPos, transform.rotation);
            
/*            enemyMap = (GameObject)Instantiate(miniMap, spawnPos, transform.rotation);
            enemyMap.transform.SetParent(GameObject.Find("BG/MINIMAP1").transform, false);
            verify++;
*/
        }
        if (score.boss == true && on == false) {
            BGBossBar.SetActive(true);
            
            Vector3 spawnPos = new Vector3(Random.Range(LeftPosition.position.x, RightPosition.position.x), transform.position.y, 0);
            Instantiate(Resources.Load(bossName[0]), spawnPos, transform.rotation);
            on = true;
  
        }
    }
    void Update()
    {
        /*byby
        if (enemyMap)
        {
            enemyMap.name = enemyMap.name + verify;
            Debug.Log(enemyMap.name);

            //Debug.Log("in");
            GameObject.Find("MINIMAP1/Icon_TargetYellow(Clone)" + verify).gameObject.GetComponent<RectTransform>().anchoredPosition = Camera.main.ViewportToWorldPoint(GameObject.FindGameObjectWithTag("Enemy").transform.position);
            Debug.Log(verify);
        }*/
    }
    public IEnumerator Enemy(EnemyData enemy)
    {
        var jsonData = JsonUtility.ToJson(enemy);
        using (UnityWebRequest www = UnityWebRequest.Post(GlobalConstant.apiURL + "/enemy", jsonData))
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
                    var tempArray = JArray.Parse(result);
                    for (int i = 0; i < tempArray.Count; i++)
                    {
                        enemyName.Add((string)tempArray[i]["enemy"]);
                    }                    
                }
                else
                {
                    Debug.Log("Error! data couldn't get.");
                }
            }
        }
    }
    public IEnumerator Boss(EnemyData enemy)
    {
        
        var jsonData = JsonUtility.ToJson(enemy);
        using (UnityWebRequest www = UnityWebRequest.Post(GlobalConstant.apiURL + "/boss", jsonData))
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
                    var tempArray = JArray.Parse(result);
                    for (int i = 0; i < tempArray.Count; i++)
                    {
                        bossName.Add((string)tempArray[i]["boss"]);
                        
                        bossHpController = ((GameObject)Resources.Load(bossName[0])).GetComponent<HpController>();
                        if (bossHpController.gameObject.tag == "boss")
                        {
                            bossHpController.hp = (int)tempArray[i]["hp"];
                            Debug.Log("bossname" + bossHpController.hp);
                        }
                    }
                   // Vector3 spawnPos = new Vector3(Random.Range(LeftPosition.position.x, RightPosition.position.x), transform.position.y, 0);
                }
                else
                {
                    Debug.Log("Error! data couldn't get.");
                }
            }
        }
    }
}