using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bg_Move : MonoBehaviour //Ground, Sky, Back ��� �̵� �ӵ� ����
{
    public float scrollspeed; 
    float targetOffset;
    Renderer m_Renderer;

    void Start()
    {
        m_Renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        targetOffset += Time.deltaTime * scrollspeed;
        m_Renderer.material.mainTextureOffset = new Vector2(targetOffset, 0);
    }

    public void speedUP()
    {
        scrollspeed -= 0.0005f;
    }
}
