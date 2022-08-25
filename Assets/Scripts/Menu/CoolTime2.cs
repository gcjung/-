using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolTime2 : MonoBehaviour
{
    public Text text_CoolTime;
    public Image image_fill;
    public float time_cooltime;
    public float time_cooltime2;
    private float time_current;
    private float time_start;
    private bool isEnded = true;

    public Warrior war;
    public WarriorSkill warSkill;
    public GameObject hero;


    protected virtual void Start()
    {
        time_cooltime2 = war.coolTime2;


        Init_UI();
        //image_fill.gameObject.SetActive(true);
    }

    protected virtual void Update()
    {
        if (warSkill.isSkill2On && hero.activeSelf == true && GameManager.instance.warriorHPnow <= 50)
        {
            image_fill.gameObject.SetActive(true);
            Trigger_Skill();
            time_cooltime = time_cooltime2;
        }
        //else
        //    image_fill.gameObject.SetActive(true);
        
        if (isEnded)
            return;
        Check_CoolTime();
    }

    private void Init_UI()
    {
        image_fill.type = Image.Type.Filled;
        image_fill.fillMethod = Image.FillMethod.Radial360;
        image_fill.fillOrigin = (int)Image.Origin360.Top;
        image_fill.fillClockwise = false;
    }

    private void Check_CoolTime()
    {
        time_current = Time.time - time_start;
        if (time_current < time_cooltime)
        {
            Set_FillAmount(time_cooltime - time_current);
        }
        else if (!isEnded)
        {
            End_CoolTime();
            
        }
    }

    private void End_CoolTime()
    {
        Set_FillAmount(50);
        isEnded = true;
        text_CoolTime.gameObject.SetActive(false);
        Debug.Log("2Skills Available!");
    }

    private void Trigger_Skill()
    {
        if (!isEnded)
        {
            //Debug.LogError("Hold On");
            return;
        }

        Reset_CoolTime();
    }

    private void Reset_CoolTime()
    {
        image_fill.gameObject.SetActive(true);
        text_CoolTime.gameObject.SetActive(true);
        time_current = time_cooltime;
        time_start = Time.time;
        Set_FillAmount(time_cooltime);
        isEnded = false;
        
    }
    private void Set_FillAmount(float _value)
    {
        image_fill.fillAmount = _value / time_cooltime;
        string txt = _value.ToString("0.0");
        text_CoolTime.text = txt;
    }   
}
