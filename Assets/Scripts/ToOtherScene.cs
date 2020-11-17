using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToOtherScene : MonoBehaviour
{
   // public GameObject gameSceneStatus;

   // public StatusManagerGame smg;

  //  public GameObject gameSceneManager;

   // public GameManager gm;

    GameObject playerStatus;

    void Start()
    {
        playerStatus = GameObject.Find("PlayerStatus");

        string user_pk = playerStatus.GetComponent<StatusManagerStart>().user_pk;
        Debug.Log(SceneManager.GetActiveScene().name +" / user_pk:  " + user_pk);
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoBack()
    {
        SceneManager.LoadScene("GameScene");
        //DontDestroyOnLoad(playerStatus);
    }
    public void GoSolution()
    {
        SceneManager.LoadScene("QuizSolutionScene");
        //DontDestroyOnLoad(playerStatus);
    }

    public void GoFeedQuiz()
    {

        SceneManager.LoadScene("FeedQuizScene");
        DontDestroyOnLoad(playerStatus);
/*        gm = gameSceneManager.GetComponent<GameManager>();
        string user_pk = gm.user_pk;

        smg = gameSceneStatus.GetComponent<StatusManagerGame>();
        smg.user_pk = user_pk;
        smg.CallFeedQuiz();*/
    }
    public void GoMyPage()
    {

        SceneManager.LoadScene("MyPageScene");
        DontDestroyOnLoad(playerStatus);
        /*gm = gameSceneManager.GetComponent<GameManager>();
        string user_pk = gm.user_pk;

        smg = gameSceneStatus.GetComponent<StatusManagerGame>();
        smg.user_pk = user_pk;
        smg.CallMyPage();*/
    }

    public void GoStock()
    {
        SceneManager.LoadScene("StockScene");
        DontDestroyOnLoad(playerStatus);
    }
}
