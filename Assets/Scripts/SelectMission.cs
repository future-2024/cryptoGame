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
    private GameObject window;
    //Dictionary<int, GameObject> blackMaskobject;
    List<GameObject> blackMaskobject;
    List<GameObject> imgArray;
    List<GameObject> starArray;
    
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
        
        window = GameObject.Find("Scroll View/Viewport/Content").gameObject;
        blackMaskobject = new List<GameObject>();
        imgArray = new List<GameObject>();
        starArray = new List<GameObject>();
        for (int cnt = 0; cnt < window.transform.childCount; cnt++) {
            blackMaskobject.Add(window.transform.GetChild(cnt).gameObject.transform.Find("BlackMask").gameObject);
            imgArray.Add(window.transform.GetChild(cnt).gameObject.transform.Find("Image").gameObject);
            starArray.Add(window.transform.GetChild(cnt).gameObject.transform.Find("LevelStars").gameObject);
        }
        StartCoroutine(GetPlayHistory());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void HydroEvent()
    {
        PlayerPrefs.SetString("land", GlobalConstant.land1Name);
        Application.LoadLevel(GlobalConstant.land1Name);
    }
    void CentEvent()
    {
        PlayerPrefs.SetString("land", GlobalConstant.land2Name);
        Application.LoadLevel(GlobalConstant.land2Name);
    }
    void AndroEvent()
    {
        PlayerPrefs.SetString("land", GlobalConstant.land3Name);
        Application.LoadLevel(GlobalConstant.land3Name);
    }
    void DoradoEvent()
    {
        PlayerPrefs.SetString("land", GlobalConstant.land4Name);
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
                    result = "{\"events\":" + result + "}";
                    HistoryEvent playerHistory = JsonUtility.FromJson<HistoryEvent>(result);
                    for (int cnt = 0; cnt < playerHistory.events.Length; cnt++)
                    {
                        blackMaskobject[cnt].SetActive(false);
                        imgArray[cnt].SetActive(false);
                        for(int cnt2 = 0; cnt2 < playerHistory.events[cnt].cntLevel; cnt2 ++)
                        {
                            Debug.Log(starArray[cnt]);
                            starArray[cnt].transform.GetChild(cnt2 + 1).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                        }
                        
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
