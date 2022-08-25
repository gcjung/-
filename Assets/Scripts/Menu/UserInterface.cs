using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider HealthBar;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            HealthBar.value -= 0.1f;
        }
    }
}
