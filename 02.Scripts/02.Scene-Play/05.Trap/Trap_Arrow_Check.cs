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

    public float arrowSpeed;
    
    //격발 한 번만
    private bool trigger_Flag = true;

    private new void Awake()
    {
        trigger_Flag = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (trigger_Flag)
        {
            
            if (other.CompareTag("Player"))
            {
                trigger_Flag = false;
                if (this.CompareTag("Trap-ArrowLeft-Check"))
                {
                    arrowSpeed = 350f;
                }
                else if (this.CompareTag("Trap-ArrowRight-Check"))
                {
                    arrowSpeed = -350f;
                }
               
                for (int i=0; i< arrowStartingAnim.Length; i++)
                {
                    arrowStartingAnim[i].SetTrigger("IsCheck");
                    arrow_1 = Instantiate(arrow_1) as GameObject;

                    if (this.arrowSpeed < 0) {
                        arrow_1.transform.localScale = new Vector2(-1, arrow_1.transform.localScale.y);
                    }
                    arrow_1.GetComponent<Trap_Arrow>().arrowSpeed = this.arrowSpeed;
                    arrow_1.transform.position = new Vector2(arrowStartingAnim[i].transform.position.x, arrowStartingAnim[i].transform.position.y);
                    
                }
            }
        }
    }
}
