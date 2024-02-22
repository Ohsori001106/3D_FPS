using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;


public enum MonsterState
{
    Idle,     // ���
    Trace,    // ����
    Attack,   // ����
    Comeback,   // ����
    Damaged,  // ���� ����
    Die       // ���
}
public class Monster : MonoBehaviour, IHitable
{
    [Range(0, 100)]
    public float Health;
    public float MaxHealth = 100;
    public Slider HealthSliderUI;
    public float MoveSpeed = 4f;  // �̵� ����
    public const float TOLERANCE = 0.1f;

    private CharacterController _characterController;  // ĳ���� ��Ʈ�ѷ�

    public Transform _target; // �÷��̾�
    public float FindDistance = 5; // ���� ����
    public float AttackDistance = 2; // ���� ����

    public Vector3 StartPoisition;   // ���� ��ġ
    public float MoveDistance = 20f; // ������ �� �ִ� �Ÿ�


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

        // ���� �д�: ���¿� ���� �ൿ�� �ٸ��� �ϴ� ����
        // 1. ���Ͱ� ���� �� �ִ� �ൿ�� ���� ���¸� ������.
        // 2. ���µ��� ���ǿ� ���� �ڿ������� ��ȯ(Transition)�ǰ� �����Ѵ�.

        switch (_currentState)
        {
            case MonsterState.Idle:
                // Idle �����϶��� �ൿ �ڵ� �ۼ�
                Idle();
                break; 
            case MonsterState.Trace:
                // Trace �����϶��� �ൿ �ڵ带 �ۼ�
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
        // todo: ������ Idle �ִϸ��̼� ���

        // Idle �����϶��� �ൿ �ڵ带 �ۼ�
        if (Vector3.Distance(_target.position,transform.position)<= FindDistance)
        {
            Debug.Log("������ȯ: Idle -> Trace");
            _currentState = MonsterState.Trace;
        }
    }

    private void Trace()
    {
        // �÷��̾ �Ĵٺ���
        Vector3 dir = _target.transform.position - this.transform.position;
        dir.Normalize();
        dir.y = 0;
        // �÷��̾�� �ٰ�����
        _characterController.Move(dir * MoveSpeed * Time.deltaTime);
        // �Ĵٺ���
        transform.LookAt(_target);

        // �÷��̾ ���ݹ����� ������
        if (Vector3.Distance(_target.position,transform.position)<=AttackDistance)
        {
            Debug.Log("���� ��ȯ: Trace -> Attack");
            _currentState = MonsterState.Attack;
        }
        if (Vector3.Distance(transform.position, StartPoisition) >= MoveDistance)
        {
            Debug.Log("���� ��ȯ: Trace -> Return");
            _currentState = MonsterState.Comeback;
        }

    }
    private void Attack()
    {

    }

    private void Comeback()
    {
        // ���� ���� �ൿ����

        Vector3 dir = StartPoisition-this.transform.position;
        dir.Normalize();

        _characterController.Move(dir* MoveSpeed * Time.deltaTime);
        RotateCharacter(StartPoisition);

        if (Vector3.Distance(transform.position , StartPoisition)< TOLERANCE)
        {
            Debug.Log("���� ��ȯ: Comeback -> idle");
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
        // ���� �� �����ۻ���
        ItemObjectFactory.Instance.MakePercent(transform.position);
        
            Destroy(gameObject);
        
    }

    public void RotateCharacter(Vector3 targetPosition)
    {
        // ĳ������ ��ġ�� �������� ȸ���� ���
        Vector3 direction = targetPosition - transform.position;
        direction.y = 0f; // y ���� ȸ���� ������ ��ġ�� �ʵ��� ����

        // ��ǥ ������ Euler ���� ���
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;


        // ȸ�� ����
        transform.eulerAngles = new Vector3(0, targetAngle, 0);
    }
}
