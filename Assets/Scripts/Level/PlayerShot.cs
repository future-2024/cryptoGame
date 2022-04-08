using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json.Linq;


public class PlayerShot : MonoBehaviour {

    //variable for laser prefab
    private string laser;
    private string support;
    //public LaserShot shotScript;
    //delay between laser shots
    public float delayTime;
    //boolean, if laser can be spawned
    bool canShoot = true;
    public string bulletMode = "default";
    private string type = "player";

    //will be executed every frame
    void Start ()
    {

    }

    void OnEnable()
    {
        StartCoroutine(Bullet());
    }
    void Update () {

        //check if laser can be spawned and right mouse button is pressed
        if (canShoot && Input.GetMouseButton(1)) {

            //disable shooting check for next frame
            canShoot = false;
            
            if (bulletMode == "parallel")
            {
                Instantiate(Resources.Load(laser), transform.position, transform.rotation);
                Instantiate(Resources.Load(laser), transform.position + new Vector3(0.4f, 0, 0), transform.rotation);
                Instantiate(Resources.Load(laser), transform.position - new Vector3(0.4f, 0, 0), transform.rotation);
            }

            if (bulletMode == "default" || bulletMode == "through")
            {
                Instantiate(Resources.Load(laser), transform.position, transform.rotation);
            }

            if (bulletMode == "bulletpower" || bulletMode == "speed")
            {
                Instantiate(Resources.Load(support), transform.position, transform.rotation);
            }
            StartCoroutine(NoFire());
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag ==  "parallel")
        {
            bulletMode = other.gameObject.tag;
            Destroy(other.gameObject);
            StartCoroutine(FireAct());
        }
        else if (other.gameObject.tag == "through")
        {
            bulletMode = other.gameObject.tag;
            Destroy(other.gameObject);
            StartCoroutine(FireAct());

        }
        else if (other.gameObject.tag == "bulletpower")
        {
            bulletMode = other.gameObject.tag;
            Destroy(other.gameObject);
            StartCoroutine(FireAct());

        }
        else if (other.gameObject.tag == "speed")
        {
            bulletMode = other.gameObject.tag;
            Destroy(other.gameObject);
            StartCoroutine(FireAct());

        }
    }

    //coroutine function, we can pause it
    IEnumerator NoFire () {
        yield return new WaitForSeconds (delayTime);
        canShoot = true;
    }
    IEnumerator FireAct () { 
        yield return new WaitForSeconds(10);
        bulletMode = "default";
    }
    public IEnumerator Bullet()
    {
         UnityWebRequest www = UnityWebRequest.Get(GlobalConstant.apiURL + "/bullet?tt=player");
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
                    laser = (string)tempArray["first"];
                    support = (string)tempArray["sec"];
                }
                else
                {
                    Debug.Log("Error! data couldn't get.");
                }
            }
        
    }
}