using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public int armorModifier;
    public int damageModifier;
    public equipmentSlot equipSlot;
    public SkinnedMeshRenderer mesh;
    public equipmentMeshRegion[] coveredMeshRegions;

    public override void use()
    {
        base.use();
        //Equip and remove
        EquipmentManager.instance.equip(this);

        removeFromInventory();
         
    }
}

public enum equipmentSlot { Head, Chest, Legs, Weapon, Shield, Feet}
public enum equipmentMeshRegion { Legs,Arms ,Torso }//Body blends
