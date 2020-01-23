using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Interactable
{
    public override void Interact()
    {
        base.Interact();
        //attack
        Debug.Log("Interacting with the enemy");
    }
}
