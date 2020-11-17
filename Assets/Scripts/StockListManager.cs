using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
public class StockListManager : MonoBehaviour
{
    public GameObject p;
    public GameObject p2;
    public InputField inputStockNum;
    public InputField inputStockAvgPrice;
    GameObject playerStatus;
    public string inputStockNumText;
    public string inputStockAvgPriceText;
    string user_pk;
    public string stockBtnName;
    public int idx;
    
    public string[] stock_name_list= { "삼성전자", "SK하이닉스", "삼성바이오로직스", "LG화학", 
                                        "NAVER", "셀트리온", "현대차","삼성SDI",
                                        "카카오","LG생활건강","기아차","현대모비스",
                                        "삼성물산","POSCO","KB금융","SK텔레콤",
                                        "엔씨소프트","신한지주","SK","SK이노베이션"};
    
    public string[] stock_code_list = { "005930", "000660", "207940","051910",
                                        "035420","068270","005380","006400",
                                        "035720","051900","000270","012330",
                                        "028260","005490","105560","017670",
                                        "036570","055550","034730","096770"};

    public int yValue;

    void Start()
    {
        playerStatus = GameObject.Find("PlayerStatus");

        user_pk = playerStatus.GetComponent<StatusManagerStart>().user_pk;

        p.SetActive(false);
        for (int i =0; i<20; i++)
        {

            ScrollRect scrollRect = GameObject.Find("Scroll View").GetComponent<ScrollRect>();
            Button btn = scrollRect.content.GetChild(i).GetComponent<Button>();
            // string btn_name = "Btn" + (i + 1).ToString();
            Text btn_txt = btn.transform.Find("Text").GetComponent<Text>();
            btn_txt.text = stock_name_list[i];
            yValue = 1;
            
        }
    }


    public void OnClickBtn()
    {
        if (!p.activeSelf)
        {
            string temp = EventSystem.current.currentSelectedGameObject.name;
            idx = int.Parse(temp) - 1;
            stockBtnName = stock_name_list[idx];
            inputStockNum = p.transform.Find("InputStockNum").gameObject.GetComponent<InputField>();
            inputStockAvgPrice = p.transform.Find("InputStockAvgPrice").gameObject.GetComponent<InputField>();
            Text txt = inputStockNum.transform.Find("Text").GetComponent<Text>();
            Text txt2 = inputStockAvgPrice.transform.Find("Text").GetComponent<Text>();
            txt.text = "";
            txt2.text = "";


            p.SetActive(true);
        }
    }
    public void removePannel()
    {
        if (p.activeSelf)
        {
            p.SetActive(false);
        }
    }
    public void OnClickSavePanelBtn()
    {
        if(yValue > 5)
        {
            return;
        }

        inputStockNum = p.transform.Find("InputStockNum").gameObject.GetComponent<InputField>();
        inputStockAvgPrice = p.transform.Find("InputStockAvgPrice").gameObject.GetComponent<InputField>();


        inputStockNumText = inputStockNum.text;
        inputStockAvgPriceText = inputStockAvgPrice.text;
        string str = stockBtnName + " 주식을\n평균단가 " + inputStockAvgPriceText + "원에 " + inputStockNumText + "주 보유";
        Debug.Log(str);

        p.SetActive(false);
        GameObject gobj = p2.transform.Find("Text" + yValue++.ToString()).gameObject;
        gobj.transform.GetComponent<Text>().text = str;




        StartCoroutine(RegisterStock());
        //p2.AddComponent




    }

    IEnumerator RegisterStock()
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        string formStr = stock_code_list[idx] + ":" + inputStockAvgPriceText + ":" + inputStockNumText;

        Debug.Log("주식저장 폼데이터: "+ formStr);
        formData.Add(new MultipartFormDataSection("stock_list", formStr));

        UnityWebRequest webRequest = UnityWebRequest.Post("http://52.78.94.149:80/api/v1/users/" + user_pk + "/stock/register", formData);

        yield return webRequest.SendWebRequest();

        if (webRequest.isNetworkError || webRequest.isHttpError)
        {
            Debug.Log(webRequest.error);
        }
        else
        {
            string result = webRequest.downloadHandler.text;
            Debug.Log("주식저장 결과" + result);


        }

    }

}
