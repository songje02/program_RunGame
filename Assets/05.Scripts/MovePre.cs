using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePre : MonoBehaviour
{
    public float UPspeed = 3f; // �̵� �ӵ�
    public float speed = 5f;
    public float maxHeight = -2.5f; // �ִ� ����
    public float minHeight = -3.3f; // �ּ� ����
    private bool movingUp = true; // ���� �̵� ������ ����

    void Update()
    {
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
}