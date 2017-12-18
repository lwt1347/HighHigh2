using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackGround_Physics : MonoBehaviour, IGameObject
{


    //물리적 배경의 속도
    public float speed = 0.1f;



    public void GameUpdate()
    {
        //0 ~ 1 사이 값 사이클
        float y = Mathf.Repeat(Time.time * speed, 1);

        //y축으로만 이동 이동된 y만큼 보충
        Vector2 offset = new Vector2(0, y);

        //Renderer: 그림 그려줌
        GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}
