using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{

    //몬스터 생성 속도
    [SerializeField]
    protected float delayTime;

    [SerializeField]
    private GameObject Monster_Cow;
    
    //몬스터 생성위치
    [SerializeField]
    private GameObject posDefault;

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

            //몬스터 생성
            MakeMonster();
        }
    }//IEnumerator RespawnScaffoldGround() 종료

    private void MakeMonster()
    {
        if (Monster_List.Count < 1) {

            //소 생성
            Monster_Cow = GameObject.Instantiate(Monster_Cow, posDefault.transform.position, posDefault.transform.rotation) as GameObject;
            //플레이어 + 10 y 위치에
            posDefault.transform.position = new Vector2(posDefault.transform.position.x, player.transform.position.y + 10);
            Monster_Cow.GetComponent<MonsterCowMove>().MonsterMoveInit(player);
            Monster_List.Add(Monster_Cow);
        }

    }//MakeMonster() 종료



}
