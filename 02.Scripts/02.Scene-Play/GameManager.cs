using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    
    //플레이어
    [SerializeField]
    private GameObject player = null;
    
    [SerializeField]
    private GameObject scaffoldGround_RespawnManager = null;

    //좌우벽 매니저 wallManager [변수명 오류남]
    [SerializeField]
    private GameObject wall_Manager = null;
    
    private void Awake()
    {
        //해상도 600:1024 고정
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.SetResolution(24, 600, true);
    }//Awake() 종료

    private void Start()
    {
        //캐릭터 생성
        player = Instantiate(player) as GameObject;   // = 을 해주어야 할당이 제대로 된다.
        CameraFollow.Instance.CameraFollowInit(player); //카메라 시점 이동시키기

        //발판 매니저 생성
        scaffoldGround_RespawnManager = Instantiate(scaffoldGround_RespawnManager) as GameObject;
        scaffoldGround_RespawnManager.GetComponent<ScaffoldGround_RespawnManager>().scaffoldGround_RespawnManagerInit(player);

        wall_Manager = Instantiate(wall_Manager) as GameObject;


        ////좌측 벽 생성
        //for(int i = 0; i < moveBackGroundLeft.Length; i++)
        //{
        //    moveBackGroundLeft[i] = Instantiate(moveBackGroundLeft[i]);
        //}

        ////우측 벽 생성
        //for (int i = 0; i < moveBackGroundRight.Length; i++)
        //{
        //    moveBackGroundRight[i] = Instantiate(moveBackGroundRight[i]);
        //}



    }//Start() 종료

    private float downSpeed = 1.0f;
    private float downSpeedTemp = 0f;
    void Update () {

        downSpeedTemp = downSpeed * Time.deltaTime;
        //발판 움직임
        scaffoldGround_RespawnManager.GetComponent<ScaffoldGround_RespawnManager>().GameUpdate();


        //플레이어 게임 업데이트
        player.GetComponent<Player>().GameUpdate();

        //벽 매니저
        wall_Manager.GetComponent<WallManager>().GameUpdate();


        ////좌우 벽 업데이트
        //moveBackGroundLeft[0].GetComponent<MoveBackGround>().GameUpdate();
        //     //벽 위치 변경
        //     if (moveBackGroundLeft[0].GetComponent<MoveBackGround>().ChangeWall()) //>= 0.999f)
        //     {

        //         //벽 위치 선택적 알고리즘 추가할 부분.
        //         //0번째는 내비두고 1번째 애들을 골라서 선택하면 된다.
        //         GameObject temp = moveBackGroundLeft[1];
        //         moveBackGroundLeft[1] = moveBackGroundLeft[0];
        //         moveBackGroundLeft[0] = temp;

        //         Vector2 tempPosition = moveBackGroundLeft[1].transform.position;
        //         moveBackGroundLeft[1].transform.position = moveBackGroundLeft[0].transform.position;
        //         moveBackGroundLeft[0].transform.position = tempPosition;
        //         //break;
        //     }



        // //좌우 벽 업데이트
        // moveBackGroundRight[0].GetComponent<MoveBackGround>().GameUpdate();
        //     //벽 위치 변경
        //     if (moveBackGroundRight[0].GetComponent<MoveBackGround>().ChangeWall()) //>= 0.999f)
        //     {

        //         //벽 위치 선택적 알고리즘 추가할 부분.
        //         //0번째는 내비두고 1번째 애들을 골라서 선택하면 된다.
        //         GameObject temp = moveBackGroundRight[1];
        //         moveBackGroundRight[1] = moveBackGroundRight[0];
        //         moveBackGroundRight[0] = temp;

        //         Vector2 tempPosition = moveBackGroundRight[1].transform.position;
        //         moveBackGroundRight[1].transform.position = moveBackGroundRight[0].transform.position;
        //         moveBackGroundRight[0].transform.position = tempPosition;
        //         //break;
        //     }




    }//Update () 종료
}
