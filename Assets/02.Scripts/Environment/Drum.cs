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

    public Texture[] textures;
    private new MeshRenderer renderer;
    private Rigidbody rb;

    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        renderer = GetComponentInChildren<MeshRenderer>();

        int idx = UnityEngine.Random.Range(0, textures.Length);

        GetComponent<Renderer>().material.mainTexture = textures[idx];
    }
    public void Hit(DamageInfo damageInfo)
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

        ItemObjectFactory.Instance.MakePercent(transform.position);


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
        // 3. ã�� �ݶ��̴� �߿��� Ÿ�� ������(IHitable) ������Ʈ�� ã�Ƽ� Hit()�Ѵ�.
        foreach (Collider collider in colliders)
        {
            IHitable hitable = collider.GetComponent<IHitable>();
            if (hitable != null)
            {

                DamageInfo damageInfo = new DamageInfo(DamgeType.Normal, Damage);
                // 4. Hit() �Ѵ�.
                hitable.Hit(damageInfo);
                

                
            }
        }
        int enviromentLayer = LayerMask.GetMask("Environment");
        Collider[] enviromentLayers = Physics.OverlapSphere(transform.position, ExplosionRadius, enviromentLayer);
        foreach (Collider collider in enviromentLayers)
        {
            Drum drum = null;
            if (collider.TryGetComponent<Drum>(out drum))
            {
                // 4. Hit() �Ѵ�.
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
