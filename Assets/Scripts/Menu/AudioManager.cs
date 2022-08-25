using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; 

    [SerializeField]
    private AudioClip[] audioClips;

    private AudioSource audioSource;

    private float volume;

    public float Volume
    { get { return volume; } set { volume = value; } }
    
    
    public void SetMusicVolume(float volume)
    {
        audioSource.volume = volume;
        this.volume = volume;
    }

    private void Awake()
    {

        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);  // 씬이 넘어가도 object 안삭제

        audioSource = GetComponent<AudioSource>();
        Volume = 1f;
        audioSource.volume = Volume;
    }

    public void GoToStageMenu()
    {

        SceneManager.LoadScene("Stage Menu");
        audioSource.clip = audioClips[1];
        audioSource.Play();
    }

    public void GoToStageOne()
    {

        SceneManager.LoadScene("Stage1");
        audioSource.clip = audioClips[2];
        audioSource.Play();
    }


    private void Update()
    {
        if(GameObject.FindWithTag("BGMSlider") != null)
        {
            Volume = GameObject.FindWithTag("BGMSlider").GetComponent<Slider>().value;
            audioSource.volume = Volume;
        }
    }

}
