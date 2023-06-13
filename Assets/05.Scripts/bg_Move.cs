using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bg_Move : MonoBehaviour //Ground, Sky, Back 배경 이동 속도 조작
{
    public float scrollspeed; 
    float targetOffset;
    Renderer m_Renderer;

    void Start()
    {
        scrollspeed = -0.1f;
        m_Renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        targetOffset += Time.deltaTime * scrollspeed;
        m_Renderer.material.mainTextureOffset = new Vector2(targetOffset, 0);
    }

    public void speedUP()
    {
        scrollspeed -= 0.00001f;
    }
}
