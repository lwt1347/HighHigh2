using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyArea : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other)
    {
        //바닥 제거
        if (other.GetComponent<ScaffoldGround>()) {
            other.GetComponent<ScaffoldGround>().setDestroy();  //발판 파괴 설정
        }else if (other.GetComponent<TrapCenter>())
        {//함정 제거
            other.GetComponent<TrapCenter>().setDestroy();      //함정 파괴 설정
        }else if (other.GetComponent<MonsterCenter>())
        {
            //몬스터 제거
            other.GetComponent<MonsterCenter>().setDestroy();      //몬스터 파괴 설정
        }

       
    }

   
}
