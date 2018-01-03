using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyArea3D : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<NatureCenter>())
        {
            other.GetComponent<NatureCenter>().setDestroy();      //자연 파괴 설정
        }
       
    }
    
}
