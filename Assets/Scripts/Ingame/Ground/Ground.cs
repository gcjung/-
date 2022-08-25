using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Ground : MonoBehaviour
{
    [SerializeField]
    private TowerSelectUI       towerSelectUI;          // 타워선택UI
    [SerializeField]
    private TowerUpgradeSellUI  towerUpgradeSellUI;     // 타워업글판매UI
    [SerializeField]
    private TowerInfoUI         towerInfoUI;            // 타워정보UI
    [SerializeField]
    private GameObject          noMoneyText;

    [SerializeField]
    private GameObject[]        towerPrefab;            // 타워 프리팹 배열
    [SerializeField]
    private InGameManager       inGameManager;          // 게임매니저(골드, 다이아, 별 관리 등등)

    private int[]               constructionCost;       // 건물 건설비용
    private GameObject          builtTower;             // 현재 땅에 설치된 타워
    public SpriteRenderer       towerAttackRange;       // 현재 땅에 설치된 타워의 공격사거리표시
    public bool                 isTowerBuild;           // 타워가 현재땅에 지어졌는지 Flag

    private Camera              mainCamera;
    private SpriteRenderer      sprite;                 // 현재 땅의 Sprite
    
    public static Ground        prevSelectGround;       // 이전 선택 땅

    public Tower BuiltTower
    {
        get { return builtTower.GetComponent<Tower>(); }
    }

    private void Awake()
    {
        constructionCost = new int[4] { 70, 50, 120, 60 };      // [배럭,궁수,투석기,법사]
        inGameManager = FindObjectOfType<InGameManager>();
        sprite = GetComponent<SpriteRenderer>(); 
        mainCamera = Camera.main;

        isTowerBuild = false;
    }

    private void OnMouseDown()
    {
      
            if (!EventSystem.current.IsPointerOverGameObject()) // UI가 위에 있으면 UI이벤트만 발생
            {
                // (다른 땅 선택했을때 이전 선택 땅의 색을 원래대로 되돌림)
                if (prevSelectGround != null)
                {                 // 이전 선택된 땅이 있다면 (처음선택한 땅이 아니라면)
                    prevSelectGround.GetComponent<SpriteRenderer>().color = Color.white; // 원래 색으로 변경.

                    if (prevSelectGround.isTowerBuild)
                    {
                        if(prevSelectGround.BuiltTower.GetTowerType() == TowerType.Barrack)
                        {
                            prevSelectGround.towerAttackRange.enabled = false;
                        }
                    }

                }
                prevSelectGround = this.gameObject.GetComponent<Ground>();             // 현재 땅의 정보를 저장

                OnClickGroundUI();
            }
        
    }
    private void OnMouseEnter()
    {
        if (sprite.color == new Color(1f, 0.4f, 0.4f, 0.8f))    // "타워"가 클릭된 상태일때
        {
            towerInfoUI.OnMouseEnterGround();
            towerAttackRange.enabled = true;
        }
    }

    private void OnMouseExit()          
    {
        if (builtTower != null && BuiltTower.GetTowerType() != TowerType.Barrack)   // 지워진 타워가 배럭이 아닐때
        {
            towerInfoUI.OffTowerInfoUI();                       // 타워정보UI OFF
            towerAttackRange.enabled = false;                   // 공격사거리표시 끄기
        }
        else if (builtTower != null && BuiltTower.GetTowerType() == TowerType.Barrack)
        {
            towerInfoUI.OffTowerInfoUI();                       // 타워정보UI OFF
        }
    }

    void OnClickGroundUI()
    {
        if (isTowerBuild == false)  // 타워가 없을때 -> 타워선택UI
        {
            sprite.color = new Color(0.3f, 1, 0.3f, 0.8f);                          // 선택된 땅 초록색으로 표시
            towerUpgradeSellUI.OffTowerUpSell_UI();

            // 땅을 중심으로 타워선택UI 위치설정해줌
            towerSelectUI.GetComponent<RectTransform>().position = mainCamera.WorldToScreenPoint(transform.position + new Vector3(0, 0.3f, 0));
            
            towerSelectUI.OnTowerSelectUI(gameObject.GetComponent<Ground>());       // UI ON, 땅 정보를 매개변수로 줌
        }
        else                        // 타워가 있을때 -> 타워업글판매UI 및 타워정보UI
        {
            sprite.color = new Color(1f, 0.4f, 0.4f, 0.8f);                         // 선택된 땅 빨간색으로 표시
            towerSelectUI.OffTowerSelectUI();
            
            towerAttackRange.enabled = true;                                        // 타워 공격범위 표시
            
            // 타워정보UI 출력
            towerInfoUI.OnTowerInfoUI(builtTower.GetComponent<Tower>());            // 타워정보UI ON
            // 땅을 중심으로 타워선택UI 위치설정해줌 (타워별로 조정필요)
            towerUpgradeSellUI.GetComponent<RectTransform>().position = mainCamera.WorldToScreenPoint(transform.position + new Vector3(0, 0.4f, 0));
            towerUpgradeSellUI.OnTowerUpSell_UI(gameObject.GetComponent<Ground>()); // UI ON, 땅 정보를 매개변수로 줌
        }
            
    }

    public void BuyTower(int towerindex)
    {
        //sprite.color = Color.white;                         // 타워를 설치하면 땅을 원래 색으로 바꿈
        //towerSelectUI.OffTowerSelectUI();                   // UI 비활성화

        Vector3 offsetTowerPosition = new Vector3(0, 0.6f, 0);

        if (inGameManager.Gold - constructionCost[towerindex] >= 0) // 돈이 있을 경우
        {
            isTowerBuild = true;                                // 땅에 타워가 건설되었다(= true)
            SfxManager.instance.BuildTower();

            if (towerindex == (int)TowerType.Wizard)            // 위자드 타워일때만 위치값 따로 보정하여 설치
            {
                builtTower = Instantiate(towerPrefab[towerindex], transform.position + new Vector3(0, 0.39f, 0), Quaternion.identity); // 타워를 ground 중앙에 위치하기 위한 y값 조정
                towerAttackRange = builtTower.transform.GetChild(0).GetComponent<SpriteRenderer>();     // 타워의 공격범위 표시
            }
            else                                                // 나머지 타워들은 위치값 똑같이 보정하여 설치
            {
                builtTower = Instantiate(towerPrefab[towerindex], transform.position + offsetTowerPosition, Quaternion.identity);      // 타워를 ground 중앙에 위치하기 위한 y값 조정
                towerAttackRange = builtTower.transform.GetChild(0).GetComponent<SpriteRenderer>();     // 타워의 공격범위 표시
            }

            inGameManager.Gold -= builtTower.GetComponent<Tower>().ConstructionCost;  // 건설비용을 빼줌
                                                                                      // 앞쪽의 타워는 뒤쪽타워에 가려지지 않게 해줌
            builtTower.GetComponent<SpriteRenderer>().sortingOrder = sprite.sortingOrder;
        }
        else
        {
            noMoneyText.SetActive(true);
            noMoneyText.GetComponent<RectTransform>().position = mainCamera.WorldToScreenPoint(transform.position + new Vector3(0, 0.3f, 0));
            noMoneyText.GetComponent<NoMoneyText>().OnNoMoneyText();
        }

        UndoGround();
    }

    public void SellTower()
    {
        isTowerBuild = false;                                   // 땅에 타워가 없다(= false)

        SfxManager.instance.DestroyTower();
        inGameManager.Gold += builtTower.GetComponent<Tower>().SellingCost;       // 판매비용을 더해줌

        if (BuiltTower is BarrackTower)
        {
            BarrackTower barrackTower = BuiltTower as BarrackTower;
            barrackTower.DestroyKnight();
        }

        UndoGround();
        Destroy(builtTower);                                    // 팔았으니깐 파괴
    }

    public void UpgradeTower()
    {

        if (inGameManager.Gold - BuiltTower.UpgradeCost >= 0)   // 돈이 있으면 업글!
        {
            inGameManager.Gold -= BuiltTower.Upgrade();         // 타워의 공격력, 업글비용 등 오름
        }
        else
        {
            noMoneyText.SetActive(true);
            noMoneyText.GetComponent<RectTransform>().position = mainCamera.WorldToScreenPoint(transform.position + new Vector3(0, 0.3f, 0));
            noMoneyText.GetComponent<NoMoneyText>().OnNoMoneyText();
            
        }

        UndoGround();
    }

    public void UndoGround()
    {
        sprite.color = Color.white;                         // 땅의 색을 원래대로 바꿈
        towerSelectUI.OffTowerSelectUI();                   // UI 비활성화
        towerUpgradeSellUI.OffTowerUpSell_UI();             // 타워업글판매UI OFF
    }
}






// 썻다가 버린 코드들.. UI가 게임화면 가장자리에서 짤리는걸 방지할때 썻던 코드
/*
이전 TowerSelect UI 조정용 
if ((Input.mousePosition.x <= Screen.width / 2) && (Input.mousePosition.y >= Screen.height / 2))
{
    //Debug.Log("2사분면");
    towerSelectUI_rectTransform.pivot = Vector2.up;

}
else if ((Input.mousePosition.x <= Screen.width / 2) && (Input.mousePosition.y < Screen.height / 2))
{
    //Debug.Log("3사분면");
    towerSelectUI_rectTransform.pivot = Vector2.zero;
}
else if ((Input.mousePosition.x > Screen.width / 2) && (Input.mousePosition.y >= Screen.height / 2))
{
    //Debug.Log("1사분면");
    towerSelectUI_rectTransform.pivot = Vector2.one;
}
else
{
    //Debug.Log("4사분면");
    towerSelectUI_rectTransform.pivot = Vector2.right;
}
*/