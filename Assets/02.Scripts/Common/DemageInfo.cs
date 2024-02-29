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
    public DamgeType DamgeType; // 0:일반, 1: 크리티컬
    public int       Amount;    // 데미지량
    public Vector3   Poition;
    public Vector3   Normal;    // 법선백터

    public DamageInfo(DamgeType damgeType, int amount)
    {
        
        this.Amount = amount;
        this.DamgeType = DamgeType.Normal;
        this.Poition = Vector3.zero;
        this.Normal = Vector3.zero;
    }
}
