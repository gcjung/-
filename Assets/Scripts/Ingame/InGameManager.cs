using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameManager : MonoBehaviour
{
    [SerializeField]
    private Text text_Gold;
    [SerializeField]
    private Text text_Life;

    public Warrior war;
    public GameObject hero;
    public HeroMovement hm;
    
    public GameObject lose_Popup;
    public Slider EXPslider;

    
    private int gold;
    private int life;
    private int star;

    public bool isCoolRevive;
    public float reviveCool;
    public int Gold
    {
        get { return gold; } 
        set { gold = value;
            text_Gold.text = gold.ToString();
        }
    }
    public int Life 
    { 
        get { return life; } 
        set { life = value;
            text_Life.text = life.ToString();

            if (life <= 0)
            {
                lose_Popup.SetActive(true);
                Time.timeScale = 0;
            }
        } 
    }

    public int InGameStar
    {
        get { return star; }
        set { star = value; }
    }

    private void Awake()
    {
        Gold = 140;
        Life = 20;
        isCoolRevive = false;
        reviveCool = 15f;
    }
    void Update()
    {
        if (war.isDie)
        {
            StartCoroutine(DieHero());
  
            StartCoroutine(reviveCo());
        }

        EXPslider.value = GameManager.instance.Exp;
        EXPslider.maxValue = GameManager.instance.EXPMax;
    }

    IEnumerator DieHero()
    {
        yield return new WaitForSeconds(2f);
        hero.SetActive(false);
    }
    IEnumerator reviveCo()
    {
        war.isDie = false;
        yield return new WaitForSeconds(15f);
        hero.SetActive(true);

        GameManager.instance.HP = GameManager.instance.warriorHPmax;
        war.reviveEffect.SetActive(true);
        isCoolRevive = false;
        Invoke("reviveEffectOff", 3f);
        StartCoroutine(war.FadeIn());

    }
    void reviveEffectOff()
    {
        war.reviveEffect.SetActive(false);
    }
}
