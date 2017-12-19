using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaffoldGround : Singleton<ScaffoldGround>, IGameObject
{

    [SerializeField]
    private Player player = null;

    [SerializeField]
    private BoxCollider2D boxCollider2D = null;

    public void GameUpdate()
    {

    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        //바닥 사각형 
        if ((player.playerDirctionFlag))
        {
             boxCollider2D.isTrigger = false;
            
        }
        else
        {
            
            boxCollider2D.isTrigger = true;
        }
        
        //Debug.Log("p = " + (player.transform.position.x + player.boxCollider2D.size.x) + ", " + this.transform.position.x);

    }


    private void OnTriggerExit2D(Collider2D other)
    {
        //바닥 사각형 
        
            boxCollider2D.isTrigger = true;
        
    }




}

 