using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : NatureCenter
{
    [SerializeField]
    private GameObject redLine;
    private float scaleX = 0f;

    private void Start()
    {
        scaleX = redLine.transform.localScale.x;
    }

    private void Update()
    {
        redLine.transform.localScale = new Vector2(scaleX, redLine.transform.localScale.y);
        scaleX -= 0.015f;
        if (scaleX <= 0)
        {
            scaleX = 0;
        }
    }

}
