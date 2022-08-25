using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{

    public static SkillManager instance=null; // 씬이 넘어가도 데이터 저장되도록 


    public int skill1Level = 1;
    public int skill2Level = 1;
    public int skill3Level = 1;
    public int skill4Level = 1;

    public Text skill1Text;
    public Text skill2Text;
    public Text skill3Text;
    public Text skill4Text;

    private int diamond;

    public int Diamond
    {
        get { return diamond; }
        set { diamond = value; }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);  // 씬이 넘어가도 object 삭제 X

      skill1Level = 1;
      skill2Level = 1;
      skill3Level = 1;
      skill4Level = 1;




    }
    private void LateUpdate()
    {
        if (skill1Text != null)
        {
            skill1Text.text = skill1Level.ToString();
            skill2Text.text = skill2Level.ToString();
            skill3Text.text = skill3Level.ToString();
            skill4Text.text = skill4Level.ToString();
        }

        
    }
    public void SetTextsSkill()
    {
 
        if (skill1Text == null)
            skill1Text = GameObject.FindWithTag("Skill1Text").GetComponent<Text>();
        if (skill2Text == null)
            skill2Text = GameObject.FindWithTag("Skill2Text").GetComponent<Text>();
        if (skill3Text == null)
            skill3Text = GameObject.FindWithTag("Skill3Text").GetComponent<Text>();
        if (skill4Text == null)
            skill4Text = GameObject.FindWithTag("Skill4Text").GetComponent<Text>();
    }

}
