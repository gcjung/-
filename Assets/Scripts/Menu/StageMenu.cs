using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageMenu : MonoBehaviour
{
    public GameObject settingMenu;
    public GameObject paymentMenu;
    public GameObject heroMenu;
    public GameObject activeSkill;
    public GameObject ultimateSkill;
    public GameObject passiveA;
    public GameObject passiveB;
    public GameObject towerMenu;
    public GameObject archerPic;
    public GameObject barrackPic;
    public GameObject wizardPic;
    public GameObject catopultPic;
    public GameObject archerSkill;
    public GameObject barrackSkill;
    public GameObject wizardSkill;
    public GameObject catopultSkill;
    public GameObject archerInfoA;
    public GameObject barrackInfoB;
    public GameObject wizardInfoW;
    public GameObject catopultInfoC;
    public GameObject archerAText;
    public GameObject archerBText;
    public GameObject archerCText;
    public GameObject archerDText;
    public GameObject barrackAText;
    public GameObject barrackBText;
    public GameObject barrackCText;
    public GameObject barrackDText;
    public GameObject wizardAText;
    public GameObject wizardBText;
    public GameObject wizardCText;
    public GameObject wizardDText;
    public GameObject catopultAText;
    public GameObject catopultBText;
    public GameObject catopultCText;
    public GameObject catopultDText;


    public SkillManager skillManager;
    //public GameManager gameManager;

 
    public void OnClickOpenSettingMenu()
    {
        Debug.Log("설정");
        settingMenu.SetActive(true);
    }

    public void OnClickCloseSettingMenu()
    {
        Debug.Log("닫기");
        settingMenu.SetActive(false);
    }

    public void OnClickOpenPaymentMenu()
    {
        paymentMenu.SetActive(true);
    }

    public void OnClickClosePaymentMenu()
    {
        paymentMenu.SetActive(false);
    }

    //영웅정보 창
    public void OnClickOpenHeroInfo()
    {
        heroMenu.SetActive(true);
    }
    public void OnClickOpenPassiveASkillInfo()
    {
        activeSkill.SetActive(false);
        ultimateSkill.SetActive(false);
        passiveA.SetActive(true);
        passiveB.SetActive(false);
    }
    public void OnClickOpenPassiveBSkillInfo()
    {
        activeSkill.SetActive(false);
        ultimateSkill.SetActive(false);
        passiveA.SetActive(false);
        passiveB.SetActive(true);
    }
    public void OnClickOpenActiveSkillInfo()
    {
        activeSkill.SetActive(true);
        ultimateSkill.SetActive(false);
        passiveA.SetActive(false);
        passiveB.SetActive(false);
    }
    public void OnClickOpenUltimateSkillInfo()
    {
        activeSkill.SetActive(false);
        ultimateSkill.SetActive(true);
        passiveA.SetActive(false);
        passiveB.SetActive(false);
    }
    

    public void OnClickUpgradeSkill1()
    {
        SfxManager.instance.UpgradeSound();
        if (skillManager.skill1Level < 3 && GameManager.instance.Diamond >= 100)
        {
            GameManager.instance.Diamond -= 100;
            skillManager.skill1Level++;
        }
    }
    public void OnClickUpgradeSkill2()
    {
        SfxManager.instance.UpgradeSound();
        if (skillManager.skill2Level < 3 && GameManager.instance.Diamond >= 100)
        {
            GameManager.instance.Diamond -= 100;
            skillManager.skill2Level++;
        }
    }
    public void OnClickUpgradeSkill3()
    {
        SfxManager.instance.UpgradeSound();
        if (skillManager.skill3Level < 3 && GameManager.instance.Diamond >= 100)
        {
            GameManager.instance.Diamond -= 100;
            skillManager.skill3Level++;
        }
    }
    public void OnClickUpgradeSkill4()
    {
        SfxManager.instance.UpgradeSound();
        if (skillManager.skill4Level < 3 && GameManager.instance.Diamond >= 100)
        {
            GameManager.instance.Diamond -= 100;
            skillManager.skill4Level++;
        }
    }
    public void OnClickCloseHeroInfo()
    {
        heroMenu.SetActive(false);
    }

    //타워스킬 창
    public void OnClickOpenTowerSkill()
    {
        towerMenu.SetActive(true);
    }

    public void OnClickSelectArcher()
    {
        archerPic.SetActive(true);
        archerSkill.SetActive(true);
        archerInfoA.SetActive(true);
        barrackPic.SetActive(false);
        barrackSkill.SetActive(false);
        wizardPic.SetActive(false);
        catopultPic.SetActive(false);
        catopultSkill.SetActive(false);
        barrackInfoB.SetActive(false);
        wizardInfoW.SetActive(false);
        catopultInfoC.SetActive(false);
    }
    public void OnClickSelectBarrack()
    {
        archerPic.SetActive(false);
        archerSkill.SetActive(false);
        barrackPic.SetActive(true);
        barrackSkill.SetActive(true);
        wizardPic.SetActive(false);
        wizardSkill.SetActive(false);
        catopultPic.SetActive(false);
        catopultSkill.SetActive(false);
        archerInfoA.SetActive(false);
        barrackInfoB.SetActive(true);
        wizardInfoW.SetActive(false);
        catopultInfoC.SetActive(false);
    }
    public void OnClickSelectWizard()
    {
        archerPic.SetActive(false);
        archerSkill.SetActive(false);
        barrackPic.SetActive(false);
        barrackSkill.SetActive(false);
        wizardPic.SetActive(true);
        wizardSkill.SetActive(true);
        catopultPic.SetActive(false);
        catopultSkill.SetActive(false);
        archerInfoA.SetActive(false);
        barrackInfoB.SetActive(false);
        wizardInfoW.SetActive(true);
        catopultInfoC.SetActive(false);
    }
    public void OnClickSelectCatopult()
    {
        archerPic.SetActive(false);
        archerSkill.SetActive(false);
        barrackPic.SetActive(false);
        barrackSkill.SetActive(false);
        wizardPic.SetActive(false);
        wizardSkill.SetActive(false);
        catopultPic.SetActive(true);
        catopultSkill.SetActive(true);
        archerInfoA.SetActive(false);
        barrackInfoB.SetActive(false);
        wizardInfoW.SetActive(false);
        catopultInfoC.SetActive(true);
    }


    //타워스킬설명
    public void OnClickOpenArcherSkillAInfo()
    {
        archerAText.SetActive(true);
        archerBText.SetActive(false);
        archerCText.SetActive(false);
        archerDText.SetActive(false);
    }
    public void OnClickOpenArcherSkillBInfo()
    {
        archerAText.SetActive(false);
        archerBText.SetActive(true);
        archerCText.SetActive(false);
        archerDText.SetActive(false);
    }

    public void OnClickOpenArcherSkillCInfo()
    {
        archerAText.SetActive(false);
        archerBText.SetActive(false);
        archerCText.SetActive(true);
        archerDText.SetActive(false);
    }
    public void OnClickOpenArcherSkillDInfo()
    {
        archerAText.SetActive(false);
        archerBText.SetActive(false);
        archerCText.SetActive(false);
        archerDText.SetActive(true);
    }

    public void OnClickOpenBarrackSkillAInfo()
    {
        barrackAText.SetActive(true);
        barrackBText.SetActive(false);
        barrackCText.SetActive(false);
        barrackDText.SetActive(false);
    }
    public void OnClickOpenBarrackSkillBInfo()
    {
        barrackAText.SetActive(false);
        barrackBText.SetActive(true);
        barrackCText.SetActive(false);
        barrackDText.SetActive(false);
    }
    public void OnClickOpenBarrackSkilllCInfo()
    {
        barrackAText.SetActive(false);
        barrackBText.SetActive(false);
        barrackCText.SetActive(true);
        barrackDText.SetActive(false);
    }
    public void OnClickOpenBarrackSkillDInfo()
    {
        barrackAText.SetActive(false);
        barrackBText.SetActive(false);
        barrackCText.SetActive(false);
        barrackDText.SetActive(true);
    }

    public void OnClickOpenWizardSkillAInfo()
    {
        wizardAText.SetActive(true);
        wizardBText.SetActive(false);
        wizardCText.SetActive(false);
        wizardDText.SetActive(false);
    }
    public void OnClickOpenWizardSkillBInfo()
    {
        wizardAText.SetActive(false);
        wizardBText.SetActive(true);
        wizardCText.SetActive(false);
        wizardDText.SetActive(false);
    }
    public void OnClickOpenWizardSkillCInfo()
    {
        wizardAText.SetActive(false);
        wizardBText.SetActive(false);
        wizardCText.SetActive(true);
        wizardDText.SetActive(false);
    }
    public void OnClickOpenWizardSkillDInfo()
    {
        wizardAText.SetActive(false);
        wizardBText.SetActive(false);
        wizardCText.SetActive(false);
        wizardDText.SetActive(true);
    }

    public void OnClickOpenCatopultSkillAInfo()
    {
        catopultAText.SetActive(true);
        catopultBText.SetActive(false);
        catopultCText.SetActive(false);
        catopultDText.SetActive(false);
    }
    public void OnClickOpenCatopultSkillBInfo()
    {
        catopultAText.SetActive(false);
        catopultBText.SetActive(true);
        catopultCText.SetActive(false);
        catopultDText.SetActive(false);
    }
    public void OnClickOpenCatopultSkillCInfo()
    {
        catopultAText.SetActive(false);
        catopultBText.SetActive(false);
        catopultCText.SetActive(true);
        catopultDText.SetActive(false);
    }
    public void OnClickOpenCatopultSkillDInfo()
    {
        catopultAText.SetActive(false);
        catopultBText.SetActive(false);
        catopultCText.SetActive(false);
        catopultDText.SetActive(true);
    }




    public void OnClickUpgradeTowerSkill()
    {
        Debug.Log("타워업글");
        SfxManager.instance.UpgradeSound();
    }
    public void OnClickCloseTowerSkill()
    {
        towerMenu.SetActive(false);
    }


    public void OnClickEnterStage()
    {
        Debug.Log("스테이지 입장");
        SceneManager.LoadScene("Stage1");
        AudioManager.instance.GoToStageOne();
    }

    public void OnClickQuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
