//Can Reduce code by merging *Data script//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json.Linq;


public class ItemManager : MonoBehaviour
{
    //variable for position, which will be used for calculating random position between two points
    public Transform RightPosition;
    //delay between spawns
    public float spawnDelay;
    //array for prefabs, which should be spawn
    public List<string> itm;
    private float width;
    private double widthd;
    private ItemData item;

    //will be executed once at start
    void Start()
    {
        //"Spawn" function will be called repeatedly
        InvokeRepeating("ItemSpawn", spawnDelay, spawnDelay);
        widthd = Screen.width / Screen.dpi;
        width = (float)widthd;
        item = new ItemData()
        {
            land = PlayerPrefs.GetString("land"),
            level = PlayerPrefs.GetString("level"),
        };
        StartCoroutine(Items(item));
    }

    //spawn function
    void ItemSpawn()
    {
        //calculate random position between AsteroidSpawner and RighPosition
        Vector3 spawnPos = new Vector3(Random.Range(-width, width), transform.position.y, 0);
        //calculate random variable i between 0 and array length (number of members)
        int i = Random.Range(0, itm.Count-1);
        //place prefab at calculated position
        Debug.Log(itm[i]);
        Instantiate(Resources.Load(itm[i]), spawnPos, transform.rotation);
    }
    public IEnumerator Items(ItemData itemm)
    {
//        Debug.Log(itemm);
        var jsonData = JsonUtility.ToJson(itemm);
        //Debug.Log(jsonData);
        using (UnityWebRequest www = UnityWebRequest.Post(GlobalConstant.apiURL + "/item", jsonData))
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
                        itm.Add((string)tempArray[i]["item"]);
                    }
                }
                else
                {
                    Debug.Log("Error! data couldn't get.");
                }
            }
        }
    }
}
