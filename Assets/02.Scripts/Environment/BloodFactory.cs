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
                return blood; // 비활성화된 혈액 객체 반환
            }
        }
        return null; // 풀에 비활성화된 혈액 객체가 없는 경우
    }
    public void Make(Vector3 position, Vector3 normal)
    {
        GameObject bloodObject = GetPooledBlood();
        /* // 풀에서 혈액 객체 가져오기
        if (bloodObject == null) // 풀에 혈액 객체가 없는 경우
        {
            // 풀에 혈액 객체가 없으므로 새로운 혈액 객체 생성
            bloodObject = Instantiate(BloodPrefab);
            _bloodPool.Add(bloodObject);
        }*/

        // 혈액 객체의 위치와 방향 설정
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
        
       
        bloodObject.SetActive(true); // 활성화 상태로 변경
    }
}
