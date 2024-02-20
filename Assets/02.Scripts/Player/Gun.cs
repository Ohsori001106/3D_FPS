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
public class Gun : MonoBehaviour
{
    public GunType GType;

    public int damage = 10;

    
    public float FireCoolTime = 0.2f;

    // 총알 개수
    public int BulletRemainCount;
    // 최대총알숫자
    public int BulletMaxCount = 30;

    public float Relode = 1.5f;

    public Sprite ProfileImage;



    void Start()
    {
        BulletRemainCount = BulletMaxCount;
       
    }

    // Update is called once per frame
   

    
}
