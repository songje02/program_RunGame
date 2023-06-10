using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FixPre : MonoBehaviour
{
    public float speed = 5f;

    public void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
