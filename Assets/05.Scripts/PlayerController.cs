using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour //플레이어 스크립트
{
    public checkCollider playerCol;
    public checkCollider groundCol;

    float posY;
    bool isJump = false;

    void Start()
    {
        posY = this.gameObject.transform.position.y;
    }

    void Update() 
    {
        if (Input.GetMouseButtonDown(0)) //클릭할 때마다 점프
        {
            if (playerCol.CheckCollision(groundCol))
            {
                isJump = true;
            }
        }
    }

    public void playPlayer()
    {
        if (isJump)
        {
            transform.position = new Vector3(transform.position.x, posY, this.transform.position.z);
            posY += 0.15f;
            if (transform.position.y >= 0.5f)
            {
                isJump = false;
            }
        }
        if (!isJump)
        {
            if (transform.position.y >= -1.77)
            {
                transform.position = new Vector3(transform.position.x, posY, this.transform.position.z);
                posY -= 0.08f;
            }
        }
    }
}
