using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaffoldGround_RespawnManager : MonoBehaviour, IGameObject
{

    //발판이 생길 위치
    [SerializeField]
    private GameObject[] posDefault;
    private float[] posDefaultTemp;

    //랜덤으로 생성되는 발판들의 길이를 검사해서 최대거리가 넘어가면 발판 하나를 더 생성한다.
    private GameObject[] posCheckTemp;
    private float[] posCheckTempOffSet; //발판 이미지의 크기를 알아온다.

    //발판 흙
    [SerializeField]
    private ScaffoldGround[] scaffoldGroundSoil;

    //발판 벽돌
    [SerializeField]
    private ScaffoldGround[] scaffoldGroundBrick;

    //발판 생성하는 위치가 랜덤으로 변경되어야 한다.
    [SerializeField]
    private Transform tempPosPosition;

   

    //스테이지에서 선택될 발판 템프변수
    private ScaffoldGround scaffoldGroundTemp; 

    //발판과 상호작용하기 위한 플레이어
    private GameObject player;

    //발판 리스트
    private List<ScaffoldGround> scaffoldGroundList = new List<ScaffoldGround>();
    
    //발판 생성 속도
    public float delayTime;

    //발판 선택 랜덤 변수
    private int randomRange;

    //큰 발판 생성 확률
    private int stagePerBigScaffold = 0;
    private int stagePerBigScaffoldTemp_Start = 0;
    private int stagePerBigScaffoldTemp_End = 0;
    //0 흙, 1벽돌, 2얼음, 3불

    //발판 리스트 비움
    private void Awake()
    {
        scaffoldGroundList.ForEach(x => ScaffoldGroundRemoved());
    }//Awake() 종료
    
    void Start()
    {
        //코루틴 발판 생성
        //InvokeRepeating("MakeScaffoldGround", 1.0f, delayTime);
        StartCoroutine(RespawnScaffoldGround());

        //발판 생성하는 pos의 위치를 옴겨준다. pos 자체의 위치를 옮기게 되면 다음 게임에서 옮겨진 위치에서 시작한다.
        posDefaultTemp = new float[posDefault.Length];
        posCheckTempOffSet = new float[posDefault.Length];
        posCheckTemp = new GameObject[posDefault.Length];

        for (int i=0; i< posDefault.Length; i++)
        {
            posDefaultTemp[i] = posDefault[i].transform.position.y;
            posCheckTemp[i] = new GameObject();
        }

       




    }//Start() 종료
    
    IEnumerator RespawnScaffoldGround()
    {
        while (true)
        {
            yield return new WaitForSeconds(delayTime);

            //발판 생성
            MakeScaffoldGround();

            


        }
    }//IEnumerator RespawnScaffoldGround() 종료

    //발판 삭제
    public void ScaffoldGroundRemoved()
    {
        scaffoldGroundList.ForEach((target) =>
        {
            if (target.removeFlag) {
                //scaffoldGroundList.Remove(target);
                //Destroy(target.gameObject);
            }

        });

    }//Removed(ScaffoldGround target) 종료

    //발판과 상호작용하기 위해 플레이어 정보를 가져온다.
    public void scaffoldGround_RespawnManagerInit(GameObject player)
    {
        this.player = player;
    }//scaffoldGround_RespawnManagerInit(GameObject player) 종료

    //올라간 미터에 따라 난이도 증가
    int temp = 0;
    void MakeScaffoldGround()
    {
       
        //아랫발판 삭제 루틴
        if (scaffoldGroundList.Count > 50)
        {
            temp = scaffoldGroundList.Count - 50;
            for (int i=0; i < temp; i++)
            {
                scaffoldGroundList[i].setDestroy();
            }
        }

        StageSet(0, 0);
        for (int i = 0; i < posDefault.Length; i++)
        {

            //총 11개
            //발판을 pos 에 pos 방향으로 복제한다.
            if (stagePerBigScaffold > Random.Range(0, 100))
            {
                stagePerBigScaffoldTemp_Start = 0;
                stagePerBigScaffoldTemp_End = 10;
            }else
            {
                stagePerBigScaffoldTemp_Start = 10;
                stagePerBigScaffoldTemp_End = scaffoldGroundSoil.Length - 1;
            }

            
            //발판 복사할 위치
            tempPosPosition.position = new Vector2(posDefault[i].transform.position.x, (posDefaultTemp[i] - 0.5f) + Random.Range(0, 1.0f));

            
            //발판 위치 변경
            posDefaultTemp[i] = posDefaultTemp[i] += 3;

            randomRange = Random.Range(stagePerBigScaffoldTemp_Start, stagePerBigScaffoldTemp_End);
            scaffoldGroundTemp = GameObject.Instantiate(
                    scaffoldGroundSoil[randomRange],
                    tempPosPosition.transform.position,
                    posDefault[i].transform.rotation);

            //발판 사이 거리측정 -> 불가능한곳 없도록
            posCheckTemp[i].transform.position = tempPosPosition.position;
            posCheckTempOffSet[i] = scaffoldGroundSoil[randomRange].GetComponent<BoxCollider2D>().size.x;

            //이미지 좌우 반전
            if (Random.Range(0,10) < 5)
            {
                scaffoldGroundTemp.transform.localScale = new Vector2(scaffoldGroundTemp.transform.localScale.x * -1, scaffoldGroundTemp.transform.localScale.y);
            }

            scaffoldGroundList.Add(scaffoldGroundTemp);
            
            //받아온 플레이어 정보를 각각의 발판에 전달한다.
            scaffoldGroundList[scaffoldGroundList.Count-1].ScaffoldGroundInit(player);
        }

    }//MakeScaffoldGround() 종료
    



    //불가능 프로토콜 -> 점프 가 불가능한 시점에 발판 하나 생기도록 함 [최대 점프높이 측정해서 ]


    //스테이지 선택
    private void StageSet(int stage, float meterPer)
    {
        //SGBlock1-1 ~ 1-10 넓은 발판
        //SGBlock1-11 ~ 18 얇은 발판

        //현재 높이 선택
        if (meterPer < 100){ //큰 발판 11개중 80퍼
            stagePerBigScaffold = 20;
        }
        else if (meterPer < 200){//60퍼
            stagePerBigScaffold = 60;
        }
        else if (meterPer < 300){ //40퍼
            stagePerBigScaffold = 40;
        }
        else if (meterPer >= 400){//20퍼
            stagePerBigScaffold = 20;
        }
    }//StageSet() 종료
    

    public void GameUpdate()
    {

        //발판 삭제
        ScaffoldGroundRemoved();
        
    }//GameUpdate() 종료

}

