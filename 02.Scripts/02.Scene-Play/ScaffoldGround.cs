using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaffoldGround : Singleton<ScaffoldGround>, IGameObject
{
   
    //발판과 상호작용하기 위한 플레이어
    private GameObject player = null;

    [SerializeField]    //ScaffoldGround 상판 BoxCollider
    private BoxCollider2D thisBoxCollider2D = null;

    //발판 삭제 하기 위해서 - 리스트에서 제거
    [SerializeField]
    private ScaffoldGround_RespawnManager scaffoldGround_RespawnManager = null;

    //플레이어 발 BoxCollider
    private BoxCollider2D playerBoxCollider2D = null;

    //블럭과 같이 떨어지는 속도
    public float downSpeed = 1.5f;

    //삭제 여부 true 일때 삭제
    public bool removeFlag = false;

    //초기화
    private void Awake()
    {
        removeFlag = false;
    }//Awake() 종료

    //삭제 설정
    public void setDestroy()
    {
        removeFlag = true;
    }

    public void GameUpdate()
    {
       
    }//GameUpdate() 종료

    public void ScaffoldGroundInit(GameObject player)
    {
        //Player 받아오기
        this.player = player;
        playerBoxCollider2D = player.GetComponent<BoxCollider2D>();

    }//ScaffoldGroundInit(GameObject player) 종료

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == playerBoxCollider2D)
        {
            //바닥 사각형 
            if ((player.GetComponent<Player>().playerDirctionFlag))
            {
                thisBoxCollider2D.isTrigger = false;
            }
        }
        
    }//OnTriggerEnter2D(Collider2D other) 종료
    
    private void OnTriggerExit2D(Collider2D other)
    {
        //바닥 사각형 
        if (other == playerBoxCollider2D)
        {
            thisBoxCollider2D.isTrigger = true;
        }

    }//OnTriggerExit2D(Collider2D other) 종료

}

 