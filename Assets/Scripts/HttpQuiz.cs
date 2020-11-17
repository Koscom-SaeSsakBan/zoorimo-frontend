using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;


[System.Serializable]
class ResQuizData
{
    public string id;
    public string question;
    public string answer;
    public string solution;
}
public class HttpQuiz : MonoBehaviour
{
    GameObject playerStatus;
    string user_pk;
    string quiz_index;

    public GameObject p;

    string quiz_question;
    string quiz_answer;
    string quiz_solution;

    public InputField Input_quiz_user_answer;
    string quiz_user_answer;
    string quiz_result;

    GameObject questionText;
    GameObject test;
    GameObject resultText;
    GameObject solutionText;
    // Start is called before the first frame update
    void Start()
    {
        p.SetActive(false);

        playerStatus = GameObject.Find("PlayerStatus");
        questionText = GameObject.Find("TextProblem");

        resultText = p.transform.Find("TextResult").gameObject;
        solutionText= p.transform.Find("TextSolution").gameObject;
        test = GameObject.Find("TextTest");
        user_pk = playerStatus.GetComponent<StatusManagerStart>().user_pk;
        Debug.Log("QUIZ REQUEST TEST user_pk :   " + user_pk);

        quiz_index = Random.Range(1, 10).ToString();
        StartCoroutine(ReqeustQuiz());


    }

    IEnumerator ReqeustQuiz()
    {
       UnityWebRequest webRequest = UnityWebRequest.Get("http://52.78.94.149:80/api/v1/users/" + user_pk + "/quiz/"+quiz_index);

        yield return webRequest.SendWebRequest();

        if (webRequest.isNetworkError || webRequest.isHttpError)
        {
            Debug.Log(webRequest.error);
        }
        else
        {
            string result = webRequest.downloadHandler.text;
            Debug.Log("퀴즈 결과: " + result);
            ResQuizData d = JsonUtility.FromJson<ResQuizData>(result);
            quiz_question = d.question;
            quiz_answer = d.answer;
            quiz_solution = d.solution;

            Debug.Log("문제의 정답은 : "+quiz_answer);

            test.transform.GetComponent<Text>().text = quiz_question;
            // questionText.transform.GetComponent<TextMeshPro>().text = quiz_question;


        }

    }
    public void GoSolution()
    {
        Input_quiz_user_answer = GameObject.Find("InputAnswer").GetComponent<InputField>();
        Debug.Log(Input_quiz_user_answer.text);
        quiz_user_answer = Input_quiz_user_answer.text;
        if (quiz_user_answer == quiz_answer) {
            quiz_result = "정답입니다";
            StartCoroutine(ReqeustQuizResult());
        } 
        else quiz_result = "틀렸습니다";
        
        if (!p.activeSelf)
        {
            resultText.transform.GetComponent<Text>().text = quiz_result;
            solutionText.transform.GetComponent<Text>().text = quiz_solution;
            p.SetActive(true);
            
        }

    }

    IEnumerator ReqeustQuizResult()
    {
       
        UnityWebRequest webRequest = UnityWebRequest.Get("http://52.78.94.149:80/api/v1/users/" + user_pk + "/quiz/true");

        yield return webRequest.SendWebRequest();

        if (webRequest.isNetworkError || webRequest.isHttpError)
        {
            Debug.Log(webRequest.error);
        }
        else
        {
            string result = webRequest.downloadHandler.text;
            Debug.Log("퀴즈 결과 전송 완료 !");  

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
