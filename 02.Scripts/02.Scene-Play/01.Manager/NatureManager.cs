using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NatureManager : MonoBehaviour
{

    //자연재해 생길 위치
    [SerializeField]
    private GameObject[] posDefault;

    //재해 생성하는 위치가 랜덤으로 변경되어야 한다.
    [SerializeField]
    private Transform tempPosPosition;
    
    //재해 생성 속도
    [SerializeField]
    private float delayTime;

    //재해와 상호작용하기 위한 플레이어
    private GameObject player;

    //재해 리스트
    private List<GameObject> natureList = new List<GameObject>();

    //재해
    [SerializeField]
    private GameObject[] nature;

    private GameObject natureTemp;

    public void Set_StartCoroutine()
    {
        //코루틴 발판 생성
        StartCoroutine(RespawnNature());
    }//Set_StartCoroutine()

    IEnumerator RespawnNature()
    {
        while (true)
        {
            yield return new WaitForSeconds(delayTime);


            NatureRemoved(); //파괴
            MakeNature();

        }
    }//IEnumerator RespawnNature() 종료

    //재해와 상호작용하기 위해 플레이어 정보를 가져온다.
    public void NatureManagerInit(GameObject player)
    {
        this.player = player;
    }//NatureManagerInit(GameObject player) 종료

    private void NatureRemoved()
    {
        natureList.ForEach((target) =>
            {
                if (target.GetComponent<NatureCenter>().removeFlag)
                {
                    natureList.Remove(target);
                    Destroy(target.gameObject);
                }
            });
    }//NatureRemoved() 종료

    void MakeNature()
    {

        
        tempPosPosition.transform.position = new Vector2(posDefault[Random.Range(0, posDefault.Length)].transform.position.x + Random.Range(0, 12), player.transform.position.y + 10);
        natureTemp = GameObject.Instantiate(nature[Random.Range(0, nature.Length)], tempPosPosition.transform.position, tempPosPosition.transform.rotation) as GameObject;
        natureList.Add(natureTemp);




    }//MakeNature() 종료
}
