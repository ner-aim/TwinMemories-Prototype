using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    public Equipment[] defaultItems;
    public SkinnedMeshRenderer targetMesh;
    Equipment[] currentEquipment; //Items currently equipped
    SkinnedMeshRenderer[] currentMeshes;


    Inventory inventory;
    public delegate void onEquipmentChange(Equipment newItem, Equipment oldItem);
    public onEquipmentChange onEquipmentChanged;

    private void Start()
    {
        inventory = Inventory.instance;
        int numSlots = System.Enum.GetNames(typeof(equipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
        currentMeshes = new SkinnedMeshRenderer[numSlots];
        EquipDefaultItems();
    }

    public void equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;
        Unequip(slotIndex);
        Equipment oldItem = Unequip(slotIndex);

       

        if(onEquipmentChanged!=null){
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        setEquipmentBlendShape(newItem, 100);

        currentEquipment[slotIndex] = newItem;
        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.mesh);
        newMesh.transform.parent = targetMesh.transform;

        newMesh.bones = targetMesh.bones;
        newMesh.rootBone = targetMesh.rootBone;
        currentMeshes[slotIndex] = newMesh;
    }

    public Equipment Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            if (currentMeshes[slotIndex] != null)
            {
                Destroy(currentMeshes[slotIndex].gameObject);
            }
            Equipment oldItem = currentEquipment[slotIndex];
            setEquipmentBlendShape(oldItem, 0);
            inventory.Add(oldItem);
            currentEquipment[slotIndex] = null;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }
            return oldItem; 
        }
        return null;
    }

    public void unequipAll()
    {
        for(int i=0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }

        EquipDefaultItems();

    }


    void setEquipmentBlendShape(Equipment item, int weight)
    {
        foreach(equipmentMeshRegion blendShape in item.coveredMeshRegions)
        {
            targetMesh.SetBlendShapeWeight((int)blendShape, weight);
        }
    }

    void EquipDefaultItems()
    {
        foreach(Equipment item in defaultItems)
        {
            equip(item);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            unequipAll();
        }
    }
}
