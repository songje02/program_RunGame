using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePre : MonoBehaviour
{
    public float UPspeed = 3f; // 이동 속도
    public float speed = 5f;
    public float maxHeight = -2.5f; // 최대 높이
    public float minHeight = -3.3f; // 최소 높이
    private bool movingUp = true; // 위로 이동 중인지 여부

    void Update()
    {
        // 위아래로 이동
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

        // 왼쪽으로 이동
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}