using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;


public class signUp : MonoBehaviour
{
    public GameObject sign;

    public InputField username;
    public InputField email;
    public InputField password;
    
    public Text alert;

    public Button login;

    public string url;
    public string url2;
    Rigidbody2D LeftDoorRigid;
    Rigidbody2D RightDoorRigid;
    

    void Start()
    {
        Button signBut = sign.GetComponent<Button>();
        signBut.onClick.AddListener(SignUp);

        Button loginBtn = login.GetComponent<Button>();
        loginBtn.onClick.AddListener(loginFun);

        url = GlobalConstant.apiURL + "/users";
        url2 = GlobalConstant.apiURL + "/login";
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
    void loginFun()
    {
        gameObject.SetActive(false);
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
                    alert.text = result;
                    if (result == "Registered")
                    {                        
                        username.text = "";
                        email.text = "";
                        password.text = "";
                        gameObject.SetActive(false);
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

