using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // ��ǥ: ī�޶� ���� �ð����� �����ϰ� ���� �ʹ�.
    // �ʿ� �Ӽ�:
    // - ����ŷ �ð�
    public float ShakingDuration = 0.2f;
    // - ����ŷ �����ð�
    public float _shakingTimer = 0f;
    // - ����ŷ �Ŀ�
    public float ShakingPower = 0.025f;
    // - ����ŷ ���̳�
    private bool _isShaking = false;
    
    void Start()
    {
        _shakingTimer = 0f;
        _isShaking = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (!_isShaking)
        {
            return;
        }
        // ���� ����:
        // 1. �ð��� �帥��.
        _shakingTimer += Time.deltaTime;

        // 2. �����ϰ� ����.
        transform.position = Vector3.zero + Random.insideUnitSphere * ShakingPower;

        // 3. ���� �ð��� ������ �ʱ�ȭ
        if (_shakingTimer >= ShakingDuration)
        {
            _isShaking = false;
            transform.position = Vector3.zero;
        }
    }

    public void Shake()
    {
        _shakingTimer = 0f;
            _isShaking = true;
    }
}
