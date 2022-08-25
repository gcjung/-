using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultTower : Tower
{
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        Init();
    }
    public override void Init()                             // 1단계 타워
    {
        TowerName = "투석기";
        TowerLevel = 1;
        Damage = 40;
        AttackSpeed = 3.0f;
        AttackRange = "장거리";
        ConstructionCost = 120;
        UpgradeCost = ConstructionCost * 160 / 100;         // 업그레이드비용 건설비용 60%증가
        SellingCost = ConstructionCost * 70 / 100;          // 판매비용은 건설 비용의 70%
        TotalSpentMoney = ConstructionCost;                 // 총 사용 금액은 건설비용
    }

    public override int Upgrade()
    {
        int tempUpgradeCost = UpgradeCost;

        
        if (TowerLevel < MaxTowerLevel)
        {
            TotalSpentMoney += UpgradeCost;                 // 총 사용 금액에 업글비용을 추가해줌
            sprite.sprite = towerLevelImage[TowerLevel - 1];// 타워 초기레벨은 1, 인덱스는 0부터 시작

            TowerLevel++;                                   // 타워 레벨업
            Damage += Damage * 50 / 100;                    // 데미지 증가
            UpgradeCost += UpgradeCost * 60 / 100;          // 업그레이드비용 건설비용 60%증가
            SellingCost = TotalSpentMoney * 70 / 100;       // 판매금액 == 총 사용비용의 70%
            transform.GetChild(0).GetComponent<TowerWeapon>().SetTowerUpgradeInfo();

            return tempUpgradeCost;
        }
        else
        {
            Debug.Log("해당 타워는 MAX LEVEL입니다.");
            return 0;
        }
    }

    public override TowerType GetTowerType()
    {
        return TowerType.Catapult;
    }

}
