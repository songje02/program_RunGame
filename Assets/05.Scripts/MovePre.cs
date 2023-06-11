using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePre : MonoBehaviour  //�̵��� ��ֹ� �̵� ��ũ��Ʈ
{
    public float UPspeed = 3f; // �̵� �ӵ�
    public float speed = 5f;
    public float maxHeight = -1.5f; // �ִ� ����
    public float minHeight = -3.3f; // �ּ� ����
    private bool movingUp = true; // ���� �̵� ������ ����

    private void Start()
    {
        UPspeed = 3f;
    }

    void Update()
    {
        speedUP();
        // ���Ʒ��� �̵�
        if (movingUp)
        {
            transform.Translate(Vector3.up * UPspeed * Time.deltaTime);
            if (transform.position.y >= maxHeight)
            {
                movingUp = false;
            }
        }
        else
        {
            transform.Translate(Vector3.down * UPspeed * Time.deltaTime);
            if (transform.position.y <= minHeight)
            {
                movingUp = true;
            }
        }

        // �������� �̵�
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
    public void speedUP()
    {
        speed += 0.01f;
    }
}