using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    //메인카메라가 캐릭터를 따라 다닌다.
    public Transform target;
    public float smoothing = 5f;

    Vector3 offset;

    private void Start()
    {
        offset = transform.position - target.position;
    }

    private void FixedUpdate()
    {
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        //Lerp = 두 위치 사이를 부드럽게 이동할 수 있다. smoothing 만큼 캐릭터를 카메라가 쫓아다니는 형식으로, 붙어 가는게 아님.
        //smoothing * Time.deltaTime -> 초당 50회 실행 하지 않고 1번 실행 하는 결과로 나타냄

    }
}
