using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCowMove : MonsterCenter{
    
    [SerializeField]
    private Rigidbody2D rb;

    public bool attackMode = false;

    //몬스터와 상호작용하기 위해 플레이어 정보를 가져온다.
    public void MonsterMoveInit(GameObject player)
    {
        this.player = player;
    }//MonsterManagerInit(GameObject player) 종료

    private void Start()
    {
        if (Random.Range(0,10) < 5) {
            this.transform.localScale = new Vector3(-1, 1, 1);
        }else
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void Update()
    {
        
        if (attackMode) {
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

            //이동방향 및 이미지 방향
            MonsterMoveDirection();
        }
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (attackMode)
        {
            rb.AddForce(new Vector2(xForce * 3f, 0));
        }
        if (rb.velocity.x >= 7.5f)
        {
            rb.velocity = new Vector2(7.5f, rb.velocity.y);
        }
        else if (rb.velocity.x <= -7.5f)
        {
            rb.velocity = new Vector2(-7.5f, rb.velocity.y);
        }

        if (rb.velocity.y <= -5.5f)
        {
            //중력가속도가 너무 많이 올라가면 ScaffoldGround 를 뚫고 내려감 - 방지
            rb.velocity = new Vector2(rb.velocity.x, -5.5f);
        }

    }//OnTriggerStay2D(Collider2D other) 종료
    
   


}
