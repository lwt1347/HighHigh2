using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorHitCheck : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().DieEffect();
            Debug.Log("플레이어 히트 (메테오)");
        }
    }

}
