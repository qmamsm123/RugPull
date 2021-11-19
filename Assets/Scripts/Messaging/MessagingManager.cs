using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagingManager : MonoBehaviour
{
    [SerializeField] public Scrollbar scrollbar;         // ��ũ�ѹ�
    [SerializeField] public ScrollRect scrollView;       // ��ũ�Ѻ�
    [SerializeField] public GameObject DialogueUnit;     // ��ȭ�� ����� �� �� ������
    [SerializeField] public RectTransform ContentView;   // ��ȭ�� ����� ���̾ƿ�
    [SerializeField] public Button OP01, OP02, OP03;     // ��ȭ�� �ܰ躰 ������

    private int step;    // ���� ��ȭ�� �ܰ踦 �˷��ִ� ��

    IEnumerator ForceScrollDown()
    {
        yield return new WaitForEndOfFrame();
        scrollView.verticalNormalizedPosition = 0f;
    }

    void printDialogue(string speaker, string msg)
    {
        GameObject dialogueUnit = Instantiate(DialogueUnit, ContentView);
        dialogueUnit.transform.GetChild(1).GetComponent<Text>().text = speaker;
        dialogueUnit.transform.GetChild(2).GetComponent<Text>().text = msg;
        StartCoroutine(ForceScrollDown());
    }
    
    void setOptions(string optionOne, string optionTwo, string optionThree)
    {
        OP01.GetComponentInChildren<Text>().text = "CH01";
        OP02.GetComponentInChildren<Text>().text = "CH02";
        OP03.GetComponentInChildren<Text>().text = "CH03";
    }

    void Start()
    {
        step = 0;
        setOptions("CH01", "CH02", "CH03");
    }

    // ù ��° �ɼ��� ������ ��
    public void onClickOptionOne()
    {
        if (step > 3)
            step = 0;
        switch (step)
        {
            case 0:
                printDialogue("A", "OptionOne");
                break;
            case 1:
                printDialogue("A", "OptionOne");
                break;
            case 2:
                printDialogue("A", "OptionOne");
                break;
            case 3:
                printDialogue("A", "OptionOne");
                break;
        }
        step++;
    }

    // �� ��° �ɼ��� ������ ��
    public void onClickOptionTwo()
    {
        if (step > 3)
            step = 0;
        switch (step)
        {
            case 0:
                printDialogue("B", "OptionTwo");
                break;
            case 1:
                printDialogue("B", "OptionTwo");
                break;
            case 2:
                printDialogue("B", "OptionTwo");
                break;
            case 3:
                printDialogue("B", "OptionTwo");
                break;
        }
        step++;
    }

    // �� ��° �ɼ��� ������ ��
    public void onClickOptionThree()
    {
        if (step > 3)
            step = 0;
        switch (step)
        {
            case 0:
                printDialogue("C", "OptionThree");
                break;
            case 1:
                printDialogue("C", "OptionThree");
                break;
            case 2:
                printDialogue("C", "OptionThree");
                break;
            case 3:
                printDialogue("C", "OptionThree");
                break;
        }
        step++;
    }
}
