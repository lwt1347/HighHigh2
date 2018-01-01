using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackGround : MonoBehaviour, IGameObject
{

    //물리적 배경의 속도
    public float speed = 0.1f; //배경 그림 회전
    private float upSpeed = 1.0f; //배경 물리적 위로
    private Vector2 offset;

    private bool ChangeWallFlag = false;
    //좌우 벽 배경이 변경될 시점
    public bool ChangeWall()
    {
        if (offset.y >= 0.97f)
        {
            
            if (ChangeWallFlag)
            {
                Debug.Log("!");
                ChangeWallFlag = false;
                return true;
            }
            else
            {
                return false;
            }
        }
        if (offset.y <= 0.05f)
        {
            ChangeWallFlag = true;
        }
        
        return false;
    }

    

    public void GameUpdate()
    {
        //0 ~ 1 사이 값 사이클
        float y = Mathf.Repeat(Time.time * speed, 1);

        //좌측 1번 벽(공룡 뼈)
        if (this.tag == "ScaffoldGround")
        {
            GetComponent<EdgeCollider2D>().offset = new Vector2(0,-y);   
        }

        //y축으로만 이동 이동된 y만큼 보충
        offset = new Vector2(0, y);
        

        //Renderer: 그림 그려줌
        GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);

        
        this.transform.Translate(Vector2.up * upSpeed * Time.deltaTime);

    }
}