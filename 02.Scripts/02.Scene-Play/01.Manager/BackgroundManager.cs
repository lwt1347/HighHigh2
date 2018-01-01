using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour, IGameObject
{
    //: WallManager
    private int randomBackground = 0;
    private int randomBackground_Sub = 0;
    private int randomBackground_SubTemp = 0;

    
    private GameObject[] backGrouind = null;

    [SerializeField]
    private GameObject[] backGrouind_Soil = null;

    [SerializeField]
    private GameObject[] backGrouind_Brisk = null;

    protected float yPositionTemp;
    protected bool tagFlag = true;

    private void Awake()
    {

        //테마 변경
        Change_Thema(0);

        //배경화면 생성
        yPositionTemp = backGrouind[0].transform.position.y + backGrouind[0].GetComponent<BoxCollider2D>().size.y/2;

    }

    private void Change_Thema(int value)
    {
        backGrouind = null;
        if (value == 0)
        {
            backGrouind = new GameObject[backGrouind_Soil.Length];    //할당
            for (int i = 0; i < backGrouind_Soil.Length; i++)
            {
                backGrouind[i] = GameObject.Instantiate(backGrouind_Soil[i]) as GameObject;
            }
        }else if (value == 1)
        {
            backGrouind = new GameObject[backGrouind_Brisk.Length];    //할당
            for (int i = 0; i < backGrouind_Brisk.Length; i++)
            {
                backGrouind[i] = GameObject.Instantiate(backGrouind_Brisk[i]) as GameObject;
            }
        }
    }//Change_Thema(int value) 종료

    public void GameUpdate()
    {
        if (CameraFollow.Instance.transform.position.y >= yPositionTemp - 3)
        {
            yPositionTemp += backGrouind[0].GetComponent<BoxCollider2D>().size.y;

            //2개가 존재해야한다.
            if (tagFlag)
            {
                //0,1 번째 벽이 중복되면 안되고 또한 자기 이전의 자신 값도 되면 안된다.
                //0 번째

                randomBackground_Sub = randomBackground = SetWallRandomValue.setWallRandomValue(backGrouind.Length, randomBackground_Sub, 0, 0);
                backGrouind[randomBackground].transform.position = new Vector3(backGrouind[randomBackground].transform.position.x, yPositionTemp - backGrouind[0].GetComponent<BoxCollider2D>().size.y / 2, 1);
               
               
                tagFlag = false;
            }
            else
            {
                randomBackground_SubTemp = randomBackground_Sub = SetWallRandomValue.setWallRandomValue(backGrouind.Length, randomBackground_Sub, randomBackground_SubTemp, 1);
                backGrouind[randomBackground_Sub].transform.position = new Vector3(backGrouind[randomBackground_Sub].transform.position.x, yPositionTemp - backGrouind[0].GetComponent<BoxCollider2D>().size.y / 2, 1);

            
                tagFlag = true;
            }
        }
    }

}
