using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryScroll : MonoBehaviour
{
    void FixedUpdate()
    {
        if(gameObject.activeSelf == true)
        {
            transform.Translate(Vector2.up * 1.5f);
        }
    }
}
