using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
class ResZoorimoData
{
    public string status;
    public string size;
}
public class HttpZoorimoStatus : MonoBehaviour
{
    float time;
    float checkTime;
    GameObject playerStatus;
    string user_pk;

    public GameObject zoorimo_2;
    public GameObject zoorimo_1;
    public GameObject zoorimo0;
    public GameObject zoorimo1;
    public GameObject zoorimo2;

    // Start is called before the first frame update
    void Start()
    {
        /*
        zoorimo_2 = GameObject.Find("zoorimo_-2").GetComponent<GameObject>();
        zoorimo_1 = GameObject.Find("zoorimo_-1").GetComponent<GameObject>();
        zoorimo0 = GameObject.Find("zoorimo_0").GetComponent<GameObject>();
        zoorimo1 = GameObject.Find("zoorimo_1").GetComponent<GameObject>();
        zoorimo2 = GameObject.Find("zoorimo_2").GetComponent<GameObject>();
        */

        zoorimo_2.SetActive(false);
        zoorimo_1.SetActive(false);
        zoorimo0.SetActive(false);
        zoorimo1.SetActive(false);
        zoorimo2.SetActive(false);

        time = 0.0f;
        checkTime = 2.0f;
        playerStatus = GameObject.Find("PlayerStatus");

        user_pk = playerStatus.GetComponent<StatusManagerStart>().user_pk;

        StartCoroutine(ReqeustZoorimoStatus());

    }

    IEnumerator ReqeustZoorimoStatus()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get("http://52.78.94.149:80/api/v1/users/" + user_pk+"/status/");
        
        yield return webRequest.SendWebRequest();
    
        if (webRequest.isNetworkError || webRequest.isHttpError)
        {
            Debug.Log(webRequest.error);
        }
        else
        {
            string result = webRequest.downloadHandler.text;
           // Debug.Log("주리모상태: " + result); 
            ResZoorimoData d = JsonUtility.FromJson<ResZoorimoData>(result);
            Debug.Log("주리모 Status: " + d.status);
            Debug.Log("주리모 사이즈 : " + d.size);


            switch(d.status){
                case "-2":
                    {
                        zoorimo_2.SetActive(true);
                        zoorimo_1.SetActive(false);
                        zoorimo0.SetActive(false);
                        zoorimo1.SetActive(false);
                        zoorimo2.SetActive(false);
                        Vector2 spriteSize = zoorimo_2.GetComponent<SpriteRenderer>().sprite.rect.size;
                        spriteSize.x = 40 * float.Parse(d.size);
                        spriteSize.y = 40 * float.Parse(d.size);

                        zoorimo_2.GetComponent<SpriteRenderer>().transform.localScale = spriteSize;
                        zoorimo_2.transform.position = new Vector3(651, -361, 0);
                        Debug.Log("주리모크기: " + spriteSize.x);

                        break;
                    }
                case "-1":
                    {
                        zoorimo_2.SetActive(false);
                        zoorimo_1.SetActive(true);
                        zoorimo0.SetActive(false);
                        zoorimo1.SetActive(false);
                        zoorimo2.SetActive(false);
                        Vector2 spriteSize = zoorimo_1.GetComponent<SpriteRenderer>().sprite.rect.size;
                        spriteSize.x = 40 * float.Parse(d.size);
                        spriteSize.y = 40 * float.Parse(d.size);

                        zoorimo_1.GetComponent<SpriteRenderer>().transform.localScale = spriteSize;
                        zoorimo_1.transform.position = new Vector3(651, -361, 0);


                        Debug.Log("주리모크기: " + spriteSize.x);
                        break;
                    }
                case "0":
                    {
                        zoorimo_2.SetActive(false);
                        zoorimo_1.SetActive(false);
                        zoorimo0.SetActive(true);
                        zoorimo1.SetActive(false);
                        zoorimo2.SetActive(false);
                        Vector2 spriteSize = zoorimo0.GetComponent<SpriteRenderer>().sprite.rect.size;
                        spriteSize.x = 40 * float.Parse(d.size);
                        spriteSize.y = 40 * float.Parse(d.size);

                        zoorimo0.GetComponent<SpriteRenderer>().transform.localScale = spriteSize;
                        zoorimo0.transform.position= new Vector3(651, -361, 0);

                        Debug.Log("주리모크기: " + spriteSize.x);
                        break;
                    }
                case "1":
                    {
                        zoorimo_2.SetActive(false);
                        zoorimo_1.SetActive(false);
                        zoorimo0.SetActive(false);
                        zoorimo1.SetActive(true);
                        zoorimo2.SetActive(false);
                        Vector2 spriteSize = zoorimo1.GetComponent<SpriteRenderer>().sprite.rect.size;
                        spriteSize.x = 40 * float.Parse(d.size);
                        spriteSize.y = 40 * float.Parse(d.size);

                        zoorimo1.GetComponent<SpriteRenderer>().transform.localScale = spriteSize;
                        zoorimo1.transform.position = new Vector3(651, -361, 0);

                        Debug.Log("주리모크기: " + spriteSize.x);
                        break;
                    }
                case "2":
                    {
                        zoorimo_2.SetActive(false);
                        zoorimo_1.SetActive(false);
                        zoorimo0.SetActive(false);
                        zoorimo1.SetActive(false);
                        zoorimo2.SetActive(true);

                        Vector2 spriteSize = zoorimo2.GetComponent<SpriteRenderer>().sprite.rect.size;
                        spriteSize.x= 40 * float.Parse(d.size);
                        spriteSize.y= 40 * float.Parse(d.size);

                        zoorimo2.GetComponent<SpriteRenderer>().transform.localScale = spriteSize;
                        zoorimo2.transform.position = new Vector3(651, -361, 0);


                        Debug.Log("주리모크기: " +spriteSize.x);
                        break;
                    }


            }

            // status (-2  ~ -1 )
            // 사이즈에 따른 크기조절 
        }

    }

    // Update is called once per frame
    void Update()
    {
        //사이즈
        time += Time.deltaTime;  // 프레임당 시간
        if (time > checkTime)  // 경과 시간이 특정 시간이 보다 클 경우
        {
            time = 0.0f;

            StartCoroutine(ReqeustZoorimoStatus());

        }



        // 주리모 실시간 상태 업데이트를 버튼으로 할지 아니면 백그라운드 시간마다 리퀘스트 
        // 주리모 상태에따른 이미지 보여주기 구현
    }
}
