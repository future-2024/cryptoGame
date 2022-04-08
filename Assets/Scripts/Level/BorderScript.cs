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
    }
}
