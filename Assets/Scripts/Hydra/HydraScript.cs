using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json.Linq;
using System.Data;
using UnityEngine.Networking;
public class HydraScript : MonoBehaviour
{
    public Button level_1;
    public Button level_2;
    public Button back;
    private string url;

    public Image backgroundImage;

    private GameObject window;
    List<GameObject> levelButtons;
    float height;
    float width;
    int symbol;
    public Text starCnt;
    // Start is called before the first frame update
    void Start()
    {
        starCnt.text = "0";
        url = GlobalConstant.apiURL + "/playHistory";
        Button levelBut_1 = level_1.GetComponent<Button>();
        levelBut_1.onClick.AddListener(level1);
        
        Button levelBut_2 = level_2.GetComponent<Button>();
        levelBut_2.onClick.AddListener(level2);

        Button backBtn = back.GetComponent<Button>();
        backBtn.onClick.AddListener(backFun);

        window = GameObject.Find("Levels_V1Blue/BG/Leves_Page1/").gameObject;
        levelButtons = new List<GameObject>();
        for (int cnt = 0; cnt < window.transform.childCount; cnt++)
        {
            levelButtons.Add(window.transform.GetChild(cnt).gameObject);
        }
        height = 1440;
        width = 2560;
        backgroundImage.rectTransform.sizeDelta = new Vector2(width, height);
        
        StartCoroutine(ShowLevel());
        symbol = 1;
    }

    // Update is called once per frame
    void Update()
    {
        height = (float)(height + 0.01 * 9 * symbol);
        width = (float)(width + 0.01 * 16 * symbol);
        if (width > 3840)
            symbol = -1;
        else if (width < 2560)
            symbol = 1;
        backgroundImage.rectTransform.sizeDelta = new Vector2(width, height);
    }
    void level1()
    {
        PlayerPrefs.SetString("level", "1");
        Application.LoadLevel(GlobalConstant.level1_1);
    }
    void level2()
    {
        PlayerPrefs.SetString("level", "2");
        Application.LoadLevel(GlobalConstant.level1_2);
    }
    void backFun()
    {
        Application.LoadLevel(GlobalConstant.landList);
    }

    public IEnumerator ShowLevel()
    {
        var items = new UserItem()
        {
            userName = PlayerPrefs.GetString("userName")
        };
        var jsonData = JsonUtility.ToJson(items);
        using (UnityWebRequest www = UnityWebRequest.Post(url + "/getHydra", jsonData))
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
                    LevelEvent hydraHistory = JsonUtility.FromJson<LevelEvent>(result);
                    int tempStarCnt = 0;
                    for(int cnt=0; cnt < hydraHistory.events.Length + 1; cnt++)
                    {
                        if(cnt != hydraHistory.events.Length)
                            tempStarCnt += hydraHistory.events[cnt].starCnt;
                        starCnt.text = tempStarCnt.ToString();
                        levelButtons[hydraHistory.events.Length].gameObject.transform.GetChild(4).gameObject.SetActive(true);
                        levelButtons[cnt].gameObject.transform.GetChild(5).gameObject.SetActive(false);
                        levelButtons[cnt].gameObject.GetComponent<Button>().interactable = true;

                        if(cnt < hydraHistory.events.Length) { 
                            for(int cnt2 = 0; cnt2 < hydraHistory.events[cnt].starCnt; cnt2++)
                            {
                                levelButtons[cnt].gameObject.transform.GetChild(1 + cnt2).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                            }
                            for (int cnt2 = 0; cnt2 < 3; cnt2++)
                            {
                                levelButtons[cnt].gameObject.transform.GetChild(1 + cnt2).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                            }
                        }
                    }
                    Debug.Log(hydraHistory);
                }
                else
                {
                    Debug.Log("Error! data couldn't get.");
                }
            }
        }
    }
}
