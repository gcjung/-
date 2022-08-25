using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lose : MonoBehaviour
{
    private void OnEnable()
    {
        SfxManager.instance.YouLose();
    }
}
