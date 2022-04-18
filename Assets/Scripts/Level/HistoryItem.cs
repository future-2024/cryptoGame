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
public class HistoryEvent
{
    public LandVariable[] events;
}


[System.Serializable]
public class LevelItem
{
    public string userName;
    public string land;
    public string level;
    public int starCnt;
}
public class LevelEvent
{
    public LevelItem[] events;
}


[System.Serializable]
public class UserItem
{
    public string userName;
}
[System.Serializable]
public class LandVariable
{
    public bool show = false;
    public int cntLevel = 0;
}