using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPageManager : MonoBehaviour
{
    GameObject gameSceneStatus;
    string user_pk;
    // Start is called before the first frame update
    void Start()
    {
        gameSceneStatus = GameObject.Find("GameSceneStatus");
       // user_pk = gameSceneStatus.GetComponent<StatusManagerGame>().user_pk;
        Debug.Log("My Page user_pk:  " + user_pk);
    }
}
