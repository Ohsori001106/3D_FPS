using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject BombEffectPrefab;
    internal bool activeInHierarchy;

    public float ExplosionRadius = 3;
    // 구현 순서:
    // 1. 터질 때


    public int Damage = 60;
    //플페이어를 제외하고 물체에 닿으면(=충돌이 일어나면)
    //자기 자신을 사라지게 하는 코드 작성

    private Collider[] _colliders = new Collider[10];
    private void OnCollisionEnter(Collision other)
    {
        gameObject.SetActive(false); // 창고에 넣는다.

        GameObject effect = Instantiate(BombEffectPrefab);
        effect.transform.position = this.gameObject.transform.position;

        // 2. 범위안에 있는 모든 콜라이더를 찾는다.
        // -> 피직스.오버랩 함수는 특정 영역(radius) 안에 있는 특정 레이어들의 게임 오브젝트의
        //    콜라이더 컴포넌트들을 모두 찾아 배열로 반환하는 함수
        // 영역의 형태: 스피어, 큐브, 캡슐
        int layer = LayerMask.GetMask("Player") | LayerMask.GetMask("Monster");
        int count = Physics.OverlapSphereNonAlloc(transform.position, ExplosionRadius, _colliders, layer);
        // 3. 찾은 콜라이더 중에서 타격 가능한(IHitable) 오브젝트를 찾아서 Hit()한다.
        for (int i = 0; i < count; i++)
        {
            Collider c = _colliders[i];
            IHitable hitable = c.GetComponent<IHitable>();
            if (hitable != null)
            {
                DamageInfo damageInfo = new DamageInfo(DamgeType.Normal, Damage);
                hitable.Hit(damageInfo);
            }
        }
    }
}