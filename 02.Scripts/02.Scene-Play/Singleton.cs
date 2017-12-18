using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//타입을 T 로 받는다. UIManager, Manager 에서 사용함
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{

    //싱글톤 기법 - 하나만 생성해서 어디든지 가져올수 있도록 하는 기법
    private static T instance = null;  //다른 스트립트에서 Manger.Instace ~~ 로 호출 가능
    public static T Instance { get { return instance; } }

    void Awake()
    {
        instance = GetComponent<T>();
    }

}