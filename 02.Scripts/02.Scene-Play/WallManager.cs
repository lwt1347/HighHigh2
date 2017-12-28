using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour, IGameObject
{

    //좌측벽
    [SerializeField]
    private GameObject[] leftWall_Soil = null;

    //우측벽
    [SerializeField]
    private GameObject[] rightWall_Soil = null;

  
    
    private void Awake()
    {
        //좌측벽 생성
        for (int i = 0; i < leftWall_Soil.Length; i++)
        {
            leftWall_Soil[i] = Instantiate(leftWall_Soil[i]);
        }
        
            yPositionTemp = leftWall_Soil[0].transform.position.y + (leftWall_Soil[0].GetComponent<BoxCollider2D>().size.y / 2);

        //우측벽 생성
        for (int i = 0; i < rightWall_Soil.Length; i++)
        {
            rightWall_Soil[i] = Instantiate(rightWall_Soil[i]);
        }
        
    }

    //// 값 왼쪽 벽 랜덤 값으로 뽑아 내기.
    private float yPositionTemp;
    private bool tagFlag = true;
    private int randomWall_Left = 0;
    private int randomWall_LeftSub = 0;
    private int randomWall_LeftSubTemp = -1;

    private int randomWall_Right = 0;
    private int randomWall_RightSub = 0;
    private int randomWall_RightSubTemp = -1;

    //게임 업데이트
    public void GameUpdate()
    {
        if (CameraFollow.Instance.transform.position.y >= yPositionTemp - 3 )
        {
            yPositionTemp += leftWall_Soil[0].GetComponent<BoxCollider2D>().size.y;

            //2개가 존재해야한다.
            if (tagFlag) {
                //0,1 번째 벽이 중복되면 안되고 또한 자기 이전의 자신 값도 되면 안된다.
                //0 번째
                //왼쪽벽 생성
                randomWall_LeftSub = randomWall_Left = setWallRandomValue(leftWall_Soil.Length, randomWall_LeftSub,0,0);
                leftWall_Soil[randomWall_Left].transform.position = new Vector2(leftWall_Soil[randomWall_Left].transform.position.x, yPositionTemp - leftWall_Soil[randomWall_Left].GetComponent<BoxCollider2D>().size.y / 2);
                //오른쪽 벽생성
                randomWall_RightSub = randomWall_Right = setWallRandomValue(rightWall_Soil.Length, randomWall_RightSub,0,0);
                rightWall_Soil[randomWall_Right].transform.position = new Vector2(rightWall_Soil[randomWall_Right].transform.position.x, yPositionTemp - rightWall_Soil[randomWall_Right].GetComponent<BoxCollider2D>().size.y / 2);
                
                tagFlag = false;
            } else
            {
                randomWall_LeftSubTemp = randomWall_LeftSub = setWallRandomValue(leftWall_Soil.Length, randomWall_LeftSub, randomWall_LeftSubTemp, 1);
                leftWall_Soil[randomWall_LeftSub].transform.position = new Vector2(leftWall_Soil[randomWall_LeftSub].transform.position.x, yPositionTemp - leftWall_Soil[randomWall_LeftSub].GetComponent<BoxCollider2D>().size.y / 2);

                randomWall_RightSubTemp = randomWall_RightSub = setWallRandomValue(rightWall_Soil.Length, randomWall_RightSub, randomWall_RightSubTemp, 1);
                rightWall_Soil[randomWall_RightSub].transform.position = new Vector2(rightWall_Soil[randomWall_RightSub].transform.position.x, yPositionTemp - rightWall_Soil[randomWall_RightSub].GetComponent<BoxCollider2D>().size.y / 2);
                
                tagFlag = true;
            }
        }
    }

    //좌우 벽에 세울 기둥
    private int returnTemp;
  
    private int setWallRandomValue(int valueA, int valueB, int valueC,  int flag)
    {
        while (true)
        {
            returnTemp = Random.Range(0, valueA);

            if (flag == 0) {
                //0번째 벽 생성
                if (returnTemp == valueB)
                {
                    continue;
                }
            }else if (flag == 1)
            {
                //1번째 벽 생성
                if (returnTemp == valueB || returnTemp == valueC)
                {
                    continue;
                }
            }

            break;
        }
        return returnTemp;
    }


}
