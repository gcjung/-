using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

    public static GameManager instance=null; // ���� �Ѿ�� ������ ����ǵ��� 
    public Text diamondText;
    public Text statDiaText;
    public Text attackText;
    public Text HPText;
    public Text speedText;
    public Text heroPicLevel;
    public Text heroInfoLevel;
    public Text starValue;

    public int level = 1;
    //HP
    public int warriorHPnow;       //����ü��
    public int warriorHPmax = 100;       //�ִ�ü��
    public int warriorHPup;         //����

    //Atk
    public int warriorAtk;
    public float warriorUltAtk;       //���ӵ�? > �ѹ� ���� 60�� & ������

    private string warriorSpeed = "보통";

    private int diamond;
    private int star;       
    
    private int exp;
    private int expMax;


    #region 프로퍼티
    public int Diamond
    {
        get { return diamond; }
        set { diamond = value; }
    }
    public int Star
    {
        get { return star; }
        set {star = value;}
    }
    public int Exp      
    {
        get { return exp; }
        set {exp = value;}
    }
    public int EXPMax
    {
        get { return expMax; }
        set {expMax = value;}
    }
    #endregion

    public int HP
    {
        get { return warriorHPnow; }
        set
        {
            warriorHPnow = value;
            if (warriorHPnow > warriorHPmax)
                warriorHPnow = warriorHPmax;
            else if (warriorHPnow < 0)
                warriorHPnow = 0;

        }
    }
    public int Atk
    {
        get { return warriorAtk; }
        set { warriorAtk = value; }
    }

    private void Awake()
    {
        warriorHPnow = 100;
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);  // ���� �Ѿ�� object ���� X

        warriorHPup = (int)(warriorHPmax * 0.2);
    }
    void Start()
    {
        exp = 0;
        expMax = 100;
        Diamond = 110;
        warriorAtk = 10;
        warriorUltAtk = 0.1f;

    }

    public void SetTexts()
    {
        if (diamondText == null)
            diamondText = GameObject.FindWithTag("DiamondUI").GetComponent<Text>();
        if (statDiaText == null)
            statDiaText = GameObject.FindWithTag("DiamondUIInfo").GetComponent<Text>();
        if (attackText == null)
            attackText = GameObject.FindWithTag("AttackUI").GetComponent<Text>();
        if (HPText == null)
            HPText = GameObject.FindWithTag("HPUI").GetComponent<Text>();
        if (speedText == null)
            speedText = GameObject.FindWithTag("SpeedUI").GetComponent<Text>();
        if (heroPicLevel == null)
            heroPicLevel = GameObject.FindWithTag("LevelUI").GetComponent<Text>();
        if (heroInfoLevel == null)
            heroInfoLevel = GameObject.FindWithTag("LevelUIInfo").GetComponent<Text>();
        if (starValue == null)
            starValue = GameObject.FindWithTag("StarUI").GetComponent<Text>();

    }
    private void LateUpdate()
    {
        //SetTexts();

        if(diamondText != null)
        {
            diamondText.text = Diamond.ToString();
            statDiaText.text = Diamond.ToString();
            attackText.text = warriorAtk.ToString();    //오류
            HPText.text = warriorHPmax.ToString();        //오류
            speedText.text = warriorSpeed;
            heroPicLevel.text = level.ToString();   //오류
            heroInfoLevel.text = level.ToString();  //오류
            starValue.text = Star.ToString();
        }

    }


}
