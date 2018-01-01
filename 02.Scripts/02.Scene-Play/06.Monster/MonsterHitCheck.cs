using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHitCheck : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어 히트");
        }
    }
}
