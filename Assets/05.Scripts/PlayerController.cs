using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour //�÷��̾� ��ũ��Ʈ
{
    public checkCollider playerCol;
    public checkCollider groundCol;

    public float posY;
    bool isJump = false;

    void Start()
    {
        posY = this.gameObject.transform.position.y;
    }

    public void playPlayer()
    {
        if (Input.GetKeyDown(KeyCode.Space)) //Ŭ���� ������ ����
        {
            if (playerCol.CheckCollision(groundCol))
            {
                isJump = true;
            }
        }

        if (isJump)
        {
            transform.position = new Vector3(transform.position.x, posY, transform.position.z);
            posY += 10f * Time.deltaTime;

            if (transform.position.y >= 0.2f)
            {
                isJump = false;
            }
        }
        else
        {
            if (transform.position.y >= -1.77f)
            {
                transform.position = new Vector3(transform.position.x, posY, transform.position.z);
                posY -= 8f * Time.deltaTime;
            }
        }
    }
}
