using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType
{
    Health,
    Stamina,
    Bullet
}
public class Item
{
    public ItemType ItemType;
    public int Count;
    
    public Item(ItemType itemType, int count)
    {
        ItemType = itemType;
        Count = count;
    }

    public bool TryUse()
    {
        if(Count == 0)
        {
            return false;
        }

        Count -= 1;

        switch(ItemType)
        {
            case ItemType.Health:
            {
                PlayerMoveAbility playerMoveAbility = GameObject.FindWithTag("Player").GetComponent<PlayerMoveAbility>();
                playerMoveAbility.Health = playerMoveAbility.MaxHealth;
                break;
            }
            case ItemType.Stamina:
            {
                PlayerMoveAbility playerMoveAbility = GameObject.FindWithTag("Player").GetComponent<PlayerMoveAbility>();
                playerMoveAbility.Stamina = playerMoveAbility.MaxStamina;
                break;
            }
            case ItemType.Bullet:
            {

                PlayerGunFireAbility ability = GameObject.FindWithTag("Player").GetComponent<PlayerGunFireAbility>();
                ability.CurrentGun.BulletRemainCount = ability.CurrentGun.BulletMaxCount;
                ability.RefreshUI();
                break;
            }
        }
        return true;
    }
   
}
