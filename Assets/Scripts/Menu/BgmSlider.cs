using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BgmSlider : MonoBehaviour
{
    private AudioManager audioManager;
    private Slider bgmSlider;

    private void Awake()
    {
        bgmSlider = GetComponent<Slider>();
        audioManager = FindObjectOfType<AudioManager>();
        bgmSlider.value = audioManager.Volume;
    }
}
