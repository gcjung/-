using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TowerType { Barrack = 0, Archer, Catapult, Wizard }
abstract public class Tower : MonoBehaviour
{
    [SerializeField]
    protected Sprite[] towerLevelImage;     // 레벨별 타워 이미지 2장 
    protected SpriteRenderer sprite;        // 타워의 스프라이트변경을 위함

    public const int MaxTowerLevel = 3;     // 타워 최대 레벨
    
    public Sprite towerTypeImage;           // 우측하단UI 타워이미지
    private string towerName;               // UI에 표시될 타워 이름
    private int towerLevel;                 // 타워 레벨 (LV1 ~ LV3)
    private int damage;                     // 타워 공격력
    private float attackSpeed;              // 타워 공격속도
    private string attackRange;             // 타워 사거리 (문자열)
    private int constructionCost;           // 타워 건설비용
    private int upgradeCost;                // 타워 업그레이드비용
    private int sellingCost;                // 타워 판매비용 ((건설비 + 업글비)의 70%)
    private int totalSpentMoney;            // 타워에 총 사용한 금액(건설비 + 업글비)
    private int hp;                         // 병영타워전용! (병사들 hp)

    public string TowerName
    { get { return towerName; } set { towerName = value; }}
    public int TowerLevel
    { get { return towerLevel; } set { towerLevel = value;}}
    public int Damage
    { get { return damage; } set { damage = value;}}
    public float AttackSpeed
    { get { return attackSpeed; } set { attackSpeed = value;}}
    public string AttackRange
    { get { return attackRange; } set { attackRange = value; }}
    public int ConstructionCost
    { get { return constructionCost; } set { constructionCost = value;}}
    public int UpgradeCost
    { get { return upgradeCost; } set { upgradeCost = value;}}
    public int SellingCost
    { get { return sellingCost; } set { sellingCost = value;}}
    public int TotalSpentMoney
    { get { return totalSpentMoney; } set { totalSpentMoney = value;}}
    public int Hp
    { get { return hp; } set { hp = value; }}

    
    public abstract void Init();
    public abstract int Upgrade();
    public abstract TowerType GetTowerType();


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster") || collision.CompareTag("Player") || collision.CompareTag("Knight"))
        {
            if (collision.transform.position.y > transform.position.y)
                collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder - 1;
            else
                collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder + 1;
        }
    }


}
