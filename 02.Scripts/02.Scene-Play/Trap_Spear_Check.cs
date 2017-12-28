using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Spear_Check : TrapCenter
{

    [SerializeField]
    private Animator anim = null;

    [SerializeField]
    private BoxCollider2D attackBox = null;



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어 감지");
            anim.SetTrigger("IsCheck");
            //.y = 0.26f;
            //attackBox.offset = new Vector2(0, 0.26f);
        }
    }

}