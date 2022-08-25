using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolTime : MonoBehaviour
{
    public Text text_CoolTime;
    public Image image_fill;
    public float time_cooltime;

    public Warrior war;
    public WarriorSkill warSkill;


    protected virtual void Start()
    {
        Init_UI();
        image_fill.gameObject.SetActive(true);
    }

    protected virtual void Update()
    {
        if(warSkill.isSkill1On)
        {
            image_fill.gameObject.SetActive(false);
        }
        else
            image_fill.gameObject.SetActive(true);
    }

    private void Init_UI()
    {
        image_fill.type = Image.Type.Filled;
        image_fill.fillMethod = Image.FillMethod.Radial360;
        image_fill.fillOrigin = (int)Image.Origin360.Top;
        image_fill.fillClockwise = false;
    }

}
