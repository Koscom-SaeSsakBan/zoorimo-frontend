using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Globalization;


[System.Serializable]
class ResMyPage
{
    public string name;
    public string user_price;
    public string cur_price;
    public string profit_and_loss;
    public string yield_rate;
}

class ResMyPageList
{
    public ResMyPage[] stock_list;
}
public class HttpMyPage : MonoBehaviour
{

    float time;
    float checkTime;

    GameObject playerStatus;
    string user_pk;

    public string mypage_name;
    public string mypage_user_price;
    public string mypage_cur_price;
    public string mypage_profit_and_loss;
    public string mypage_yield_rate;


    public int idx;

// Start is called before the first frame update
    void Start()
        {
        time = 0.0f;
        checkTime = 2.0f;
        playerStatus = GameObject.Find("PlayerStatus");
        user_pk = playerStatus.GetComponent<StatusManagerStart>().user_pk;
        
        StartCoroutine(RequestMyPage());

        }


    IEnumerator RequestMyPage()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get("http://52.78.94.149:80/api/v1/users/" + user_pk + "/stock/status");

        yield return webRequest.SendWebRequest();

        if (webRequest.isNetworkError || webRequest.isHttpError)
        {
            Debug.Log(webRequest.error);
        }
        else
        {
            string result = webRequest.downloadHandler.text;
            // Debug.Log("주식리스트 불러오기 결과 :  " + result);

            ResMyPageList dList = JsonUtility.FromJson<ResMyPageList>(result);

            // ResMyPage d = JsonUtility.FromJson<ResMyPage>(result);
            // Debug.Log("list 길이"+dList.stock_list.Length);

            for(int i = 0; i < dList.stock_list.Length; i++)
            {
                // panel UI 가져와서 찍어주기
                ResMyPage d = dList.stock_list[i];
                mypage_name = d.name;
                mypage_user_price = d.user_price;
                mypage_cur_price = d.cur_price;

              
                mypage_profit_and_loss = d.profit_and_loss;
                mypage_yield_rate = d.yield_rate;

                GameObject p = GameObject.Find("p" + (i + 1).ToString());
                Text t1= p.transform.Find("T1").gameObject.transform.GetComponent<Text>();
                Text t2 = p.transform.Find("T2").gameObject.transform.GetComponent<Text>();
                Text t3 = p.transform.Find("T3").gameObject.transform.GetComponent<Text>();

                t1.text = mypage_name;

                NumberFormatInfo numberFormat = new CultureInfo("ko-KR", false).NumberFormat;

                t2.text = int.Parse(mypage_cur_price).ToString("c", numberFormat);


                t3.text = int.Parse(mypage_profit_and_loss).ToString("c", numberFormat) + "\n" + float.Parse(mypage_yield_rate)+"%";
                if (int.Parse(mypage_profit_and_loss) > 0)
                {
                    t3.color = Color.red;
                }
                else
                {
                    t3.color = Color.blue;
                }
            }   

        }

    }
    

    void Update()
    {     
          time += Time.deltaTime;  // 프레임당 시간
          if (time > checkTime)  // 경과 시간이 특정 시간이 보다 클 경우
          {
            time = 0.0f;

            StartCoroutine(RequestMyPage());

        }

    }

}
