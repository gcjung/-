using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TowerSelectUI : MonoBehaviour
{
    [SerializeField]
    private GameObject[]    builtTower;                 // 타워 프리팹 배열

    [SerializeField]
    private GameObject[]    checkButton;                // 타워 선택버튼 배열 (UI)
    private bool[]          isCheck;                    // 타워 선택시 한번 더 확인

    private Ground          ground;                     // 선택한 땅 정보

    [SerializeField]
    private GameObject      towerInfo;                  // 타워선택창 가운데 타워정보텍스트
    [SerializeField]
    private Text[]          towerInfoText;              // 타워정보(UI) [0-타워이름, 1-타워설명, 2-공격력, 3-공속, 4-공격사거리, 5-골드]

    public static bool      isActiveTowerSelectUI;      // UI 사용에 관한 Flag


    private void Awake()
    {
        isCheck = new bool[4] { false, false, false, false };
        isActiveTowerSelectUI = false;
    }

    public void OnClickBuildBarrack()       // Button1 클릭 시 실행 (배럭)
    {
        SelectTower((int)TowerType.Barrack);
    }
    public void OnClickBuildArcher()        // Button2 클릭 시 실행 (아처)
    {
        SelectTower((int)TowerType.Archer);
    }
    public void OnClickBuildCatapult()      // Button3 클릭 시 실행 (투석기)
    {
        SelectTower((int)TowerType.Catapult);
    }
    public void OnClickBuildWizard()        // Button4 클릭 시 실행 (위자드)
    {
        SelectTower((int)TowerType.Wizard);
    }

    //-----------------------------------------------------------------------------
    // description    : 타워 선택시 한번 더 체크해줌 ((클릭)체크표시 띄어줌 -> (클릭)타워 짓기)
    //=============================================================================
    public void SelectTower(int index)
    {
        if (isCheck[index] == true)     // 체크상태
        {
            for (int i = 0; i < checkButton.Length; i++)
            {
                isCheck[i] = false;                         // 모두 다 false
            }

            towerInfo.SetActive(false);                     // 타워정보창 OFF
            checkButton[index].SetActive(false);            // 타워건설했으면 체크표시 해제
            ground.BuyTower(index);                         // 선택한 타워 건설
        }
        else                            // 체크가 안된상태
        {  
            for (int i = 0; i < checkButton.Length; i++)    
            {
                isCheck[i] = false;                         // 모두 false로 변경
                checkButton[i].SetActive(false);            // 모두 체크표시 해제
            }

            isCheck[index] = true;                          // 선택된 Index만 true               
            checkButton[index].SetActive(true);             // 선택된 Index만 체크표시

            TowerInfoText(index);                           // 타워정보창에 텍스트 입력
            towerInfo.SetActive(true);                      // 타워정보창 ON
            
        }
    }

    //-----------------------------------------------------------------------------
    // description : 타워선택UI On, Ground를 멤버변수에 저장해줌
    //=============================================================================
    public void OnTowerSelectUI(Ground ground)       
    {                             
        this.ground = ground;     // 선택한 땅의 정보를 클래스내 멤버변수로 저장

        isActiveTowerSelectUI = true;
        gameObject.SetActive(isActiveTowerSelectUI);

        //※위치※ 윗줄 gameObject가 Active되어야 밑의 코드 실행가능(아니면 오류나옴)
        for (int i = 0; i < checkButton.Length; i++)    // UI On했으면 체크표시 다 끄도록함
        {
            isCheck[i] = false;                         // 모두 false로 변경
            checkButton[i].SetActive(false);            // 모두 체크표시 해제
        }

        towerInfo.SetActive(false);
    }

    //-----------------------------------------------------------------------------
    // description    : 타워선택UI Off
    //=============================================================================
    public void OffTowerSelectUI()
    {
        isActiveTowerSelectUI = false;
        gameObject.SetActive(isActiveTowerSelectUI);
    }

    //[0-타워이름, 1-타워설명, 2-공격력, 3-공속, 4-공격사거리, 5-골드]
    private void TowerInfoText(int towerType)
    {
        if (towerType == (int)TowerType.Barrack)
        {
            towerInfoText[0].text = "병영 타워";
            towerInfoText[1].text = "병사 3명을 소환하는 타워";
            towerInfoText[2].text = "10";
            towerInfoText[3].text = "느림";
            towerInfoText[4].text = "근거리";
            towerInfoText[5].text = "70";
        }
        else if(towerType == (int)TowerType.Archer)
        {
            towerInfoText[0].text = "아처 타워";
            towerInfoText[1].text = "빠른 속도로 공격하는 타워";
            towerInfoText[2].text = "20";
            towerInfoText[3].text = "빠름";
            towerInfoText[4].text = "중거리";
            towerInfoText[5].text = "50";
        }
        else if(towerType == (int)TowerType.Catapult)
        {
            towerInfoText[0].text = "투석기";
            towerInfoText[1].text = "광역공격을 하는 타워";
            towerInfoText[2].text = "40";
            towerInfoText[3].text = "매우 느림";
            towerInfoText[4].text = "장거리";
            towerInfoText[5].text = "120";
        }
        else if(towerType == (int)TowerType.Wizard)
        {
            towerInfoText[0].text = "마법사 타워";
            towerInfoText[1].text = "적을 느리게하는 타워";
            towerInfoText[2].text = "25";
            towerInfoText[3].text = "보통";
            towerInfoText[4].text = "중거리";
            towerInfoText[5].text = "60";
        }
    }

}













// 썻다가 버린 코드들..
/*
-----------------------------------------------------------------------------
 DESCRIPTS    : 타워를 선택한 땅에 설치
=============================================================================
private void BuildTower(int towerIndex)
{
    GameObject tempTower;

    if (towerIndex == (int)TowerType.Wizard)           // 위자드 타워일때만 위치값 따로 보정하여 설치
    {
        tempTower = Instantiate(builtTower[towerIndex], groundPosition + new Vector3(0, 0.39f, 0), Quaternion.identity); // 타워를 ground 중앙에 위치하기 위한 y값 조정
    }
    else                                // 나머지 타워들은 위치값 똑같이 보정하여 설치
    {
        tempTower = Instantiate(builtTower[towerIndex], groundPosition + offsetTowerPosition, Quaternion.identity);      // 타워를 ground 중앙에 위치하기 위한 y값 조정
    }

    // 설치한 타워의 sortingOrder의 수정을 위함
    tempTower.GetComponent<SpriteRenderer>().sortingOrder = ground.gameObject.GetComponent<SpriteRenderer>().sortingOrder;

    ground.GetComponent<SpriteRenderer>().color = Color.white;  // 타워를 설치하면 땅을 원래 색으로 바꿈
    this.ground.isTowerBuild = true;                            // 땅에 타워가 건설되었다(true)
    OffTowerSelectUI();
}
*/