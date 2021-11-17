using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagingManager : MonoBehaviour
{
    public Button CH01, CH02, CH03;

    private int step;

    void Start()
    {
        step = 0;
        CH01.GetComponentInChildren<Text>().text = "CH01";
        CH02.GetComponentInChildren<Text>().text = "CH02";
        CH03.GetComponentInChildren<Text>().text = "CH03";
    }

    public void onClick()
    {
        switch (step)
        {
            case 0:
                CH01.GetComponentInChildren<Text>().text = "CH11";
                CH02.GetComponentInChildren<Text>().text = "CH12";
                CH03.GetComponentInChildren<Text>().text = "CH13";
                break;
            case 1:
                CH01.GetComponentInChildren<Text>().text = "CH21";
                CH02.GetComponentInChildren<Text>().text = "CH22";
                CH03.GetComponentInChildren<Text>().text = "CH23";
                break;
            case 2:
                CH01.GetComponentInChildren<Text>().text = "CH31";
                CH02.GetComponentInChildren<Text>().text = "CH32";
                CH03.GetComponentInChildren<Text>().text = "CH33";
                break;
            case 3:
                CH01.GetComponentInChildren<Text>().text = "CH41";
                CH02.GetComponentInChildren<Text>().text = "CH42";
                CH03.GetComponentInChildren<Text>().text = "CH43";
                break;
        }
        step++;
    }
}
