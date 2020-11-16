﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HttpSignUp : MonoBehaviour
{

    public InputField inputID; 
    public InputField inputPW;
    public InputField inputName;
    public InputField inputEmail;

    string id;
    string pw;
    string username;
    string email;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RequestHttp()
    {
        inputID = GameObject.Find("InputID").GetComponent<InputField>();
        inputPW = GameObject.Find("InputPW").GetComponent<InputField>();
        inputName = GameObject.Find("InputName").GetComponent<InputField>();
        inputEmail = GameObject.Find("InputEmail").GetComponent<InputField>();
        id = inputID.text;
        pw = inputPW.text;
        username = inputName.text;
        email = inputEmail.text;
        Debug.Log("ID: "+id);
        Debug.Log("PW: " + pw);
        Debug.Log("Name: " + username);
        Debug.Log("Email: " + email);
    }

    public void OnClickRequestBtn()
    {
        inputID = GameObject.Find("InputID").GetComponent<InputField>();
        inputPW = GameObject.Find("InputPW").GetComponent<InputField>();
        inputName = GameObject.Find("InputName").GetComponent<InputField>();
        inputEmail = GameObject.Find("InputEmail").GetComponent<InputField>();
        id = inputID.text;
        pw = inputPW.text;
        username = inputName.text;
        email = inputEmail.text;
        StartCoroutine(ReqeustSignUp());
    }

  
    IEnumerator ReqeustSignUp()    {
  
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();        
        formData.Add(new MultipartFormDataSection("name", username));
        formData.Add(new MultipartFormDataSection("username", id));
        formData.Add(new MultipartFormDataSection("input_password", pw));
        formData.Add(new MultipartFormDataSection("email", email));
        
        UnityWebRequest webRequest = UnityWebRequest.Post("http://15.164.165.118:8000/", formData);

        yield return webRequest.SendWebRequest();        

        if (webRequest.isNetworkError || webRequest.isHttpError)
        {
            Debug.Log(webRequest.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
            string result = webRequest.downloadHandler.text;
            Debug.Log("Response: " + result);
        }
        
    }

}
