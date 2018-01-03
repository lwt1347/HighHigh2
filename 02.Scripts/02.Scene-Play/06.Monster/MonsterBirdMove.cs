using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBirdMove : MonsterCenter
{

    private void Update()
    {

        //이동방향 및 이미지 방향
        MonsterMoveDirection();

        transform.position += moveVelocity * 2.5f * Time.deltaTime;

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            if (dist == "Left")
            {
                dist = "Right";
            }
            else if (dist == "Right")
            {
                dist = "Left";
            }
        }
        else if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어 히트");
        }
    }

}
