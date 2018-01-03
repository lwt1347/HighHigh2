using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NatureCenter : NatureManager {
    
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

}
