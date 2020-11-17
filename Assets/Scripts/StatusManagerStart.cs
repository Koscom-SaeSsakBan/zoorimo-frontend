using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatusManagerStart : MonoBehaviour
{
    public string user_pk;
    public GameObject playerStatus;
    public void Call()
    {
        Debug.Log("Call !");
        SceneManager.LoadScene("GameScene");
        DontDestroyOnLoad(playerStatus);
    }
}