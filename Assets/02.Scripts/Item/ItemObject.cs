using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemObject : MonoBehaviour
{
    public ItemType ItemType;

    public float ExplosionRadius = 3;



    private void Update()
    {
        int layer = LayerMask.GetMask("Player");
        Collider[] colliders = Physics.OverlapSphere(transform.position, ExplosionRadius, layer);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        
        if (other.CompareTag("Player"))
        {
            if (ItemType == ItemType.Health)
            {
                float distance = Vector3.Distance(other.transform.position, transform.position);
                Debug.Log(distance);
                ItemManager.Instance.AddItem(ItemType.Health);
                ItemManager.Instance.RefreshUI();

                Destroy(gameObject);
            }
            
        }
        if (other.CompareTag("Player"))
        {
            if (ItemType == ItemType.Stamina)
            {
                float distance = Vector3.Distance(other.transform.position, transform.position);
                Debug.Log(distance);
                ItemManager.Instance.AddItem(ItemType.Stamina);
                ItemManager.Instance.RefreshUI();

                Destroy(gameObject);
            }
            
        }
        if (other.CompareTag("Player"))
        {
            if (ItemType == ItemType.Bullet)
            {
                float distance = Vector3.Distance(other.transform.position, transform.position);
                Debug.Log(distance);
                ItemManager.Instance.AddItem(ItemType.Bullet);
                ItemManager.Instance.RefreshUI();

                Destroy(gameObject);
            }
                
        }

    }



    





    // Todo 1. ������ �������� 3�� (Health, Stamina, Bullet) �����. (�����̳� ���� �ٸ����ؼ� ����)
    // Todo 2. �÷��̾�� ���� �Ÿ��� �Ǹ� �������� �Ծ����� �������.

    // �ǽ� ���� 31. ���Ͱ� ������ �������� ���(Health:20%, Stamina: 20%, Bullet: 10)
    // �ǽ� ���� 32. ���� �Ÿ��� �Ǹ� �������� ������ ����� ���ƿ��� �ϱ�(�߰��� ����)
}
