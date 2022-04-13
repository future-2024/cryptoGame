using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json.Linq;
using UnityEngine.Networking;

[System.Serializable]
public class HistoryItem
{
    public string userName;
    public string land;
    public string level;
    public int time;
    public int hp;
    public int itemCnt;
}

[System.Serializable]
public class UserItem
{
    public string userName;
}