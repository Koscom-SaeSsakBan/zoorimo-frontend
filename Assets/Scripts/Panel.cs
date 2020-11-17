using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{

    public GameObject p;

    public void Start()
    {
        p.SetActive(false);

    }
    public void Show()
    {
        transform.gameObject.SetActive(true);
    
    }

    public void panelclick()
    {
        Debug.Log(" 클릭");
        if (!p.activeSelf)
        {
            p.SetActive(true);

        }
        else
        {
            p.SetActive(false);
        }

    }



}
