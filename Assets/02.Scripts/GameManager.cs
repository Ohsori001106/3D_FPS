using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // 역할: 게임 관리자
    // -> 게임 전체의 상태를 알리고, 시작과 끝을 텍스트로 나타낸다.
    public enum GameState
    {
        Ready, // 준비
        Start, // 시작
        Over,  // 오버
    }
    public PlayerMoveAbility Player;

    public GameState State = GameState.Ready;

    public Text StateTextUI;

    // 게임 상태
    // 1. 게임 준비 상태
    // 2. 1.6초 후에 게임 시작 상태(Start!)
    // 3. 0.4초 후에 텍스트 사라지고...
    // 4. 플레이를 하다가
    // 5. 플레이어 체력이 0이 되면 "게임 오버" 상태

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
        
        // 1.6초 뒤에 게임 시작
        yield return new WaitForSeconds(1.6f);
        State = GameState.Start;
        Refresh();
        // 0.4. 초 뒤 텍스트 사라지고
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
