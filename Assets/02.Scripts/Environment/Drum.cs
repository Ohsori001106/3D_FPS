using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drum : MonoBehaviour, IHitable
{
    public GameObject DrumEffectPrefab;
    public float Health;
    public float MaxHealth = 3;

    public float DelayTime =3f;

    public float UPPower = 15f;
    public int RotationPower = 5;

    public float ExplosionRadius = 5;

    public int Damage = 70;
    public void Hit(int damage)
    {
        Health -= damage;
        if (Health == 0)
        {
            
            Kill();

            StartCoroutine( Destry_Coroutine(DelayTime));
        }
    }
    private void Init()
    {
        Health = MaxHealth;
    }
    void Start()
    {
        Init();
        Rigidbody rigidbody = GetComponent<Rigidbody>();
    }
    
    public void Kill()
    {
        GameObject drumBomb = Instantiate(DrumEffectPrefab);
        drumBomb.transform.position = this.transform.position;

        Rigidbody rigidbody = GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rigidbody.AddForce(Vector3.up * UPPower, ForceMode.Impulse);
            rigidbody.AddTorque(-Vector3.right * RotationPower, ForceMode.Impulse);
        }

        int findlayer = LayerMask.GetMask("Monster", "Player");
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
    }

    private IEnumerator Destry_Coroutine(float delayTime) 
    {

        yield return new WaitForSeconds(delayTime);
        
        
        Destroy(this.gameObject);

    }
}
