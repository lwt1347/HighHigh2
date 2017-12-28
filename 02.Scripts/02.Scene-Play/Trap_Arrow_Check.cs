using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Arrow_Check : TrapCenter
{

    //화살이 발사될 Pos
    [SerializeField]
    private Animator[] arrowStartingAnim;

    [SerializeField]
    private GameObject arrow_1;
    
    //격발 한 번만
    private bool trigger_Flag = true;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (trigger_Flag)
        {
            trigger_Flag = false;
            if (other.CompareTag("Player"))
            {
                for (int i=0; i< arrowStartingAnim.Length; i++)
                {
                    arrowStartingAnim[i].SetTrigger("IsCheck");
                    arrow_1 = Instantiate(arrow_1) as GameObject;
                    arrow_1.transform.position = new Vector2(arrowStartingAnim[i].transform.position.x, arrowStartingAnim[i].transform.position.y);
                }
            }
        }
    }
}
