using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour, IGameObject
{


    //// 값 왼쪽 벽 랜덤 값으로 뽑아 내기.
    protected float yPositionTemp;
    protected bool tagFlag = true;
    private int randomWall_Left = 0;
    private int randomWall_LeftSub = 0;
    private int randomWall_LeftSubTemp = -1;

    private int randomWall_Right = 0;
    private int randomWall_RightSub = 0;
    private int randomWall_RightSubTemp = -1;



    //좌우 벽
    private GameObject[] leftWall = null;
    private GameObject[] rightWall = null;

    //좌측 흙벽
    [SerializeField]
    private GameObject[] leftWall_Soil = null;
   
    //우측 흙벽
    [SerializeField]
    private GameObject[] rightWall_Soil = null;

    //좌측 흙벽
    [SerializeField]
    private GameObject[] leftWall_Brick = null;

    //우측 흙벽
    [SerializeField]
    private GameObject[] rightWall_Brick = null;


    //테마 스테이지 1~4 //흙, 벽, 아이스, 불

    private void Awake()
    {
        //테마 변경
        Change_Thema(0);
        
        yPositionTemp = leftWall[0].transform.position.y + (leftWall[0].GetComponent<BoxCollider2D>().size.y / 2);
        
        
    }

    //좌우 벽 테마 변경
    private void Change_Thema(int value)
    {
        leftWall = null;
        rightWall = null;

        //1 테마일때
        if (value == 0)
        {
            leftWall = new GameObject[leftWall_Soil.Length];    //할당
            for (int i=0; i< leftWall_Soil.Length; i++)
            {
                leftWall[i] = GameObject.Instantiate(leftWall_Soil[i]) as GameObject;
            }

            rightWall = new GameObject[rightWall_Soil.Length];    //할당
            for (int i = 0; i < rightWall_Soil.Length; i++)
            {
                rightWall[i] = GameObject.Instantiate(rightWall_Soil[i]) as GameObject;
            }
        }
        else if (value == 1)
        {
            leftWall = new GameObject[leftWall_Brick.Length];    //할당
            for (int i = 0; i < leftWall_Brick.Length; i++)
            {
                leftWall[i] = GameObject.Instantiate(leftWall_Brick[i]) as GameObject;
            }

            rightWall = new GameObject[rightWall_Brick.Length];    //할당
            for (int i = 0; i < rightWall_Brick.Length; i++)
            {
                rightWall[i] = GameObject.Instantiate(rightWall_Brick[i]) as GameObject;
            }
        }

    }//Change_Thema(int value) 종료

    //게임 업데이트
    public void GameUpdate()
    {
        if (CameraFollow.Instance.transform.position.y >= yPositionTemp - 3 )
        {
            yPositionTemp += leftWall[0].GetComponent<BoxCollider2D>().size.y;

            //2개가 존재해야한다.
            if (tagFlag) {
                //0,1 번째 벽이 중복되면 안되고 또한 자기 이전의 자신 값도 되면 안된다.
                //0 번째
                //왼쪽벽 생성
                randomWall_LeftSub = randomWall_Left = SetWallRandomValue.setWallRandomValue(leftWall.Length, randomWall_LeftSub,0,0);
                leftWall[randomWall_Left].transform.position = new Vector2(leftWall[randomWall_Left].transform.position.x, yPositionTemp - leftWall[randomWall_Left].GetComponent<BoxCollider2D>().size.y / 2);
                //오른쪽 벽생성
                randomWall_RightSub = randomWall_Right = SetWallRandomValue.setWallRandomValue(rightWall.Length, randomWall_RightSub,0,0);
                rightWall[randomWall_Right].transform.position = new Vector2(rightWall[randomWall_Right].transform.position.x, yPositionTemp - rightWall[randomWall_Right].GetComponent<BoxCollider2D>().size.y / 2);
                
                tagFlag = false;
            } else
            {
                randomWall_LeftSubTemp = randomWall_LeftSub = SetWallRandomValue.setWallRandomValue(leftWall.Length, randomWall_LeftSub, randomWall_LeftSubTemp, 1);
                leftWall[randomWall_LeftSub].transform.position = new Vector2(leftWall[randomWall_LeftSub].transform.position.x, yPositionTemp - leftWall[randomWall_LeftSub].GetComponent<BoxCollider2D>().size.y / 2);

                randomWall_RightSubTemp = randomWall_RightSub = SetWallRandomValue.setWallRandomValue(rightWall.Length, randomWall_RightSub, randomWall_RightSubTemp, 1);
                rightWall[randomWall_RightSub].transform.position = new Vector2(rightWall[randomWall_RightSub].transform.position.x, yPositionTemp - rightWall[randomWall_RightSub].GetComponent<BoxCollider2D>().size.y / 2);
                
                tagFlag = true;
            }
        }
    }

    
}
