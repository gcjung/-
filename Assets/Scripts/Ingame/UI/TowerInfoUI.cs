using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TowerInfoUI : MonoBehaviour
{
    private Tower selectedTower;                // 선택한 타워

    [SerializeField]
    private GameObject[]    towerInfoUI;        // 우측하단UI[0- 배럭타워전용, 1- 배럭 외 모든타워 정보용]
    [SerializeField]
    private Image           towerTypeImage;     // 우측하단UI 타워이미지
    [SerializeField]
    private Text[]          towerInfoText;      // 타워 정보[0-타워이름, 1-공격력,2-공속,3-사거리,4-체력(병영타워)]

    public bool isActiveTowerInfoUI;

    private void Awake()
    {
        isActiveTowerInfoUI = false;
    }

    //-----------------------------------------------------------------------------
    // description : 타워정보를 가져와서 텍스트로 출력
    //=============================================================================
    public void GetTowerInfo()
    {
        towerTypeImage.sprite = selectedTower.towerTypeImage;
        
        towerInfoText[0].text = selectedTower.TowerName;
        towerInfoText[1].text = selectedTower.Damage.ToString();
        towerInfoText[2].text = selectedTower.AttackSpeed + "s";
        towerInfoText[3].text = selectedTower.AttackRange;
        towerInfoText[4].text = selectedTower.Hp.ToString();            // 체력 (병영타워전용)
        towerInfoText[5].text = selectedTower.Damage.ToString();        // 공격력 (병영타워전용)
        towerInfoText[6].text = selectedTower.AttackSpeed + "s";        // 공격속도 (병영타워전용)
    }

    //-----------------------------------------------------------------------------
    // description : 타워선택UI On, Ground를 멤버변수에 저장해줌
    //=============================================================================
    public void OnTowerInfoUI(Tower tower)
    {
        this.selectedTower = tower;     // 선택한 땅에 설치된 타워의 정보를 클래스내 멤버변수로 저장
        GetTowerInfo();

        if (tower.GetTowerType() == TowerType.Barrack)
        { 
            towerInfoUI[0].SetActive(true);
            towerInfoUI[1].SetActive(false);
        }
        else
        {
            towerInfoUI[1].SetActive(true);
            towerInfoUI[0].SetActive(false);
        }

        isActiveTowerInfoUI = true;
        gameObject.SetActive(isActiveTowerInfoUI);
    }
    //-----------------------------------------------------------------------------
    // description : "선택된" 타워에 마우스 갖다대면 UI On
    //=============================================================================
    public void OnMouseEnterGround()
    {
        isActiveTowerInfoUI = true;
        gameObject.SetActive(isActiveTowerInfoUI);
    }
    //-----------------------------------------------------------------------------
    // description  : 타워선택UI Off
    //=============================================================================
    public void OffTowerInfoUI()
    {
        isActiveTowerInfoUI = false;
        gameObject.SetActive(isActiveTowerInfoUI);
    }
}
