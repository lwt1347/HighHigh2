using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaffoldGround_RespawnManager : MonoBehaviour, IGameObject
{
    //발판이 생길 위치
    [SerializeField]
    protected GameObject[] posDefault;
    
    public float[] posDefaultTemp;
    
    //발판 흙
    [SerializeField]
    protected GameObject[] scaffoldGroundSoil;

    //발판 벽돌
    [SerializeField]
    protected GameObject[] scaffoldGroundBrick;

    //발판 생성하는 위치가 랜덤으로 변경되어야 한다.
    [SerializeField]
    protected Transform tempPosPosition;

    //스테이지에서 선택될 발판 템프변수
    protected GameObject scaffoldGroundTemp;

    //발판과 상호작용하기 위한 플레이어
    protected GameObject player;

    //발판 리스트
    public List<GameObject> scaffoldGroundList = new List<GameObject>();

  

    //발판 생성 속도
    [SerializeField]
    protected float delayTime;

    //발판 선택 랜덤 변수
    protected int randomRange;

    //큰 발판 생성 확률
    protected int stagePerBigScaffold = 0;
    protected int stagePerBigScaffoldTemp_Start = 0;
    protected int stagePerBigScaffoldTemp_End = 0;
    //0 흙, 1벽돌, 2얼음, 3불

    //발판 리스트 비움
    protected void Awake()
    {
        scaffoldGroundList.ForEach(x => ScaffoldGroundRemoved());
    }//Awake() 종료

    protected void Start()
    {
        
        //발판 생성하는 pos의 위치를 옴겨준다. pos 자체의 위치를 옮기게 되면 다음 게임에서 옮겨진 위치에서 시작한다.
        posDefaultTemp = new float[posDefault.Length];
        
        for (int i=0; i< posDefault.Length; i++)
        {
            posDefaultTemp[i] = posDefault[i].transform.position.y;
        }
    }//Start() 종료

    //함정이랑 별개 작동
    public void Set_StartCoroutine()
    {
        //코루틴 발판 생성
        //InvokeRepeating("MakeScaffoldGround", 1.0f, delayTime);
        StartCoroutine(RespawnScaffoldGround());
    }//Set_StartCoroutine()

    IEnumerator RespawnScaffoldGround()
    {
        while (true)
        {
            yield return new WaitForSeconds(delayTime);
            
            if (CameraFollow.Instance.setNewPosition) {
                //TrapManager 에서도 사용

                //발판 생성
                ScaffoldGroundRemoved(); //파괴
                MakeScaffoldGround();
            }
        }
    }//IEnumerator RespawnScaffoldGround() 종료

    //발판 삭제
    public void ScaffoldGroundRemoved()
    {
        scaffoldGroundList.ForEach((target) =>
        {
            if (target.GetComponent<ScaffoldGround>().removeFlag) {
                scaffoldGroundList.Remove(target);
                Destroy(target.gameObject);
            }

        });

    }//Removed(ScaffoldGround target) 종료

    //발판과 상호작용하기 위해 플레이어 정보를 가져온다.
    public void RespawnManagerInit(GameObject player)
    {
        this.player = player;
    }//RespawnManagerInit(GameObject player) 종료

    //올라간 미터에 따라 난이도 증가
  
    //실제 발판을 더할 함수 //발판 추가와 함정 추가의 루틴이 달라진다.
    void MakeScaffoldGround()
    {
        StageSet(0, 0);
        for (int i = 0; i < posDefault.Length; i++)
        {
            //총 11개
            //발판을 pos 에 pos 방향으로 복제한다.
            if (stagePerBigScaffold > Random.Range(0, 100))
            {
                //큰 발판
                stagePerBigScaffoldTemp_Start = 0;
                stagePerBigScaffoldTemp_End = 10;
            }
            else
            {
                //작은 발판
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

            //이미지 좌우 반전
            if (Random.Range(0, 10) < 5)
            {
                scaffoldGroundTemp.transform.localScale = new Vector2(scaffoldGroundTemp.transform.localScale.x * -1, scaffoldGroundTemp.transform.localScale.y);
            }

            scaffoldGroundList.Add(scaffoldGroundTemp);

            //받아온 플레이어 정보를 각각의 발판에 전달한다.
            scaffoldGroundList[scaffoldGroundList.Count - 1].GetComponent<ScaffoldGround>().ScaffoldGroundInit(player);
        }
    }//AddScaffoldGround() 종료


    //불가능 프로토콜 -> 점프 가 불가능한 시점에 발판 하나 생기도록 함 [최대 점프높이 측정해서 ]


    //스테이지 선택
    protected void StageSet(int stage, float meterPer)
    {
        //SGBlock1-1 ~ 1-10 넓은 발판
        //SGBlock1-11 ~ 18 얇은 발판

        //현재 높이 선택
        if (meterPer < 100){ //큰 발판 11개중 80퍼
            stagePerBigScaffold = 80;
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
        
        
    }//GameUpdate() 종료

}

