using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BorderScript : MonoBehaviour
{
    public Button Exit;
    public GameObject Modal;
    public Text countText;
    int i = 3;
    bool flag = false;
    private object rectransform;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("countsText", 0, 1);
        Button btnExit = Exit.GetComponent<Button>();
        btnExit.onClick.AddListener(ExitApp);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            Modal.SetActive(true);
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            GameObject.Find("Direction/Up").gameObject.GetComponent<Toggle>().isOn = false;
            GameObject.Find("Direction/Right").gameObject.GetComponent<Toggle>().isOn = true;
            GameObject.Find("Direction/Down").gameObject.GetComponent<Toggle>().isOn = false;
            GameObject.Find("Direction/Left").gameObject.GetComponent<Toggle>().isOn = false;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            GameObject.Find("Direction/Up").gameObject.GetComponent<Toggle>().isOn = false;
            GameObject.Find("Direction/Right").gameObject.GetComponent<Toggle>().isOn = false;
            GameObject.Find("Direction/Down").gameObject.GetComponent<Toggle>().isOn = false;
            GameObject.Find("Direction/Left").gameObject.GetComponent<Toggle>().isOn = true;
        }
        else if (Input.GetAxis("Vertical") > 0)
        {
            GameObject.Find("Direction/Up").gameObject.GetComponent<Toggle>().isOn = true;
            GameObject.Find("Direction/Right").gameObject.GetComponent<Toggle>().isOn = false;
            GameObject.Find("Direction/Down").gameObject.GetComponent<Toggle>().isOn = false;
            GameObject.Find("Direction/Left").gameObject.GetComponent<Toggle>().isOn = false;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            GameObject.Find("Direction/Up").gameObject.GetComponent<Toggle>().isOn = false;
            GameObject.Find("Direction/Right").gameObject.GetComponent<Toggle>().isOn = false;
            GameObject.Find("Direction/Down").gameObject.GetComponent<Toggle>().isOn = true;
            GameObject.Find("Direction/Left").gameObject.GetComponent<Toggle>().isOn = false;
        }
        else 
        {
            GameObject.Find("Direction/Up").gameObject.GetComponent<Toggle>().isOn=false;
            GameObject.Find("Direction/Right").gameObject.GetComponent<Toggle>().isOn = false;
            GameObject.Find("Direction/Down").gameObject.GetComponent<Toggle>().isOn = false;
            GameObject.Find("Direction/Left").gameObject.GetComponent<Toggle>().isOn = false;
        }

        //MiniMap();
    }    
    void ExitApp()
    {
        Time.timeScale = 0;
        Modal.SetActive(true);
    }
    void countsText ()
    {
        if (i >= 0)
        {
            countText.text = i.ToString();
            i--;
        }
        else
        {
            flag = true;
        }
    }
    void MiniMap()
    {

        if (flag == true)
        {
            //Debug.Log(GameObject.Find("SpaceShip").gameObject.transform.position);
//            GameObject.Find("MINIMAP1/Icon_Target").gameObject.GetComponent<RectTransform>().anchoredPosition = Camera.main.ViewportToWorldPoint(GameObject.Find("SpaceShip").transform.position);
            //for (int enemy = 0; enemy < GameObject.FindGameObjectsWithTag("Enemy").Length; enemy++)
          //  {
                
          //  }
        }
    }
}
