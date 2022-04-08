using UnityEngine;
using System.Collections;

public class TimeDestroyer : MonoBehaviour
{

    //variable for time befor gameobject will be destroyed
    public float timeToDestroy;

    //will be executed once at start
    void Start()
    {
        //delete gameobject from the scene
        Destroy(gameObject, timeToDestroy);
    }
}
