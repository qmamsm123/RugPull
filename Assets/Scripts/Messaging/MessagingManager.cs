using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MessagingManager : MonoBehaviour
{
    public EventSystem eventSystem;        // 각 대화 단계에서 선택한 버튼을 식별할 때 사용합니다.
    public ScrollRect scrollView;          // 대화가 출력될 때 스크롤을 아래로 내려주기 위해 사용됩니다.
    public GameObject DialogueUnit;        // 대화의 한 단위를 나타내는 프리팹입니다. 프로필, 이름, 내용으로 구성돼 있습니다.
    public RectTransform ContentView;      // 대화가 여기에 생성됩니다.
    public Button OP01, OP02, OP03;        // 대화의 각 단계별 선택지를 제공하는 버튼들입니다.
    public Text NotificationBar;           // 대화가 종료되었을 때, 이를 알리는 텍스트입니다.
    public Button EndButton;               // 대화가 종료된 이후에 게임을 종료하기 위한 버튼입니다.

    // 대화의 각 단계별 분기점에서 선택에 따라 아래 값이 다르게 바뀝니다.
    private int step;

    // 대화가 생성된 이후 스크롤을 아래로 내리는 함수입니다.
    // 아래와 같은 함수를 코루틴이라고 부르며 자세한 내용은 다음을 참고해주세요.
    // https://youtu.be/ahji9F5hJJ4
    IEnumerator ForceScrollDown()
    {
        yield return new WaitForEndOfFrame();
        scrollView.verticalNormalizedPosition = 0f;
    }

    // 대화의 한 단위를 출력하는 함수입니다.
    // Instantiate 함수를 이용해서 ContentView의 자식으로 생성합니다.
    // 이후 스크롤을 아래로 내려주기 위한 함수를 호출합니다.
    void printDialogueUnit(string speaker, string msg)
    {
        GameObject dialogueUnit = Instantiate(DialogueUnit, ContentView);
        dialogueUnit.transform.GetChild(2).GetComponent<Text>().text = msg;
        StartCoroutine(ForceScrollDown());
    }

    // 대화의 각 단계별 선택지를 지정해주는 함수입니다.
    void setOptions(string optionOne, string optionTwo, string optionThree)
    {
        OP01.GetComponentInChildren<Text>().text = optionOne;
        OP02.GetComponentInChildren<Text>().text = optionTwo;
        OP03.GetComponentInChildren<Text>().text = optionThree;
    }

    // 대화가 출력되는 동안 혹은 대화가 종료된 이후 버튼을 비활성화 합니다.
    void disableOptions()
    {
        setOptions("", "", "");
        OP01.gameObject.SetActive(false);
        OP02.gameObject.SetActive(false);
        OP03.gameObject.SetActive(false);
    }

    // 대화가 모두 출력된 이후 비활성화 했던 버튼을 다시 활성화 합니다.
    void enableOptions()
    {
        OP01.gameObject.SetActive(true);
        OP02.gameObject.SetActive(true);
        OP03.gameObject.SetActive(true);
    }

    // 맨 처음 대사와 선택지를 설정해줍니다.
    void Start()
    {
        printDialogueUnit("상대방", "하고 싶은 말이 뭐에요?");
        setOptions("이름", "집", "회사");
        step = 0;
    }

    // 각 대화 단위는 1초의 간격을 두고 출력합니다.
    // 그러기 위해 아래 함수는 코루틴으로 정의돼 있습니다.
    IEnumerator printDialogue()
    {
        // 선택지 버튼을 비활성화 한 이후, 어떤 버튼이 클릭됐는지 찾습니다.
        disableOptions();
        Button clickedButton = eventSystem.currentSelectedGameObject.GetComponent<Button>();
        
        // 현재 분기점이 어디냐에 따라 출력되는 메시지와 선택지가 달라집니다.
        // 선택지에 따라 분기점(step의 값)을 잘 설정해주어 대화의 흐름을 형성해주면 됩니다.
        switch (step)
        {
            case 0:
                // 각 분기점에서는 이전 단계에서 선택된 옵션에 따라 다르게 동작합니다.
                // 원하는 경우 OR 연산자(||)를 통해 같은 동작을 의도할 수도 있습니다.
                if (Button.ReferenceEquals(clickedButton, OP01))
                {
                    // 메시지를 출력한 이후 1초를 기다립니다.
                    printDialogueUnit("당신", "이름이 뭐에요?");
                    yield return new WaitForSeconds(1);
                    printDialogueUnit("상대방", "제 이름은 ㅇㅇㅇ에요.");
                    yield return new WaitForSeconds(1);
                    // 선택지를 설정해준 후 다음 분기점을 지정해줍니다.
                    setOptions("커피", "점심", "저녁");
                    step = 1;
                }
                else if (Button.ReferenceEquals(clickedButton, OP02))
                {
                    printDialogueUnit("당신", "집이 어디에요?");
                    yield return new WaitForSeconds(1);
                    printDialogueUnit("상대방", "제 집은 이 근처에요.");
                    yield return new WaitForSeconds(1);
                    setOptions("커피", "점심", "저녁");
                    step = 1;
                }
                else if (Button.ReferenceEquals(clickedButton, OP03))
                {
                    printDialogueUnit("당신", "회사가 어디에요?");
                    yield return new WaitForSeconds(1);
                    printDialogueUnit("상대방", "제 회사는 이 근처에요.");
                    yield return new WaitForSeconds(1);
                    setOptions("이직", "퇴사", "퇴근");
                    step = 2;
                }
                break;
            case 1:
                if (Button.ReferenceEquals(clickedButton, OP01))
                {
                    printDialogueUnit("당신", "커피 드실래요?");
                    yield return new WaitForSeconds(1);
                    printDialogueUnit("상대방", "아니요.");
                }
                else if (Button.ReferenceEquals(clickedButton, OP02))
                {
                    printDialogueUnit("당신", "점심 드실래요?");
                    yield return new WaitForSeconds(1);
                    printDialogueUnit("상대방", "아니요.");
                }
                else if (Button.ReferenceEquals(clickedButton, OP03))
                {
                    printDialogueUnit("당신", "저녁 드실래요?");
                    yield return new WaitForSeconds(1);
                    printDialogueUnit("상대방", "아니요.");
                }
                // 더 이상 이어지는 대화가 없다면 step의 값이 -1을 저장하여 대화가 종료되었음을 나타냅니다.
                step = -1;
                break;
            case 2:
                if (Button.ReferenceEquals(clickedButton, OP01))
                {
                    printDialogueUnit("당신", "이직 하실래요?");
                    yield return new WaitForSeconds(1);
                    printDialogueUnit("상대방", "아니요.");
                }
                else if (Button.ReferenceEquals(clickedButton, OP02))
                {
                    printDialogueUnit("당신", "퇴사 하실래요?");
                    yield return new WaitForSeconds(1);
                    printDialogueUnit("상대방", "아니요.");
                }
                else if (Button.ReferenceEquals(clickedButton, OP03))
                {
                    printDialogueUnit("당신", "퇴근 하실래요?");
                    yield return new WaitForSeconds(1);
                    printDialogueUnit("상대방", "네.");
                }
                step = -1;
                break;
        }
        // 대화가 출력된 이후에는 버튼을 다시 활성화합니다.
        enableOptions();

        // 대화가 종료된 경우입니다.
        // 선택지 버튼을 비활성화 한 뒤, 대화 종료 알림을 출력합니다.
        // 게임을 종료하는 버튼을 활성화시켜 게임을 종료할 수 있겠끔합니다.
        if (step == -1)
        {
            disableOptions();
            yield return new WaitForSeconds(1);
            Text notificationBar = Instantiate(NotificationBar, ContentView);
            notificationBar.text = "대화가 종료되었습니다.";
            StartCoroutine(ForceScrollDown());
            EndButton.gameObject.SetActive(true);
        }
    }

    // 선택지 버튼을 클릭했을 때, 호출되는 함수입니다. (이러한 함수를 콜백함수라고 합니다)
    // 이러한 함수는 코루틴으로 정의될 수 없기 떄문에 별도로 정의한 코루틴 함수를 호출해줍니다.
    // 코루틴 함수는 StartCoroutine 함수를 통해 호출해야 합니다.
    public void onClick()
    {
        StartCoroutine(printDialogue());
    }

    // 게임 종료 버튼을 누르면 호출되는 함수입니다.
    // 다른 작업 없이 게임을 종료합니다.
    public void exitGame()
    {
        Application.Quit();
    }
}
