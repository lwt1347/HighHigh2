using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaffoldGround : Singleton<ScaffoldGround>, IGameObject
{

    [SerializeField]
    private Player player = null;

    [SerializeField]
    private BoxCollider2D boxCollider2D = null;

    private bool playerPass = false;
    private bool playerNoPass = false;


    public void GameUpdate()
    {

        if (startTrigger == true)
        {
            jumpTimer += Time.deltaTime;
            if (jumpTimer > jumpWaitingTime)
            {

                Debug.Log("jumpTimer = 온 : " + jumpTimer);
                jumpTimer = 0.0f;
                triggerFlag = true;
                startTrigger = false;
                
            }
        }
       
    }

    //슈퍼 점프를 막기위한 OnTriggerStay2D(Collider2D other)에 사용될 타임 카운트 변수
    private float jumpTimer = 0.0f;
    private float jumpWaitingTime = 0.2f;
    private bool startTrigger = false;
    private bool triggerFlag = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //바닥 사각형 
        
        if((player.playerDirctionFlag))
        {
            boxCollider2D.isTrigger = false;
        }

        startTrigger = true;
       
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (triggerFlag)
        {
            boxCollider2D.isTrigger = true;
        }else
        {
            jumpTimer = 0.0f;
        }
        startTrigger = true;
    }
    

}

 