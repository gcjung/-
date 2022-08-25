using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SfxSlider : MonoBehaviour
{
    private SfxManager sfxManager;
    private Slider sfxSlider;

    private void Awake()
    {
        sfxSlider = GetComponent<Slider>();
        sfxManager = FindObjectOfType<SfxManager>();
        sfxSlider.value = sfxManager.Volume;
    }
}
