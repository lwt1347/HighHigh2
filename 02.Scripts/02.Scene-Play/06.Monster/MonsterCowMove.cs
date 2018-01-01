using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCowMove : MonsterManager{

    private string dist = "Left";
    private float xForce;
    private Vector3 moveVelocity = Vector3.zero;

    [SerializeField]
    private Rigidbody2D rb;

    //몬스터와 상호작용하기 위해 플레이어 정보를 가져온다.
    public void MonsterMoveInit(GameObject player)
    {
        this.player = player;
    }//MonsterManagerInit(GameObject player) 종료
    
    private void Update()
    {

        if (player.transform.position.x < transform.position.x)
        {
            xForce = -1f;
            dist = "Left";
        }
        else if (player.transform.position.x > transform.position.x)
        {
            xForce = 1f;
            dist = "Right";
        }

        if (dist == "Left")
        {
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if(dist == "Right")
        {
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        //transform.position += moveVelocity * 3.5f * Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        rb.AddForce(new Vector2(xForce * 8f, 0));
    }
   

}
