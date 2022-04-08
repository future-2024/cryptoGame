using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CentaurusScript : MonoBehaviour
{
    public Button level_1;
    // Start is called before the first frame update
    void Start()
    {
        Button levelBut_1 = level_1.GetComponent<Button>();
        levelBut_1.onClick.AddListener(level1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void level1()
    {
        PlayerPrefs.SetString("level", "1");
        Application.LoadLevel(GlobalConstant.level2_1);
    }
}
