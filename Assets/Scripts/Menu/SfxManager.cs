using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SfxManager : MonoBehaviour
{

    public static SfxManager instance;

    public AudioSource activeSkill;
    public AudioSource ultimateSkill;
    public AudioSource buildTower;
    public AudioSource destroyTower;
    public AudioSource archerAtk;
    public AudioSource magicAtk;
    public AudioSource catapultAtk;
    public AudioSource fight;
    public AudioSource upgradeSkill;
    public AudioSource waveStart;
    public AudioSource youLose;
    public AudioSource youWin;



    private AudioSource sfxSource;

    private float volume;

    public float Volume
    { get { return volume; } set { volume = value; } }
    
    
    public void SetMusicVolume(float volume)
    {
        sfxSource.volume = volume;
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

        sfxSource = GetComponent<AudioSource>();
        Volume = 1f;
        sfxSource.volume = Volume;
    }

    private void Update()
    {
        if(GameObject.FindWithTag("SfxSlider") != null)
        {
            Volume = GameObject.FindWithTag("SfxSlider").GetComponent<Slider>().value;
            sfxSource.volume = Volume;
        }

    activeSkill.volume = Volume;
    ultimateSkill.volume = Volume;
    buildTower.volume = Volume;
    destroyTower.volume = Volume;
    archerAtk.volume = Volume;
    magicAtk.volume = Volume;
    catapultAtk.volume = Volume;
    fight.volume = Volume;
    upgradeSkill.volume = Volume;
    waveStart.volume = Volume;
    youLose.volume = Volume;
    youWin.volume = Volume;
    upgradeSkill.volume = Volume;

    }

    public void UpgradeSound()
    {
        upgradeSkill.Play();
    }
    public void WaveStart()
    {
        waveStart.Play();
    }
    public void ActiveSkill()
    {
        activeSkill.Play();
    }
    public void UltimateSkill()
    {
        ultimateSkill.Play();
    }
    public void BuildTower()
    {
        buildTower.Play();
    }
    public void DestroyTower()
    {
        destroyTower.Play();
    }
    public void ArcherAtk()
    {
        archerAtk.Play();
    }
    public void MagicAtk()
    {
        magicAtk.Play();
    }
    public void CatapultAtk()
    {
        catapultAtk.Play();
    }
    public void YouLose()
    {
        youLose.Play();
    }
    public void YouWin()
    {
        youWin.Play();
    }
}

