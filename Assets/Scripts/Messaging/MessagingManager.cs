using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagingManager : MonoBehaviour
{
    [SerializeField] public Scrollbar scrollbar;         // 스크롤바
    [SerializeField] public ScrollRect scrollView;       // 스크롤뷰
    [SerializeField] public GameObject DialogueUnit;     // 대화를 출력할 때 쓸 프리팹
    [SerializeField] public RectTransform ContentView;   // 대화를 출력할 레이아웃
    [SerializeField] public Button OP01, OP02, OP03;     // 대화의 단계별 선택지

    private int step;    // 현재 대화의 단계를 알려주는 값

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

    // 첫 번째 옵션을 눌렸을 때
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

    // 두 번째 옵션을 눌렸을 때
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

    // 세 번째 옵션을 눌렸을 때
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
