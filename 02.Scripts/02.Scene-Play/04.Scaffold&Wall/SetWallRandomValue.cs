using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWallRandomValue {
    
    //좌우 벽에 세울 기둥
    private static int returnTemp;

    public static int setWallRandomValue(int valueA, int valueB, int valueC, int flag)
    {
        while (true)
        {
            returnTemp = Random.Range(0, valueA);

            if (flag == 0)
            {
                //0번째 벽 생성
                if (returnTemp == valueB)
                {
                    continue;
                }
            }
            else if (flag == 1)
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
