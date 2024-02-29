using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodFactory : MonoBehaviour
{
    public static BloodFactory instance { get; private set; }
    public GameObject BloodPrefab;
    
    public List<GameObject> _bloodPool;
    public int _bloodPoolSize = 10;
    private void Awake()
    {
        instance = this; 
        _bloodPool = new List<GameObject>();
        for (int i = 0; i < _bloodPoolSize; i++)
        {
            
            GameObject blood = Instantiate(BloodPrefab);
            
            
            
            _bloodPool.Add(blood);
            
            blood.SetActive(false);
        }
    }
    private GameObject GetPooledBlood()
    {
        foreach (GameObject blood in _bloodPool)
        {
            if (!blood.activeInHierarchy)
            {
                return blood; // ��Ȱ��ȭ�� ���� ��ü ��ȯ
            }
        }
        return null; // Ǯ�� ��Ȱ��ȭ�� ���� ��ü�� ���� ���
    }
    public void Make(Vector3 position, Vector3 normal)
    {
        GameObject bloodObject = GetPooledBlood();
        /* // Ǯ���� ���� ��ü ��������
        if (bloodObject == null) // Ǯ�� ���� ��ü�� ���� ���
        {
            // Ǯ�� ���� ��ü�� �����Ƿ� ���ο� ���� ��ü ����
            bloodObject = Instantiate(BloodPrefab);
            _bloodPool.Add(bloodObject);
        }*/

        // ���� ��ü�� ��ġ�� ���� ����
        foreach (GameObject blood in _bloodPool) 
        {  
            if (!blood.activeInHierarchy)
            {
                blood.GetComponent<DestroyTime>()?.Init();
                blood.transform.position = position;
                blood.transform.forward = normal;
                break;
            } 
        }
        
       
        bloodObject.SetActive(true); // Ȱ��ȭ ���·� ����
    }
}
