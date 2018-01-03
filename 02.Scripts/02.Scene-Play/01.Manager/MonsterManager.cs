using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{

    //방향, 힘
    protected string dist = "Left";
    protected float xForce;
    protected Vector3 moveVelocity = Vector3.zero;
    
    //몬스터 return temp
    private int monsterCount = 0;
    private int cowCount = 0;
    private int birdCount = 0;

    //몬스터 생성 속도
    [SerializeField]
    protected float delayTime;

    [SerializeField]
    private GameObject Monster_Cow;    

    [SerializeField]
    private GameObject Monster_Bird;

    //직접 변경시 버그 발생
    private GameObject Monster_Temp;

    //몬스터 생성위치
    [SerializeField]
    private GameObject posDefault;
    [SerializeField]
    private GameObject posTemp;

    List<GameObject> Monster_List = new List<GameObject>();

    //몬스터와 상호작용하기 위한 플레이어
    protected GameObject player;
    
    //몬스터와 상호작용하기 위해 플레이어 정보를 가져온다.
    public void MonsterManagerInit(GameObject player)
    {
        this.player = player;

        //코루틴 발판 생성
        StartCoroutine(RespawnMonster());

    }//MonsterManagerInit(GameObject player) 종료
    
    IEnumerator RespawnMonster()
    {
        while (true)
        {
            yield return new WaitForSeconds(delayTime);

            //몬스터 제거
            MonsterRemoved();

            //몬스터 생성
            MakeMonster();
        }
    }//IEnumerator RespawnScaffoldGround() 종료

    //몬스터 삭제
    public void MonsterRemoved()
    {
        Monster_List.ForEach((target) =>
        {
            if (target.GetComponent<MonsterCenter>().removeFlag)
            {
                Monster_List.Remove(target);
                Destroy(target.gameObject);
            }

        });

    }//Removed(ScaffoldGround target) 종료

    private void MakeMonster()
    {
      

        if (CountMonsterType("Cow") < 1) {

            //소 생성
            posTemp.transform.position = new Vector2(posDefault.transform.position.x - 3 + Random.Range(0, 6), player.transform.position.y + 10);
            Monster_Temp = GameObject.Instantiate(Monster_Cow, posTemp.transform.position, posTemp.transform.rotation) as GameObject;
            //플레이어 + 10 y 위치에
            Monster_Temp.GetComponent<MonsterCowMove>().MonsterMoveInit(player);
            Monster_List.Add(Monster_Temp);
        }

        if (CountMonsterType("Bird") < 1)
        {

            posTemp.transform.position = new Vector2(posDefault.transform.position.x, player.transform.position.y + 10);
            //갈매기 생성
            Monster_Temp = GameObject.Instantiate(Monster_Bird, posTemp.transform.position, posTemp.transform.rotation) as GameObject;
            Monster_List.Add(Monster_Temp);
        }
        

    }//MakeMonster() 종료
    
    //몬스터의 종류별 몇개인지 알아온다.
    private int CountMonsterType(string value)
    {
        for (int i=0; i< Monster_List.Count; i++)
        {
            if (Monster_List[i].GetComponent<MonsterCowMove>())
            {
                cowCount++;
            }else if (Monster_List[i].GetComponent<MonsterBirdMove>())
            {
                birdCount++;
            }
        }
        monsterCount = 0;
        if (value == "Cow")
        {
            monsterCount = cowCount;
        }
        else if (value == "Bird")
        {
            monsterCount = birdCount;
        }
        cowCount = 0;
        birdCount = 0;

        return monsterCount;
    }//CountMonsterType(string value)종료

    //캐릭터 움직일 방향 및 이미지 모양
    protected void MonsterMoveDirection()
    {
        if (dist == "Left")
        {
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (dist == "Right")
        {
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }//MonsterMoveDirection()

}
