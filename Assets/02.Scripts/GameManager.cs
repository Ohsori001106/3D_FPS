using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // ����: ���� ������
    // -> ���� ��ü�� ���¸� �˸���, ���۰� ���� �ؽ�Ʈ�� ��Ÿ����.
    public enum GameState
    {
        Ready, // �غ�
        Start, // ����
        Over,  // ����
    }
    public PlayerMoveAbility Player;

    public GameState State = GameState.Ready;

    public Text StateTextUI;

    // ���� ����
    // 1. ���� �غ� ����
    // 2. 1.6�� �Ŀ� ���� ���� ����(Start!)
    // 3. 0.4�� �Ŀ� �ؽ�Ʈ �������...
    // 4. �÷��̸� �ϴٰ�
    // 5. �÷��̾� ü���� 0�� �Ǹ� "���� ����" ����

    public void Refresh()
    {
        switch (State)
        {
            case GameState.Ready:
            {
                StateTextUI.text = "Ready...";
                break;
            }
            case GameState.Start:
            {
                StateTextUI.text = "Go!";
                break;
            }
            case GameState.Over:
            {
                StateTextUI.text = "Over...";
                break;
            }
        }
    }
    void Start()
    {
        StartCoroutine(Start_Coroutine());
    }

    private IEnumerator Start_Coroutine()
    {
        State = GameState.Ready;
        StateTextUI.gameObject.SetActive(true);
        
        // 1.6�� �ڿ� ���� ����
        yield return new WaitForSeconds(1.6f);
        State = GameState.Start;
        Refresh();
        // 0.4. �� �� �ؽ�Ʈ �������
        yield return new WaitForSeconds(0.4f);
        StateTextUI.gameObject.SetActive(false);

    }

    private void Update()
    {
        if (Player.Health <= 0)
        {
            State = GameState.Over;
            StateTextUI.gameObject.SetActive(true) ;
            Refresh();
        }
    }
}
