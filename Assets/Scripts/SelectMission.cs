using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json.Linq;
using System.Data;
using UnityEngine.Networking;
//using System.Web.Script.Serialization;

public class SelectMission : MonoBehaviour
{
    // Start is called before the first frame update
    public Button Hydro;
    public Button Centaurus;
    public Button Andromedas;
    public Button Dorado;
    public List<string> mission;
    private string url;
    void Start()
    {
        url = GlobalConstant.apiURL + "/playHistory";
        Button hydroBut = Hydro.GetComponent<Button>();
        hydroBut.onClick.AddListener(HydroEvent);
        Button centBut = Centaurus.GetComponent<Button>();
        centBut.onClick.AddListener(CentEvent);
        Button androBut = Andromedas.GetComponent<Button>();
        androBut.onClick.AddListener(AndroEvent);
        Button doraBut = Dorado.GetComponent<Button>();
        doraBut.onClick.AddListener(DoradoEvent);

        StartCoroutine(GetPlayHistory());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void HydroEvent()
    {
        PlayerPrefs.SetString("land", "hydro");
        Application.LoadLevel(GlobalConstant.land1Name);
    }
    void CentEvent()
    {
        PlayerPrefs.SetString("land", "centaurus");
        Application.LoadLevel(GlobalConstant.land2Name);
    }
    void AndroEvent()
    {
        PlayerPrefs.SetString("land", "andro");
        Application.LoadLevel(GlobalConstant.land3Name);
    }
    void DoradoEvent()
    {
        PlayerPrefs.SetString("land", "dorado");
        Application.LoadLevel(GlobalConstant.land4Name);
    }
    public IEnumerator GetPlayHistory()
    {
        var items = new UserItem()
        {
            userName = PlayerPrefs.GetString("userName")
        };
        var jsonData = JsonUtility.ToJson(items);
        using (UnityWebRequest www = UnityWebRequest.Post(url + "/getHistory", jsonData))
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
                   // var tempArray = JArray.Parse(www.downloadHandler.text);
                   // for (var cnt = 0; cnt < tempArray.Count; cnt++)
                   // {
                   //       mission.Add((string)tempArray[cnt]["land"], (string)tempArray[cnt]["level"]);
                   //       mission.Add((string)tempArray[cnt]["level"]);
                   //       mission.Add((string)tempArray[cnt]["userName"]);
                   //       mission.Add((string)tempArray[cnt]["time"]);
                   //       mission.Add((string)tempArray[cnt]["levelScore"]);
                   //       mission.Add((string)tempArray[cnt]["starCnt"]);
                   //       Debug.Log("sss"+mission[0]);
                   // }
                   // HistoryArray historyArray = JsonUtility.FromJson<HistoryArray>(www.downloadHandler.text);
                   // Debug.Log(tempArray);
                   // JavaScriptSerializer js = new JavaScriptSerializer();
                   // HistoryItem[] history = js.Deserialize<HistoryItem[]>(result);
                   // Debug.Log(history);
                }
                else
                {
                    Debug.Log("Error! data couldn't get.");
                }
            }
        }
    }
}
