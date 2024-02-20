using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drum : MonoBehaviour, IHitable
{
    public GameObject DrumEffectPrefab;
    public int hitCount = 0;

    public float DelayTime =3f;

    public float UPPower = 15f;
    public int RotationPower = 5;

    public float ExplosionRadius = 5;

    private bool _isExplosion = false;

    public int Damage = 70;
    public void Hit(int damage)
    {
        
        hitCount += 1;
        if (hitCount >= 3)
        {
            Explosion();
        }
    }

  
 
    
    public void Explosion()
    {

        if (_isExplosion)
        {
            return;
        }
        _isExplosion = true;

        GameObject drumBomb = Instantiate(DrumEffectPrefab);
        drumBomb.transform.position = this.transform.position;

        Rigidbody rigidbody = GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rigidbody.AddForce(Vector3.up * UPPower, ForceMode.Impulse);
            rigidbody.AddTorque(-Vector3.right * RotationPower, ForceMode.Impulse);
        }

        int findlayer = LayerMask.GetMask("Monster", "Player", "Environment");
        Collider[] colliders = Physics.OverlapSphere(transform.position, ExplosionRadius, findlayer);
        // 3. 찾은 콜라이더 중에서 타격 가능한(IHitable) 오브젝트를 찾아서 Hit()한다.
        foreach (Collider collider in colliders)
        {
            IHitable hitable = collider.GetComponent<IHitable>();
            if (hitable != null)
            {
                
                
                    // 4. Hit() 한다.
                    hitable.Hit(Damage);
                

                
            }
        }
        int enviromentLayer = LayerMask.GetMask("Environment");
        Collider[] enviromentLayers = Physics.OverlapSphere(transform.position, ExplosionRadius, enviromentLayer);
        foreach (Collider collider in enviromentLayers)
        {
            Drum drum = null;
            if (collider.TryGetComponent<Drum>(out drum))
            {
                // 4. Hit() 한다.
                drum.Explosion();
            }
        }
        StartCoroutine(Destry_Coroutine(DelayTime));
    }

    
    

    private IEnumerator Destry_Coroutine(float delayTime) 
    {

        yield return new WaitForSeconds(delayTime);
        
        
        Destroy(this.gameObject);

    }
}
