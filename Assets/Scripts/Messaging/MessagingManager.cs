using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MessagingManager : MonoBehaviour
{
    public EventSystem eventSystem;        // �� ��ȭ �ܰ迡�� ������ ��ư�� �ĺ��� �� ����մϴ�.
    public ScrollRect scrollView;          // ��ȭ�� ��µ� �� ��ũ���� �Ʒ��� �����ֱ� ���� ���˴ϴ�.
    public GameObject DialogueUnit;        // ��ȭ�� �� ������ ��Ÿ���� �������Դϴ�. ������, �̸�, �������� ������ �ֽ��ϴ�.
    public RectTransform ContentView;      // ��ȭ�� ���⿡ �����˴ϴ�.
    public Button OP01, OP02, OP03;        // ��ȭ�� �� �ܰ躰 �������� �����ϴ� ��ư���Դϴ�.
    public Text NotificationBar;           // ��ȭ�� ����Ǿ��� ��, �̸� �˸��� �ؽ�Ʈ�Դϴ�.
    public Button EndButton;               // ��ȭ�� ����� ���Ŀ� ������ �����ϱ� ���� ��ư�Դϴ�.

    // ��ȭ�� �� �ܰ躰 �б������� ���ÿ� ���� �Ʒ� ���� �ٸ��� �ٲ�ϴ�.
    private int step;

    // ��ȭ�� ������ ���� ��ũ���� �Ʒ��� ������ �Լ��Դϴ�.
    // �Ʒ��� ���� �Լ��� �ڷ�ƾ�̶�� �θ��� �ڼ��� ������ ������ �������ּ���.
    // https://youtu.be/ahji9F5hJJ4
    IEnumerator ForceScrollDown()
    {
        yield return new WaitForEndOfFrame();
        scrollView.verticalNormalizedPosition = 0f;
    }

    // ��ȭ�� �� ������ ����ϴ� �Լ��Դϴ�.
    // Instantiate �Լ��� �̿��ؼ� ContentView�� �ڽ����� �����մϴ�.
    // ���� ��ũ���� �Ʒ��� �����ֱ� ���� �Լ��� ȣ���մϴ�.
    void printDialogueUnit(string speaker, string msg)
    {
        GameObject dialogueUnit = Instantiate(DialogueUnit, ContentView);
        dialogueUnit.transform.GetChild(2).GetComponent<Text>().text = msg;
        StartCoroutine(ForceScrollDown());
    }

    // ��ȭ�� �� �ܰ躰 �������� �������ִ� �Լ��Դϴ�.
    void setOptions(string optionOne, string optionTwo, string optionThree)
    {
        OP01.GetComponentInChildren<Text>().text = optionOne;
        OP02.GetComponentInChildren<Text>().text = optionTwo;
        OP03.GetComponentInChildren<Text>().text = optionThree;
    }

    // ��ȭ�� ��µǴ� ���� Ȥ�� ��ȭ�� ����� ���� ��ư�� ��Ȱ��ȭ �մϴ�.
    void disableOptions()
    {
        setOptions("", "", "");
        OP01.gameObject.SetActive(false);
        OP02.gameObject.SetActive(false);
        OP03.gameObject.SetActive(false);
    }

    // ��ȭ�� ��� ��µ� ���� ��Ȱ��ȭ �ߴ� ��ư�� �ٽ� Ȱ��ȭ �մϴ�.
    void enableOptions()
    {
        OP01.gameObject.SetActive(true);
        OP02.gameObject.SetActive(true);
        OP03.gameObject.SetActive(true);
    }

    // �� ó�� ���� �������� �������ݴϴ�.
    void Start()
    {
        printDialogueUnit("����", "�ϰ� ���� ���� ������?");
        setOptions("�̸�", "��", "ȸ��");
        step = 0;
    }

    // �� ��ȭ ������ 1���� ������ �ΰ� ����մϴ�.
    // �׷��� ���� �Ʒ� �Լ��� �ڷ�ƾ���� ���ǵ� �ֽ��ϴ�.
    IEnumerator printDialogue()
    {
        // ������ ��ư�� ��Ȱ��ȭ �� ����, � ��ư�� Ŭ���ƴ��� ã���ϴ�.
        disableOptions();
        Button clickedButton = eventSystem.currentSelectedGameObject.GetComponent<Button>();
        
        // ���� �б����� ���Ŀ� ���� ��µǴ� �޽����� �������� �޶����ϴ�.
        // �������� ���� �б���(step�� ��)�� �� �������־� ��ȭ�� �帧�� �������ָ� �˴ϴ�.
        switch (step)
        {
            case 0:
                // �� �б��������� ���� �ܰ迡�� ���õ� �ɼǿ� ���� �ٸ��� �����մϴ�.
                // ���ϴ� ��� OR ������(||)�� ���� ���� ������ �ǵ��� ���� �ֽ��ϴ�.
                if (Button.ReferenceEquals(clickedButton, OP01))
                {
                    // �޽����� ����� ���� 1�ʸ� ��ٸ��ϴ�.
                    printDialogueUnit("���", "�̸��� ������?");
                    yield return new WaitForSeconds(1);
                    printDialogueUnit("����", "�� �̸��� ����������.");
                    yield return new WaitForSeconds(1);
                    // �������� �������� �� ���� �б����� �������ݴϴ�.
                    setOptions("Ŀ��", "����", "����");
                    step = 1;
                }
                else if (Button.ReferenceEquals(clickedButton, OP02))
                {
                    printDialogueUnit("���", "���� ��𿡿�?");
                    yield return new WaitForSeconds(1);
                    printDialogueUnit("����", "�� ���� �� ��ó����.");
                    yield return new WaitForSeconds(1);
                    setOptions("Ŀ��", "����", "����");
                    step = 1;
                }
                else if (Button.ReferenceEquals(clickedButton, OP03))
                {
                    printDialogueUnit("���", "ȸ�簡 ��𿡿�?");
                    yield return new WaitForSeconds(1);
                    printDialogueUnit("����", "�� ȸ��� �� ��ó����.");
                    yield return new WaitForSeconds(1);
                    setOptions("����", "���", "���");
                    step = 2;
                }
                break;
            case 1:
                if (Button.ReferenceEquals(clickedButton, OP01))
                {
                    printDialogueUnit("���", "Ŀ�� ��Ƿ���?");
                    yield return new WaitForSeconds(1);
                    printDialogueUnit("����", "�ƴϿ�.");
                }
                else if (Button.ReferenceEquals(clickedButton, OP02))
                {
                    printDialogueUnit("���", "���� ��Ƿ���?");
                    yield return new WaitForSeconds(1);
                    printDialogueUnit("����", "�ƴϿ�.");
                }
                else if (Button.ReferenceEquals(clickedButton, OP03))
                {
                    printDialogueUnit("���", "���� ��Ƿ���?");
                    yield return new WaitForSeconds(1);
                    printDialogueUnit("����", "�ƴϿ�.");
                }
                // �� �̻� �̾����� ��ȭ�� ���ٸ� step�� ���� -1�� �����Ͽ� ��ȭ�� ����Ǿ����� ��Ÿ���ϴ�.
                step = -1;
                break;
            case 2:
                if (Button.ReferenceEquals(clickedButton, OP01))
                {
                    printDialogueUnit("���", "���� �ϽǷ���?");
                    yield return new WaitForSeconds(1);
                    printDialogueUnit("����", "�ƴϿ�.");
                }
                else if (Button.ReferenceEquals(clickedButton, OP02))
                {
                    printDialogueUnit("���", "��� �ϽǷ���?");
                    yield return new WaitForSeconds(1);
                    printDialogueUnit("����", "�ƴϿ�.");
                }
                else if (Button.ReferenceEquals(clickedButton, OP03))
                {
                    printDialogueUnit("���", "��� �ϽǷ���?");
                    yield return new WaitForSeconds(1);
                    printDialogueUnit("����", "��.");
                }
                step = -1;
                break;
        }
        // ��ȭ�� ��µ� ���Ŀ��� ��ư�� �ٽ� Ȱ��ȭ�մϴ�.
        enableOptions();

        // ��ȭ�� ����� ����Դϴ�.
        // ������ ��ư�� ��Ȱ��ȭ �� ��, ��ȭ ���� �˸��� ����մϴ�.
        // ������ �����ϴ� ��ư�� Ȱ��ȭ���� ������ ������ �� �ְڲ��մϴ�.
        if (step == -1)
        {
            disableOptions();
            yield return new WaitForSeconds(1);
            Text notificationBar = Instantiate(NotificationBar, ContentView);
            notificationBar.text = "��ȭ�� ����Ǿ����ϴ�.";
            StartCoroutine(ForceScrollDown());
            EndButton.gameObject.SetActive(true);
        }
    }

    // ������ ��ư�� Ŭ������ ��, ȣ��Ǵ� �Լ��Դϴ�. (�̷��� �Լ��� �ݹ��Լ���� �մϴ�)
    // �̷��� �Լ��� �ڷ�ƾ���� ���ǵ� �� ���� ������ ������ ������ �ڷ�ƾ �Լ��� ȣ�����ݴϴ�.
    // �ڷ�ƾ �Լ��� StartCoroutine �Լ��� ���� ȣ���ؾ� �մϴ�.
    public void onClick()
    {
        StartCoroutine(printDialogue());
    }

    // ���� ���� ��ư�� ������ ȣ��Ǵ� �Լ��Դϴ�.
    // �ٸ� �۾� ���� ������ �����մϴ�.
    public void exitGame()
    {
        Application.Quit();
    }
}
