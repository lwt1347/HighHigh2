using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_SawTooth : TrapCenter, IGameObject
{


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("톱니에 닿임");
        }
    }

}
