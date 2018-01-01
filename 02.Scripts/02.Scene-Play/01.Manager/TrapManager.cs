using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : ScaffoldGround_RespawnManager
{
    //랜덤 함정 정하기
    private int randomRangeTrap = 0;      
    //바닥 이미지 넓이, 높이
    private float offSetWidth = 0;
    private float offSetHeight = 0;
    private float offSetHeightPosition = 0;
    private int trapListRemoveTemp = 0;
    //함정 개수
    private int forTemp = 0;
    //함정이 설치될 발판
    private int randomTrapScaffold = 0;

    //함정이 몇개 설치될 것인가.
    private int addTrapCountValue = 0;

    //발판 위치 알아오기
    private ScaffoldGround_RespawnManager scaffoldGround_RespawnManager;
    
    //함정 리스트
    private List<GameObject> trapList = new List<GameObject>();

    //발판 템프 변수
    protected GameObject trapCenterTemp;
    
     [SerializeField]
    private Transform addTrapTempPosPosition;

    //함정
    [SerializeField]
    private GameObject[] trap;
    
    IEnumerator RespawnTrapScaffoldGround()
    {
        while (true)
        {
            yield return new WaitForSeconds(delayTime);

           //발판, 함정 제거
            DistroyTrap();
            TrapRemoved();
            //발판 생성
            MakeTrapScaffoldGround();

        }
    }//IEnumerator RespawnScaffoldGround() 종료

    
    public void DistroyTrap()
    {
        //삭제될 발판 체크
        if (trapList.Count > 20)
        {
            trapListRemoveTemp = trapList.Count - 20;
            for (int i = 0; i < trapListRemoveTemp; i++)
            {
                trapList[i].GetComponent<TrapCenter>().setDestroy();
            }
        }
    }//DistroyScaffoldGround() 종료



       //삭제 체크된 발판 삭제
    public void TrapRemoved()
    {
        trapList.ForEach((target) =>
        {
            if (target.GetComponent<TrapCenter>().removeFlag)
            {
                //trapList.Remove(target);
                //Destroy(target.gameObject);
            }
        });
    }//TrapRemoved()종료


    //발판 매니저 상속받음    
    public void Set_StartCoroutine(ScaffoldGround_RespawnManager value)
    {
        //코루틴 발판 생성
        StartCoroutine(RespawnTrapScaffoldGround());

        //GameManager 에서 생성된 ScaffoldGround_RespawnManager를 가져와 발판의 위치를 알아온다.
        scaffoldGround_RespawnManager = value;
    }//Set_StartCoroutine()
    
    

    //함정 생성 함수
    void MakeTrapScaffoldGround()
    {
        StageSet(0, 0);
        forTemp = Random.Range(0, (posDefault.Length/3));

        for (int i = 0; i < forTemp; i++)
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

            //발판 복사할 위치     x위치는 자신의 것, y위치는 부모(ScaffoldGround_RespawnManager)의 것

            randomTrapScaffold = Random.Range(0, posDefault.Length);

            //함정과 바닥이 설치될 pos 위치
            tempPosPosition.position = new Vector2((posDefault[randomTrapScaffold].transform.position.x - 0.2f) + Random.Range(0, 0.4f), 
                (scaffoldGround_RespawnManager.posDefaultTemp[0] - 0.5f) + Random.Range(0, 1.0f));
            
            if (randomTrapScaffold > 1)
            {
                randomRange = Random.Range(stagePerBigScaffoldTemp_Start, stagePerBigScaffoldTemp_End);
            
                //함정이 설치될 바닥 생성
                scaffoldGroundTemp = GameObject.Instantiate(
                        scaffoldGroundSoil[randomRange],
                        tempPosPosition.transform.position,
                        posDefault[i].transform.rotation);

                //이미지 좌우 반전
                if (Random.Range(0, 10) < 5)
                {
                    scaffoldGroundTemp.transform.localScale = new Vector2(scaffoldGroundTemp.transform.localScale.x * -1, scaffoldGroundTemp.transform.localScale.y);
                }

                //함정이 설치될 바닥 생성
                scaffoldGround_RespawnManager.scaffoldGroundList.Add(scaffoldGroundTemp);

                //받아온 플레이어 정보를 각각의 발판에 전달한다.
                scaffoldGround_RespawnManager.scaffoldGroundList[scaffoldGround_RespawnManager.scaffoldGroundList.Count - 1].GetComponent<ScaffoldGround>().ScaffoldGroundInit(player);
            }//   if(randomTrapScaffold != 0) 종료
            

            offSetWidth = scaffoldGroundSoil[randomRange].GetComponent<BoxCollider2D>().size.x;
            offSetHeight = scaffoldGroundSoil[randomRange].GetComponent<BoxCollider2D>().size.y;

            //생성된 바닥에 트랩 생성
            //생성된 바닥의 종류에 따라 함정 개수 및 위치 변경해야된다.
            offSetHeightPosition = tempPosPosition.transform.position.y;

            if (randomTrapScaffold <= 1)
            {
                //0  = 좌, 1 = 우
                //좌, 우측 벽에 석궁 설치
                randomRangeTrap = randomTrapScaffold;
            } else {
                randomRangeTrap = Random.Range(2, trap.Length);
            }

            if (randomRangeTrap == 2) //톱니
            {
                //톱니 하나 설치
                addTrapCountValue = 1;

                //아래 위로
                //스프라이트 크기 알아오기
                //scaffoldGroundTemp.GetComponent<SpriteRenderer>().size.y
                if (50 < Random.Range(0, 100))
                {
                    //위쪽 에 설치
                    offSetHeightPosition = tempPosPosition.transform.position.y + offSetHeight / 2 - (Random.Range(1, 5) / 30f);
                }
                else
                {
                    //아래쪽 설치
                    offSetHeightPosition = tempPosPosition.transform.position.y + offSetHeight / 2 + (Random.Range(1, 5) / 30f) - scaffoldGroundTemp.GetComponent<SpriteRenderer>().size.y;
                }
                
            }
            else if (randomRangeTrap == 3) //창
            {
                //창 1~3개 설치
                addTrapCountValue = Random.Range(1, 4);
                offSetHeightPosition = tempPosPosition.transform.position.y + offSetHeight/2 + 0.33f;
            }
            else if (randomRangeTrap == 4)// 가시
            {
                //가시 1~3개 설치
                addTrapCountValue = Random.Range(1, 4);
                offSetHeightPosition = tempPosPosition.transform.position.y + offSetHeight/2 + 0.1f;
            }

            if (randomTrapScaffold <= 1) {
                //석궁 하나 설치
                addTrapCountValue = 1;
                //석궁은 x 축으로 변함 없음.
                tempPosPosition.transform.position = new Vector2(posDefault[randomTrapScaffold].transform.position.x,
                      offSetHeightPosition);
            }

            AddTrap(i, addTrapCountValue);
           
        }//for (int i = 0; i < forTemp; i++) 종료

    }//MakeTrapScaffoldGround() 종료

    private void AddTrap(int i, int forValue)
    {
        for (int j = 0; j < forValue; j++) { 
            if (randomTrapScaffold > 1)
            {
                //함정 위치 선정
                addTrapTempPosPosition.transform.position = new Vector2(tempPosPosition.transform.position.x - (offSetWidth / 4) + Random.Range(0, offSetWidth/2),
                      offSetHeightPosition);
            }

            trapCenterTemp = GameObject.Instantiate(
                    trap[randomRangeTrap],
                    addTrapTempPosPosition.transform.position,
                    posDefault[i].transform.rotation);

            trapList.Add(trapCenterTemp);
        }

    }


}





