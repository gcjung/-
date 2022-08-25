using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReviveCool : MonoBehaviour
{

    public Image image_fill;
    public float time_cooltime;

    private float time_current;
    private float time_start;
    private bool isEnded = true;

    public Warrior war;
    public InGameManager inGameManager;

    protected virtual void Start()
    {


        time_cooltime = inGameManager.reviveCool;

        Init_UI();
        image_fill.gameObject.SetActive(false);
    }

    protected virtual void Update()
    {
        if (inGameManager.isCoolRevive == true)
        {
            image_fill.gameObject.SetActive(true);
            Trigger_Skill();
        }

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
        Set_FillAmount(0);
        isEnded = true;

        Debug.Log("4Skills Available!");
        inGameManager.isCoolRevive = false;

    }

    private void Trigger_Skill()
    {
        if (!isEnded)
        {
            return;
        }

        Reset_CoolTime();
    }

    private void Reset_CoolTime()
    {

        time_current = time_cooltime;
        time_start = Time.time;
        Set_FillAmount(time_cooltime);
        isEnded = false;
    }
    private void Set_FillAmount(float _value)
    {
        image_fill.fillAmount = _value / time_cooltime;
        string txt = _value.ToString("0.0");


    }
}