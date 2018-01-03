using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCowAttackMode : MonoBehaviour {

    [SerializeField]
    GameObject Cow;

    [SerializeField]
    private Animator anim;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Cow.GetComponent<MonsterCowMove>().attackMode = true;
            anim.SetTrigger("IsCheck"); 
        }
    }

}
