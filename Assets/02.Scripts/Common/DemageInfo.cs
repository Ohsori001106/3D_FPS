using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamgeType
{
    Normal,
    Critical
}
public struct DamageInfo
{
    public DamgeType DamgeType; // 0:�Ϲ�, 1: ũ��Ƽ��
    public int       Amount;    // ��������
    public Vector3   Poition;
    public Vector3   Normal;    // ��������

    public DamageInfo(DamgeType damgeType, int amount)
    {
        
        this.Amount = amount;
        this.DamgeType = DamgeType.Normal;
        this.Poition = Vector3.zero;
        this.Normal = Vector3.zero;
    }
}
