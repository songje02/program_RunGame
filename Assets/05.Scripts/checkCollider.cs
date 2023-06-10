using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkCollider : MonoBehaviour
{
    public GameObject this_val;
    public Vector2 size; 
    public string objectname;

    public void UpdateSelf()
    {
        this_val = gameObject;
    }

    public bool CheckCollision(checkCollider other)
    {
        Vector2 thisMin = (Vector2)this_val.transform.position - size / 2; 
        Vector2 thisMax = (Vector2)this_val.transform.position + size / 2;
        Vector2 otherMin = (Vector2)other.transform.position - other.size / 2; 
        Vector2 otherMax = (Vector2)other.transform.position + other.size / 2; 

        if (thisMax.x > otherMin.x && thisMin.x < otherMax.x &&
            thisMax.y > otherMin.y && thisMin.y < otherMax.y)
        {
            objectname = other.gameObject.name;
            //Debug.Log(objectname);
            return true; //충돌함
        }

        objectname = "";
        return false; //충돌 안 함
    }
  
}
