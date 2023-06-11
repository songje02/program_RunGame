using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FixPre : MonoBehaviour
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
        speed += 0.005f;
    }
}
