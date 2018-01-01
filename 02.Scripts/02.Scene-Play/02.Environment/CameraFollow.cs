using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : Singleton<CameraFollow>
{
    //메인카메라가 캐릭터를 따라 다닌다.
    private GameObject target;
    private float smoothing = 5f;

    Vector3 offset;
    public Transform targetVector;

    public void CameraFollowInit(GameObject player)
    {
        target = player;
        offset = transform.position - target.transform.position;
    }


    float tempYPo;
    float big = -100;
    private void FixedUpdate()
    {
        //Vector3 targetCamPos = target.position + offset;
        //transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);

        //카메라는 y축으로만 이동한다.
        tempYPo = (Mathf.Round(target.transform.position.y / .0001f) * .0001f);

        if (tempYPo >= big)
        {
            targetVector.transform.position = new Vector3(0, tempYPo, 0);
            Vector3 targetCamPos = targetVector.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetCamPos, 10 * Time.deltaTime);
        }

        //y축으로 캐릭터위치에 따라 움직인다. 캐릭터가 착지 상태에서의 최대 높이 이하로 떨어지지 않는다.
        if (big <= tempYPo && target.GetComponent<Player>().GetPlayerYVelocity() == 0)
        {
            big = tempYPo;
        }

        
        //Lerp = 두 위치 사이를 부드럽게 이동할 수 있다. smoothing 만큼 캐릭터를 카메라가 쫓아다니는 형식으로, 붙어 가는게 아님.
        //smoothing * Time.deltaTime -> 초당 50회 실행 하지 않고 1번 실행 하는 결과로 나타냄

    }
}
