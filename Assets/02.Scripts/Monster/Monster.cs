using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;


public enum MonsterState
{
    Idle,     // 대기
    Trace,    // 추적
    Attack,   // 공격
    Comeback,   // 복귀
    Damaged,  // 공격 당함
    Die       // 사망
}
public class Monster : MonoBehaviour, IHitable
{
    [Range(0, 100)]
    public float Health;
    public float MaxHealth = 100;
    public Slider HealthSliderUI;
    public float MoveSpeed = 4f;  // 이동 상태
    public const float TOLERANCE = 0.1f;

    private CharacterController _characterController;  // 캐릭터 컨트롤러

    public Transform _target; // 플레이어
    public float FindDistance = 5; // 감지 범위
    public float AttackDistance = 2; // 공격 범위

    public Vector3 StartPoisition;   // 시작 위치
    public float MoveDistance = 20f; // 움직일 수 있는 거리


   private MonsterState _currentState = MonsterState.Idle;

    public void Init()
    {
        Health = MaxHealth;
    }
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        StartPoisition = transform.position;

        Init();
    }
    void Update()
    {
        HealthSliderUI.value = Health / MaxHealth;

        // 상태 패던: 상태에 따라 행동을 다르게 하는 패턴
        // 1. 몬스터가 가질 수 있는 행동에 따라 상태를 나눈다.
        // 2. 상태들이 조건에 따라 자연스럽게 전환(Transition)되게 설계한다.

        switch (_currentState)
        {
            case MonsterState.Idle:
                // Idle 상태일때의 행동 코드 작성
                Idle();
                break; 
            case MonsterState.Trace:
                // Trace 상태일때의 행동 코드를 작성
                Trace();
                break;
            case MonsterState.Attack:
                Attack();
                break;
            case MonsterState.Comeback:
                Comeback();
                break;
        }
    }

    private void Idle()
    {
        // todo: 몬스터의 Idle 애니메이션 재생

        // Idle 상태일때의 행동 코드를 작성
        if (Vector3.Distance(_target.position,transform.position)<= FindDistance)
        {
            Debug.Log("상태전환: Idle -> Trace");
            _currentState = MonsterState.Trace;
        }
    }

    private void Trace()
    {
        // 플레이어를 쳐다본다
        Vector3 dir = _target.transform.position - this.transform.position;
        dir.Normalize();
        dir.y = 0;
        // 플레이어에게 다가간다
        _characterController.Move(dir * MoveSpeed * Time.deltaTime);
        // 쳐다본다
        transform.LookAt(_target);

        // 플레이어가 공격범위에 들어오면
        if (Vector3.Distance(_target.position,transform.position)<=AttackDistance)
        {
            Debug.Log("상태 전환: Trace -> Attack");
            _currentState = MonsterState.Attack;
        }
        if (Vector3.Distance(transform.position, StartPoisition) >= MoveDistance)
        {
            Debug.Log("상태 전환: Trace -> Return");
            _currentState = MonsterState.Comeback;
        }

    }
    private void Attack()
    {

    }

    private void Comeback()
    {
        // 복귀 상태 행동구현

        Vector3 dir = StartPoisition-this.transform.position;
        dir.Normalize();

        _characterController.Move(dir* MoveSpeed * Time.deltaTime);
        RotateCharacter(StartPoisition);

        if (Vector3.Distance(transform.position , StartPoisition)< TOLERANCE)
        {
            Debug.Log("상태 전환: Comeback -> idle");
            _currentState = MonsterState.Idle;
        }
    }

    public void Hit(int damage)
    {
        
        
            Health -= damage;
            if (Health < 0)
            {
                Die();

            }
        
            
    }

    



    public void Die()
    {
        // 죽을 때 아이템생성
        ItemObjectFactory.Instance.MakePercent(transform.position);
        
            Destroy(gameObject);
        
    }

    public void RotateCharacter(Vector3 targetPosition)
    {
        // 캐릭터의 위치를 기준으로 회전을 계산
        Vector3 direction = targetPosition - transform.position;
        direction.y = 0f; // y 값은 회전에 영향을 미치지 않도록 설정

        // 목표 방향의 Euler 각도 계산
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;


        // 회전 적용
        transform.eulerAngles = new Vector3(0, targetAngle, 0);
    }
}
