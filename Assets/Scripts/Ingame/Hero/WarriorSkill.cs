using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorSkill : MonoBehaviour
{


    public bool isCool2 = true;
    public bool isCool3 = true;
    public bool isCool4 = true;

    int skill1Level = 1;
    int skill2Level = 1;
    int skill3Level = 1;
    int skill4Level = 1;

    public knight knight;
    knight knight1, knight2, knight3;

    public GameObject skill4;
    public GameObject skill3Effect;
    public GameObject skill4Effect;
    public GameObject skill2Effect;

    
    public HeroMovement hm;
    public Warrior war;
    public CoolTime4 cool4;

    //public SkillManager skillManager;
    //public GameManager gameManager;

    public bool isSkill1On = false;
    public bool isSkill2On = false;

    //스킬1 ui를 위한 버프 받는 병사들 리스트
    List<GameObject> knightsAround = new List<GameObject>();

    public skill4 skill4Script;
    private void Awake()
    {
        //gameManager = FindObjectOfType<GameManager>();
        //skillManager = FindObjectOfType<SkillManager>();
        this.skill1Level = SkillManager.instance.skill1Level;
        this.skill2Level = SkillManager.instance.skill2Level;
        this.skill3Level = SkillManager.instance.skill3Level;
        this.skill4Level = SkillManager.instance.skill4Level;

        WarriorSkillLvSet();
    }


    void Update()
    {
        WarriorSkillSet();

        
    }

    void WarriorSkillLvSet() 
    {
        
        //스킬 1
        knight.buffPercent = knight.buffPercent +(0.1f * (skill1Level-1));

        //스킬 2
        GameManager.instance.warriorHPup = GameManager.instance.warriorHPup +(10 * (skill2Level-1));


        //스킬 3
        knight.KnightHPmax = (int)(knight.knightHPmax * Mathf.Pow((float) 1.1 , skill3Level-1));
        knight.KnightAtk = (int)(knight.KnightAtk * Mathf.Pow((float)1.1, skill3Level - 1));

        //스킬 4
        war.coolTime4 = (int)(war.coolTime4 * Mathf.Pow((float)(0.9),skill4Level-1)); //궁극기 쿨타임 10퍼 줄이기
        cool4.time_cooltime4 = war.coolTime4;

    }



    void WarriorSkillSet()     //스킬1~4
    {
        if (isCool2 && GameManager.instance.warriorHPnow <= 50)
        {
            WarriorSkill2();
            StartCoroutine(CoolTimeCo2());
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isCool3)
            {
                WarriorSkill3();
                StartCoroutine(CoolTimeCo3());
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (isCool4)
            {
                WarriorSkill4();
                StartCoroutine(CoolTimeCo4());
            }
        }
        if(Input.GetKeyDown(KeyCode.W))
        {
            if (isCool4)
            {
                WarriorSkill4();
                StartCoroutine(CoolTimeCo4());
                skill4Script.isSkill4On = true;
            }

        }
    }

    //skill1
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Knight"))
        {
            int HpPlus = (int)((double)knight.knightHPmax * knight.buffPercent);
            int AtkPlus = (int)((double)knight.knightAtk * knight.buffPercent);
            //Debug.Log("knight.buffPercent : " + knight.buffPercent);

            //HP
            collision.GetComponent<knight>().KnightHP += HpPlus;
            //Debug.Log("HpPlus" + HpPlus);
            //Debug.Log("체력 늘림 : " + collision.GetComponent<knight>().KnightHP);
            //ATK
            collision.GetComponent<knight>().KnightAtk += AtkPlus;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Knight"))
        {
            int HpPlus = (int)((double)knight.knightHPmax * knight.buffPercent);
            int AtkPlus = (int)((double)knight.knightAtk * knight.buffPercent);
            //빠져나올 때 +한만큼 빼주는데, 그게 0보다 적으면 그냥 현재 피만큼 냅둠
            //HP
            if (collision.GetComponent<knight>().KnightHP - HpPlus > 0)
            {
                collision.GetComponent<knight>().KnightHP -= HpPlus;
                //Debug.Log("체력 다시 줄임 : " + collision.GetComponent<knight>().KnightHP);
            }
            //ATK
            collision.GetComponent<knight>().KnightAtk -= AtkPlus;

            isSkill1On = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Knight"))
        {
            isSkill1On = true;
        }
    }
    


    

    void WarriorSkill2()
    {
        double warriorHealPer = GameManager.instance.warriorHPup * 0.01;
        int plusHp = (int)(GameManager.instance.warriorHPmax * warriorHealPer);
        GameManager.instance.HP += plusHp;
        skill2Effect.SetActive(true);
        Invoke("Skill2EffectOff", 4f);

    }
    void WarriorSkill3()
    {
        SfxManager.instance.ActiveSkill();

        knight1 = ObjectPool.GetObject();
        knight1.transform.position = transform.position + new Vector3(0.5f, 0f, 0);
        knight1.WorriorSpawnSetting();

        knight2 = ObjectPool.GetObject();
        knight2.transform.position = transform.position + new Vector3(-0.5f, 0f, 0);
        knight2.WorriorSpawnSetting();

        knight3 = ObjectPool.GetObject();
        knight3.transform.position = transform.position + new Vector3(0f, -0.5f, 0);
        knight3.WorriorSpawnSetting();

        skill3Effect.SetActive(true);
        Invoke("Skill3EffectOff", 4f);
    }
    void WarriorSkill4()
    {
        SfxManager.instance.UltimateSkill();
        skill4.SetActive(true);
        skill4Effect.SetActive(true);
    }

    //스킬 이펙트 끄기
    void Skill2EffectOff()
    {
        skill2Effect.SetActive(false);
    }
    void Skill3EffectOff()
    {
        skill3Effect.SetActive(false);
    }



    //스킬별쿨타임
    IEnumerator CoolTimeCo2()
    {
        isCool2 = false;
        isSkill2On = true;
        yield return new WaitForSeconds(war.coolTime2);
        isCool2 = true;
        isSkill2On = false;
    }
    IEnumerator CoolTimeCo3()
    {
        isCool3 = false;
        yield return new WaitForSeconds(war.coolTime3);
        isCool3 = true;
    }
    IEnumerator CoolTimeCo4()
    {
        isCool4 = false;
        yield return new WaitForSeconds(war.coolTime4);
        isCool4 = true;
    }
}
