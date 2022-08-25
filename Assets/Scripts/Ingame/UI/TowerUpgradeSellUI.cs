using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum FuncNum { Upgrade = 0, Sell, Move } // Move(병영타워전용)

public class TowerUpgradeSellUI : MonoBehaviour
{
    [SerializeField]
    private GameObject[]    checkButton;            // 타워 선택버튼 배열
    private bool[]          isCheck;                // 타워 선택시 한번 더 확인

    [SerializeField]
    private GameObject      towerInfo;              // 타워선택창 가운데 타워정보텍스트
    [SerializeField]
    private Text[]          towerInfoText;          // 타워정보(UI) [0-타워이름, 1-타워설명, 2-공격력, 3-공속, 4-공격사거리, 5-골드]

    [SerializeField]
    private GameObject      knightMoveUI;
    private Ground          ground;                 // 선택한 땅 정보
    private Tower           builtTower;

    public static bool      isActiveTowerUpSellUI;  // UI가 사용에 관한 Flag

    private void Awake()
    {
        isCheck = new bool[3] {false,false,false};
        isActiveTowerUpSellUI = false;
    }

    public void OnClickUpgrade()        // 업그레이드 버튼 클릭시 실행
    {
        SelectFunction(FuncNum.Upgrade);
    }
    public void OnClickSell()           // 판매 버튼 클릭시 실행
    {
        SelectFunction(FuncNum.Sell);
    }
    public void OnClickMove()           // Move버튼 클릭시 실행
    {
        SelectFunction(FuncNum.Move);
    }

    //-----------------------------------------------------------------------------
    // description  : 업글 or 판매 선택시 한번 더 체크하고 기능수행
    //=============================================================================
    public void SelectFunction(FuncNum funcNum)
    {
        int selectNum = (int)funcNum;

        if (isCheck[selectNum] == true)     // 체크상태 (두번째 체크)
        {
            for (int i = 0; i < checkButton.Length; i++)    // 체크상태 모두 false로 변경
            {
                isCheck[i] = false;                         
            }

            checkButton[selectNum].SetActive(false);        // 체크표시 해제

            if (selectNum == (int)FuncNum.Sell)
            {
                ground.SellTower();                         // 선택한 타워 판매
            }
            else if (selectNum == (int)FuncNum.Upgrade)
            {
                ground.UpgradeTower();                      // 선택한 타워 업그레이드
            }
            else if (selectNum == (int)FuncNum.Move)
            {
                builtTower.GetComponentInChildren<BarrackKnightSpawn>().OnKnightMove();
                ground.UndoGround();
            }

        }
        else                                // 체크가 안된상태 (첫번째 체크)
        {
            for (int i = 0; i < checkButton.Length; i++)
            {
                isCheck[i] = false;                         // 모두 false로 변경
                checkButton[i].SetActive(false);            // 모두 체크표시 해제
            }

            isCheck[selectNum] = true;                      // 선택된 Index만 true               
            checkButton[selectNum].SetActive(true);         // 선택된 Index만 체크표시

            if (funcNum == FuncNum.Upgrade)
            {
                towerInfo.SetActive(true);
                TowerInfoText((int)builtTower.GetTowerType());
            }
            else
            {
                towerInfo.SetActive(false);
            }
        }
    }

    //-----------------------------------------------------------------------------
    // description  : 타워업글판매UI ON
    //=============================================================================
    public void OnTowerUpSell_UI(Ground ground)
    {
        this.ground = ground;
        this.builtTower = ground.BuiltTower;

        isActiveTowerUpSellUI = true;
        gameObject.SetActive(true);

        // ※위치※ 윗줄 gameObject가 Active되어야 밑의 코드 실행가능(아니면 오류나옴)
        if (builtTower.GetTowerType() == TowerType.Barrack)
        {
            knightMoveUI.SetActive(true);               // 배럭일 경우만 버튼UI 활성화
        }
        else
        {
            knightMoveUI.SetActive(false);              // 배럭제외타워는 버튼UI 비활성화
        }

        // ※위치※ 윗줄 gameObject가 Active되어야 밑의 코드 실행가능(아니면 오류나옴)
        for (int i = 0; i < checkButton.Length; i++)    // UI On했으면 체크표시 다 끄도록함
        {
            isCheck[i] = false;                         // 모두 false로 변경
            checkButton[i].SetActive(false);            // 모두 체크표시 해제
        }
    }

    //-----------------------------------------------------------------------------
    // description  : 타워업글판매UI Off
    //=============================================================================
    public void OffTowerUpSell_UI()
    {
        isActiveTowerUpSellUI = false;
        gameObject.SetActive(false);
        towerInfo.SetActive(false);
    }

    //[0-타워이름, 1-타워설명, 2-공격력, 3-공속, 4-공격사거리, 5-골드]
    private void TowerInfoText(int towerType)
    {
        if (towerType == (int)TowerType.Barrack)
        {
            towerInfoText[0].text = builtTower.TowerName;
            towerInfoText[1].text = "LV."+builtTower.TowerLevel.ToString();
            towerInfoText[2].text = (builtTower.Damage * 150 / 100).ToString();
            towerInfoText[3].text = "느림";
            towerInfoText[4].text = "근거리";
            towerInfoText[5].text = builtTower.UpgradeCost.ToString();
        }
        else if (towerType == (int)TowerType.Archer)
        {
            towerInfoText[0].text = builtTower.TowerName;
            towerInfoText[1].text = "LV." + builtTower.TowerLevel.ToString();
            towerInfoText[2].text = (builtTower.Damage * 150 / 100).ToString();
            towerInfoText[3].text = "빠름";
            towerInfoText[4].text = "중거리";
            towerInfoText[5].text = builtTower.UpgradeCost.ToString();
        }
        else if (towerType == (int)TowerType.Catapult)
        {
            towerInfoText[0].text = builtTower.TowerName;
            towerInfoText[1].text = "LV." + builtTower.TowerLevel.ToString();
            towerInfoText[2].text = (builtTower.Damage * 150 / 100).ToString();
            towerInfoText[3].text = "매우 느림";
            towerInfoText[4].text = "장거리";
            towerInfoText[5].text = builtTower.UpgradeCost.ToString();
        }
        else if (towerType == (int)TowerType.Wizard)
        {
            towerInfoText[0].text = builtTower.TowerName;
            towerInfoText[1].text = "LV." + builtTower.TowerLevel.ToString();
            towerInfoText[2].text = (builtTower.Damage * 150 / 100).ToString();
            towerInfoText[3].text = "보통";
            towerInfoText[4].text = "중거리";
            towerInfoText[5].text = builtTower.UpgradeCost.ToString();
        }
    }
}
