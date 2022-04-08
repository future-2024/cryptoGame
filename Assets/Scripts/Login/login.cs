using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;


public class login : MonoBehaviour
{
    public GameObject sign;
    public GameObject log;

    public InputField username;
    public InputField email;
    public InputField password;
    public Text alert;
    public GameObject LeftDoor;
    public GameObject RightDoor;

    public string url = GlobalConstant.apiURL + "/users";
    public string url2 = GlobalConstant.apiURL + "/login";
    Rigidbody2D LeftDoorRigid;
    Rigidbody2D RightDoorRigid;

    void Start()
    {
        LeftDoorRigid = LeftDoor.GetComponent<Rigidbody2D>();
        RightDoorRigid = RightDoor.GetComponent<Rigidbody2D>();
        Button loginBut = log.GetComponent<Button>();
        loginBut.onClick.AddListener(LogIn);
        Button signBut = sign.GetComponent<Button>();
        signBut.onClick.AddListener(SignUp);
    }

    // Update is called once per frame
    void SignUp()
    {
        var user = new userdata()
        {
            name = username.text,
            email = email.text,
            pass = password.text
        };
        StartCoroutine(Sign(url, user));
    }
    void LogIn()
    {
        var user = new userdata()
        {
            name = username.text,
            email = email.text,
            pass = password.text
        };
        PlayerPrefs.SetString("name", username.text);
        StartCoroutine(Log(url2, user));
    }
    public IEnumerator Sign(string url, userdata user)
    {

        var jsonData = JsonUtility.ToJson(user);
        using (UnityWebRequest www = UnityWebRequest.Post(url, jsonData))

        {

            www.SetRequestHeader("content-type", "application/json");
            www.uploadHandler.contentType = "application/json";
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                alert.text = www.error;
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    // handle the result
                    var result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    if (result == "Registered")
                    {
                        alert.text = "Registered Succefully. Please Log in";
                        username.text = "";
                        email.text = "";
                        password.text = "";
                    } else if(result == "exist")
                    {
                        alert.text = "User already exists";
                    } else
                    {
                        alert.text = "Please insert a valid information!";
                    }
                }
                else
                {
                    //handle the problem
                    alert.text = "Error! data couldn't get.";
                }
            }
        }
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
                    if (result == "Logged In")
                    {
                        alert.text = "Welcome!";
                        PlayerPrefs.Save();
                        StartCoroutine(Open());
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
    public IEnumerator Open()
    {
        Vector3 LeftMove = new Vector3(-1, 0, 0);
        LeftDoorRigid.velocity = LeftMove * 3;
        Vector3 RightMove = new Vector3(1, 0, 0);
        RightDoorRigid.velocity = RightMove * 3;
        yield return new WaitForSeconds(1);
        Application.LoadLevel(GlobalConstant.LoadingScene);
    }
}

