using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// ���� : ���� ������
// -> ���� ��ü�� ���¸� �˸���, ���۰� ���� �ؽ�Ʈ�� ��Ÿ����.

public enum GameState
{
    Ready,          // �غ�
    Start,           // ����
    Over,           // ����
    Pause,
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // ������ ���´� ó���� �غ� ����
    public GameState state { get; private set; } = GameState.Ready;

    public TextMeshProUGUI StateTextUI;

    public Color ReadyStateColor;
    public Color StartStateColor;
    public Color OverStateColor;

    public void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(Start_Coroutine());
    }

    private IEnumerator Start_Coroutine()
    {
        // ���� ����
        // 1. ���� �غ� ����
        state = GameState.Ready;
        StateTextUI.gameObject.SetActive(true);
        Refresh();

        // 2. 1.6�� �Ŀ� ���� ���� ����
        yield return new WaitForSeconds(1.6f);
        state = GameState.Start;
        Refresh();

        // 3. 0.4�� �Ŀ� �ؽ�Ʈ �������
        yield return new WaitForSeconds(0.4f);
        StateTextUI.gameObject.SetActive(false);
    }

    // 4. �÷��̸� �ϴٰ� 
    // 5. �÷��̾� ü���� 0 �� �Ǹ� "���� ����" ����
    public void GameOver()
    {
        state = GameState.Over;
        StateTextUI.gameObject.SetActive(true);
        Refresh();
    }

    public void Refresh()
    {
        switch (state)
        {
            case GameState.Ready:
            {
                StateTextUI.text = "Ready . . .";
                StateTextUI.color = ReadyStateColor;
                
                break;
            }
            case GameState.Start:
            {
                StateTextUI.text = "Start ~";
                StateTextUI.color = StartStateColor;
                

                break;
            }
            case GameState.Over:
            {
                StateTextUI.text = "Over . . .";
                StateTextUI.color = OverStateColor;
                

                break;
            }
        }
    }

}