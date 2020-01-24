using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    // Start is called before the first frame update
    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += onEquipmentChanged;
    }

    // Update is called once per frame
    

    void onEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);
        }

        if (oldItem != null)
        {
            armor.RemoveModifer(oldItem.armorModifier);
            damage.RemoveModifer(oldItem.damageModifier);
        }
        
    }

    public override void Die()
    {
        base.Die();

        //Kill the player
        PlayerManager.instance.KillPlayer();
    }
}
