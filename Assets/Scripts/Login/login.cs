using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;


public class login : MonoBehaviour
{
    public GameObject log;

    public InputField username;
    public InputField password;
    public Text alert;

    public GameObject signUpObject;

    public string url2 = GlobalConstant.apiURL + "/login";
    public Button SignUp;
    void Start()
    {
        Button loginBut = log.GetComponent<Button>();
        loginBut.onClick.AddListener(LogIn);

        Button SignUpBtn = SignUp.GetComponent<Button>();
        SignUpBtn.onClick.AddListener(signUp);

        url2 = GlobalConstant.apiURL + "/login";
    }

    void LogIn()
    {
        var user = new userdata()
        {
            name = username.text,
            pass = password.text
        };
        PlayerPrefs.SetString("name", username.text);
        StartCoroutine(Log(url2, user));
    }
    void signUp()
    {
        signUpObject.SetActive(true);
        // Application.LoadLevel(GlobalConstant.SignUp);
    }
    public IEnumerator Log(string url2, userdata user)
    {

        var jsonData = JsonUtility.ToJson(user);
        using (UnityWebRequest www = UnityWebRequest.Post(url2, jsonData))

        {
            www.SetRequestHeader("content-type", "application/json");
            www.uploadHandler.contentType = "application/json";
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));
            yield return www.SendWebRequest();
            Debug.Log(url2);
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
                    Debug.Log(result);
                    alert.text = result;
                    if (result == "Logged In")
                    {
                        alert.text = "Welcome!";
                        PlayerPrefs.Save();
                        Application.LoadLevel(GlobalConstant.LoadingScene);
                    }
                }
                else
                {
                    //handle the problem
                    Debug.Log("Error! data couldn't get.");
                }
            }
        }
    }
}

