using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendlyUnit : MonoBehaviour 
{

}

public class Warrior : FriendlyUnit
{
    public HeroMovement hm;
    public bool isDie = false;
    public GameObject reviveEffect;

    SpriteRenderer sp;
    

    public Slider HealthBar;
    public Text textLevel;
    public Text hpTextNow;
    public Text hpTextMax;


    //public int level = 1;
    ////HP
    //public int warriorHPnow = 55;       //ÇöÀçÃ¼·Â
    //public int warriorHPmax = 100;       //ÃÖ´ëÃ¼·Â
    //public int warriorHPup;         //Èú·®
    
    ////Atk
    //public int warriorAtk = 40;
    //public int warriorUltAtk = 2;       //Áö¼Óµô? > ÇÑ¹ø ¾²¸é 60µô & ±¤¿ªµô

    //½ºÅ³ÄðÅ¸ÀÓ
    public float coolTime2;        //ÀÚÈú
    public float coolTime3;       //º´»ç¼ÒÈ¯
    public float coolTime4;         //±Ã±Ø±â - ±¤¿ªµô

    public GameObject lvlupEffect;
    public InGameManager inGameManager;


    private void Awake()
    {
        
        //warriorHPup = (int)((double)warriorHPmax*0.2);

        coolTime2 = 40f;
        coolTime3 = 15f;
        coolTime4 = 45f;
    }
    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        var color = sp.color;
        color.a = 0;
        sp.color = color;

        StartCoroutine(FadeIn());
        reviveEffect.SetActive(true);
        Invoke("reviveEffectOff", 3f);

        
    }


    void Update()
    {
        if (GameManager.instance.warriorHPnow <= 0)
        {
            StartCoroutine(FadeAway());
            isDie = true;
            inGameManager.isCoolRevive = true;
        }

  
        if (GameManager.instance.Exp >= GameManager.instance.EXPMax )
        {
            WarriorLevelUp();
        }


    }
    private void LateUpdate()
    {
        HealthBar.maxValue = GameManager.instance.warriorHPmax;
        textLevel.text = GameManager.instance.level.ToString();
        hpTextNow.text = GameManager.instance.warriorHPnow.ToString();
        hpTextMax.text = GameManager.instance.warriorHPmax.ToString();
        HealthBar.value = GameManager.instance.HP;  
        hpTextNow.text = GameManager.instance.HP.ToString();

       
    }

    //public int HP
    //{
    //    get { return warriorHPnow; }
    //    set
    //    {
    //        if (value > warriorHPmax)
    //            value = warriorHPmax;
    //        else if (value < 0)
    //            value = 0;

    //        warriorHPnow = value;
    //    }
    //}
    //public int Atk
    //{
    //    get { return warriorAtk; }
    //    protected set { warriorAtk = value;}
    //}

    void WarriorLevelUp()
    {
        GameManager.instance.Exp =0;
        GameManager.instance.EXPMax = (int)((double)GameManager.instance.EXPMax * 1.1);
        GameManager.instance.level += 1;
        GameManager.instance.warriorHPmax = (int)((double)GameManager.instance.warriorHPmax *1.1);
        GameManager.instance.Atk = (int)((double)GameManager.instance.warriorAtk * 1.1);
        GameManager.instance.HP = GameManager.instance.warriorHPmax;

        lvlupEffect.SetActive(true);

        Debug.Log("exp max : " + GameManager.instance.EXPMax);
        Debug.Log("lvl : " + GameManager.instance.level);
        Debug.Log("Atk : " + GameManager.instance.Atk);

  
        Invoke("lvlupEffectOff", 1f);

    }
    void lvlupEffectOff()
    {
        lvlupEffect.SetActive(false);

    }
    

    

    void reviveEffectOff()
    {
        reviveEffect.SetActive(false);
    }


    IEnumerator Fade(bool isIn)
    {
        yield return new WaitForSeconds(0.5f);

        float       dir = 1;
        if (!isIn)  dir = -1;

        while (sp.color.a > 0 || sp.color.a < 1)
        {
            var color = sp.color;
            color.a += (0.01f * dir * Time.deltaTime);        //0.01 »ç¶óÁö´Â ¼Óµµ
            sp.color = color;
            yield return null;
        }
    }

    IEnumerator FadeAway()
    {
        yield return new WaitForSeconds(0.5f);        

        while (sp.color.a > 0)
        {
            var color = sp.color;
            color.a -= (0.01f * Time.deltaTime);        //0.01 »ç¶óÁö´Â ¼Óµµ
            sp.color = color;
            yield return null;
        }
    }

    public IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(0.5f);

        while (sp.color.a <= 1)
        {
            var color = sp.color;
            color.a += (5f * Time.deltaTime);
            sp.color = color;
            yield return null;
        }
    }


}
