using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public enum GunType
{
    Rifle,
    Sniper,
    Pistoi
}
public class PlayerGun : MonoBehaviour
{
    public GunType GType;

    public int damage = 10;

    
    public float FireCoolTime = 0.2f;

    // �Ѿ� ����
    public int BulletRemainCount;
    // �ִ��Ѿ˼���
    public int BulletMaxCount = 30;

    public float Relode = 1.5f;

    



    void Start()
    {
        BulletRemainCount = BulletMaxCount;
       
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    
}
