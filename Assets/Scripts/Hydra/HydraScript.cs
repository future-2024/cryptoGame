using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HydraScript : MonoBehaviour
{
    public Button level_1;
    public Button level_2;
    // Start is called before the first frame update
    void Start()
    {
        Button levelBut_1 = level_1.GetComponent<Button>();
        levelBut_1.onClick.AddListener(level1);
        
        Button levelBut_2 = level_2.GetComponent<Button>();
        levelBut_2.onClick.AddListener(level2);
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
