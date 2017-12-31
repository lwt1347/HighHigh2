using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : Singleton<Player>, IGameObject
{
    [SerializeField]
    private Rigidbody2D rb = null;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private BoxCollider2D boxCollider2D;

    //플레이어 기본 정보
    private int hp = 1;
    private int initHp = 1;
    private float speed = 3.0f;
    private float jump = 400.0f;
    public bool jumpState = true; //카메라가 점프가 불가능한 시점에만 플레이어를 추적한다. //카메라 플레이어 참조

    private float horizontal;
    private float vertical;

    //캐릭터가 보고 있는 방향
    private bool leftSite;
    private bool rightSite;
    

    //캐릭터가 낙하중인지 판단하기 위한 변수
    private bool dirctionFlag = false;

    private void Awake()
    {
        //hp 초기화
        hp = initHp;

        //초기 시작 오른쪽 향함
        DirectionInit(0);
        

    }//private void Awake() 종료
    
    public bool playerDirctionFlag //true = 하강, false = 상승
    {
        set
        {
            dirctionFlag = value;
        }
        get
        {
            return dirctionFlag;
        }
    }//public bool playerDirctionFlag 종료
    
    public void GameUpdate()
    {
        
        //좌우 키보드 입력.
        horizontal = Input.GetAxis("Horizontal");
        
        //x 방향으로 가할 힘
        float xForce = horizontal * speed * Time.deltaTime;
        this.gameObject.transform.Translate(xForce, 0 , 0);
        //rb.AddForce(new Vector2(xForce * 150, 0));
        

        if (jumpState)
        {
            //점프 버튼
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                //땅에 닿으면 한번 위로 올려준다음 Add포스를 사용. 이유: 충돌 판정후에 즉각적으로 올라오지 않고 점프키를 난타하면 점프를 못할 경우 발생.
                this.gameObject.transform.Translate(0, 0.3f, 0);
                //translate 와 AddForce의 차이점 = 전자는 +1 만큼 이동 시키고,
                //후자는 +1만큼의 힘을 가한다.
                rb.AddForce(new Vector2(0, jump));
                jumpState = false;
            }
        }

        ////캐릭터가 낙하중인지 판단한다.
        if (rb.velocity.y < 0)  //하강
        {
            playerDirctionFlag = true;
            if (rb.velocity.y <= -9.5f)
            {
                //중력가속도가 너무 많이 올라가면 ScaffoldGround 를 뚫고 내려감 - 방지
                rb.velocity = new Vector2(rb.velocity.x, -9.5f);
            }
        }
        else if(rb.velocity.y > 0) //상승
        {
            playerDirctionFlag = false;
        }
        
    }//public void GameUpdate() 종료

    //플레이어 y방향 가속도 알아오기
    public float GetPlayerYVelocity()
    {
        return rb.velocity.y;
    }//GetPlayerYVelocity() 종료 

    private void DirectionInit(int direction) //0오른쪽, 1왼쪽
    {
        if (direction == 0)
        {
            leftSite = false;
            rightSite = true;
            anim.SetBool("IsSite", true); //IsSite = true = 오른쪽 방향
        }
        else if(direction == 1)
        {
            leftSite = true;
            rightSite = false;
            anim.SetBool("IsSite", false);
        }
    }//DirectionInit(int direction) 종료



    private void FixedUpdate()
    {
       
        if (Input.GetKey(KeyCode.RightArrow) == true || Input.GetKey(KeyCode.D) == true)
        {
          
            DirectionInit(0);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) == true || Input.GetKey(KeyCode.A) == true)
        {
            DirectionInit(1);
        }

        //오른쪽으로 이동 및 애니메이션 작동.
        if (horizontal > 0)
        {
            anim.SetBool("IsRunRight", true);
        }
        else
        {
            anim.SetBool("IsRunRight", false);
        }

        //왼쪽 방향 애니메이션 작동
        if(horizontal < 0)
        {
            anim.SetBool("IsRunLeft", true);
        }
        else
        {
            anim.SetBool("IsRunLeft", false);
        }
        
        //점프중일때
        if (!jumpState)
        {
            //달리기 애니메이션 종료
            anim.SetBool("IsRunRight", false);
            anim.SetBool("IsRunLeft", false);

            //점프 애니메이션 작동
            if (rightSite) { 
                anim.SetBool("IsJumpRight", true);
            }
            else if (leftSite)
            {
                anim.SetBool("IsJumpLeft", true);
            }
        }
        else
        {
            //점프 애니메이션 종료
            anim.SetBool("IsJumpRight", false);
            anim.SetBool("IsJumpLeft", false);
        }
    }//private void FixedUpdate() 종료
    

    //슈퍼 점프를 막을 수 있다. OnTriggerStay2D()에 접하는 시간은 최대 0.4f
    private void OnTriggerStay2D(Collider2D other)
    {
        if ((other.CompareTag("ScaffoldGround") || other.CompareTag("Ground")) && other.isTrigger == false)
        {
            //y방향 가속도가 0일때 점프가 가능하다.
            if (GetPlayerYVelocity() == 0)
            {   
                //Action
                //땅에 닿아야 점프 가능
                jumpState = true;
            }
        }else if (other.CompareTag("WallScaffoldGround"))
        {
            jumpState = true;
        }
       
    }//OnTriggerStay2D() 종료

   
    private void OnTriggerExit2D(Collider2D other)
    {
        //캐릭터가 낙하 중일때는 점프 할 수 없다.
        jumpState = false;
    }//OnTriggerExit2D(Collider2D other) 종료

   


}
