using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FixPre : MonoBehaviour //고정형 장애물 이동 스크립트
{
    public float speed = 5f;

    private void Start()
    {
        speed = 5f;
    }

    public void Update()
    {
        speedUP();
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    public void speedUP()
    {
        speed += 0.01f;
    }
}
