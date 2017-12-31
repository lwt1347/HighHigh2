using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Arrow : Trap_Arrow_Check
{
    [SerializeField]
    private Rigidbody2D rb;

    private void Start()
    {
        //500f 만큼의 힘으로 화살을 발사한다.
        rb.AddForce(new Vector2(arrowSpeed, 0));
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어 제거");
            
        }
        
    }
}
