using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MonsterGround : MonoBehaviour
{
    [SerializeField]
    private TowerSelectUI       towerSelectUI;          // 타워선택UI
    [SerializeField]
    private TowerUpgradeSellUI  towerUpgradeSellUI;     // 타워업글판매UI

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Ground.prevSelectGround != null)        // 이전선택된 Ground가 있다면
            {
                Ground.prevSelectGround.GetComponent<SpriteRenderer>().color = Color.white;

                if (Ground.prevSelectGround.isTowerBuild)
                {
                    if (Ground.prevSelectGround.BuiltTower.GetTowerType() == TowerType.Barrack)
                    {
                        Ground.prevSelectGround.towerAttackRange.enabled = false;
                    }
                }
            }


            towerSelectUI.OffTowerSelectUI();           // 타워선택 UI끄기 
            towerUpgradeSellUI.OffTowerUpSell_UI();     // 타워업글판매 UI끄기
        }
    }

}
