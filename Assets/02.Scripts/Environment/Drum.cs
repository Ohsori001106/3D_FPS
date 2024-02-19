using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drum : MonoBehaviour,IHitable
{
    public float Health;
    public float MaxHealth = 3;
    public void Hit(int damage)
    {
        Health -= damage;
        if (Health < 0)
        {
            Destroy(gameObject);
        }
    }
    private void Init()
    {
        Health = MaxHealth;
    }
    void Start()
    {
        Init();
    }
}
