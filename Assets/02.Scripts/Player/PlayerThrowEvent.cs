using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class PlayerThrowEvent : MonoBehaviour
{
    private PlayerBombFireAbility _owner;
    void Start()
    {
        _owner = GetComponentInParent<PlayerBombFireAbility>();
    }

    public  void ThrowEvent()
    {
        Debug.Log("���� �̺�Ʈ �߻�~~");
        _owner.ThrowEvent();
    }
}
