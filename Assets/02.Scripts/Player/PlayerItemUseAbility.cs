using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerItemUseAbility : MonoBehaviour
{ 
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ItemManager.Instance.TryUseItem(ItemType.Health);
            ItemManager.Instance.RefreshUI();

        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            ItemManager.Instance.TryUseItem(ItemType.Stamina);
            ItemManager.Instance.RefreshUI();

        }
        if (Input.GetKeyDown(KeyCode.U))
        {

            ItemManager.Instance.TryUseItem(ItemType.Bullet);
            ItemManager.Instance.RefreshUI();

        }
    }
}
