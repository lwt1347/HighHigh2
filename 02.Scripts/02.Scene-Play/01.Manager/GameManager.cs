using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    
    //플레이어
    [SerializeField]
    private GameObject player = null;
    
    [SerializeField]
    private GameObject scaffoldGround_RespawnManager = null;

    [SerializeField]
    private GameObject trap_Manager = null;

    //좌우벽 매니저 wallManager [변수명 오류남]
    [SerializeField]
    private GameObject wall_Manager = null;

    [SerializeField]
    private GameObject background_Manager = null;

    [SerializeField]
    private GameObject monster_Manager = null;

    [SerializeField]
    private GameObject nature_Manager = null;

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
        scaffoldGround_RespawnManager.GetComponent<ScaffoldGround_RespawnManager>().RespawnManagerInit(player);
        scaffoldGround_RespawnManager.GetComponent<ScaffoldGround_RespawnManager>().Set_StartCoroutine();

        //함정 매니저 생성
        trap_Manager = Instantiate(trap_Manager) as GameObject;
        trap_Manager.GetComponent<TrapManager>().RespawnManagerInit(player);
        //코루틴을 이곳에서 생성한 오브젝트로 돌려야 한다.
        trap_Manager.GetComponent<TrapManager>().Set_StartCoroutine(scaffoldGround_RespawnManager.GetComponent<ScaffoldGround_RespawnManager>());

        //몬스터 매니저 생성
        monster_Manager = Instantiate(monster_Manager) as GameObject;
        monster_Manager.GetComponent<MonsterManager>().MonsterManagerInit(player);

        //자연재해 매니저 생성
        nature_Manager = Instantiate(nature_Manager) as GameObject;
        nature_Manager.GetComponent<NatureManager>().NatureManagerInit(player);
        nature_Manager.GetComponent<NatureManager>().Set_StartCoroutine();

        //벽 매니저 생성
        wall_Manager = Instantiate(wall_Manager) as GameObject;

        //배경 매니저 생성
        background_Manager = Instantiate(background_Manager) as GameObject;
        

    }//Start() 종료
    
    void Update () {

        //발판 움직임
        scaffoldGround_RespawnManager.GetComponent<ScaffoldGround_RespawnManager>().GameUpdate();
        
        //플레이어 게임 업데이트
        player.GetComponent<Player>().GameUpdate();

        //벽 매니저
        wall_Manager.GetComponent<WallManager>().GameUpdate();

        //배경 매니저
        background_Manager.GetComponent<BackgroundManager>().GameUpdate();

    }//Update () 종료
}
