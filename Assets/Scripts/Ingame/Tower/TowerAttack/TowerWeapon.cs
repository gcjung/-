using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponState { SearchTarget, AttackToTarget }

public class TowerWeapon : MonoBehaviour
{
    [SerializeField]
    private GameObject  projectile;                     // 투사체 프리팹
    [SerializeField]
    private Transform   spawnPoint;                     // 투사체 발사위치

    private Tower       myTower;                        // 타워의 정보를 저장
    private int         towerLevel;                     // 타워의 레벨 
    private int         damage;                         // 타워의 데미지
    private float       attackSpeed;                    // 타워의 공격속도
    
    private WeaponState weaponState;                    // 타워의 상태[0-목표감지중, 1-목표공격중]

    private List<Enemy> towerEnemyList;                 // 타워 사거리 안의 몬스터 리스트
    private Transform   targetEnemy;                    // 공격대상으로 지정된 타겟

    private Animator    animator;                       // 애니메이터 
    private void Start()
    {
        myTower = transform.parent.GetComponent<Tower>();   // 부모옵젝인 타워의 정보를 변수에 저장
        damage = myTower.Damage;                            // 타워 데미지 
        attackSpeed = myTower.AttackSpeed;                  // 타워 공격속도 
        towerEnemyList = new List<Enemy>();                 // 리스트 초기화

        if(myTower.GetTowerType() == TowerType.Catapult)
            animator = transform.parent.GetComponent<Animator>();   // 애니메이터 
        else if(myTower.GetTowerType() == TowerType.Archer)
            animator = transform.parent.GetChild(2).GetComponent<Animator>();
        else if(myTower.GetTowerType() == TowerType.Wizard)
            animator = transform.parent.GetChild(2).GetComponent<Animator>();
        

        weaponState = WeaponState.SearchTarget;             // 타워의 상태 [목표감지중]
        StartCoroutine(weaponState.ToString());             // [목표감지중]으로 코루틴시작
    }

    public void SetTowerUpgradeInfo()                       // 타워 업그레이드시 값 재설정
    {
        this.damage = myTower.Damage;                       // 공격력 설정 
        this.attackSpeed = myTower.AttackSpeed;             // 공속 설정
        this.towerLevel = myTower.TowerLevel;               // 타워 레벨 설정
        animator.SetInteger("Level", towerLevel);           // 타워 레벨에 따른 애니메이션 변경
        Debug.Log(towerLevel);
    }

    public void ChangeState(WeaponState newState)           // [목표감지중<->목표공격중] 코루틴변경
    {
        StopCoroutine(weaponState.ToString());              // 기존 실행중인 코루틴 정지
        
        weaponState = newState;
        //Debug.Log("ChangeState! 바뀔 상태 : " + weaponState);
        StartCoroutine(weaponState.ToString());             // 변경된 상태로 코루틴 시작
    }

    private IEnumerator SearchTarget()                      // [목표감지중] 코루틴
    {
        while(true)
        {
            float closeDistsqr = Mathf.Infinity;        

            for (int i =0;i < towerEnemyList.Count; i++)     // 사거리내 가장 가까운 몹을 목표몬스터로 지정
            {
                float distance = Vector3.Distance(towerEnemyList[i].transform.position, transform.position);

                if (distance <= closeDistsqr)
                {
                    closeDistsqr = distance;
                    targetEnemy = towerEnemyList[i].transform;
                }
            }

            if(targetEnemy != null)                         // 타겟이 설정되면 [목표감지중->목표공격중] 코루틴으로변경
            {
                ChangeState(WeaponState.AttackToTarget);     
            }

            yield return null;
        }
    }

    private IEnumerator AttackToTarget()
    {
        Attack();
        

        while (true)
        {
            if (targetEnemy == null)                        // 목표몬스터가 없다면 [목표감지중]으로 변경
            {
                ChangeState(WeaponState.SearchTarget);
                //yield return null;
            }
            
            yield return new WaitForSeconds(attackSpeed);   // 공속만큼 대기 후 공격

            if (targetEnemy != null)                        // 타겟이 아직 살아있다면 공격
            {
                Attack();                                   
            }
        }
    }


    private void Attack()
    {
        if (targetEnemy.position.x < transform.position.x)  // 적위치에 따른 애니메이션 실행
            animator.SetTrigger("LeftAttack");              
        else
            animator.SetTrigger("RightAttack");

        if (myTower.GetTowerType() == TowerType.Catapult)   // 투석기일때
        {
            Invoke("SpawnProjectile", 0.12f);               // 적감지시 0.12초 후 투사체 발사하도록 해줌        
            SfxManager.instance.CatapultAtk();
        }
        else if (myTower.GetTowerType() == TowerType.Wizard)
        {
            Invoke("SpawnProjectile", 0.17f);               // 적감지시 0.17초 후 투사체 발사하도록 해줌
            SfxManager.instance.MagicAtk();
        }
        else if (myTower.GetTowerType() == TowerType.Archer)
        {
            Invoke("SpawnProjectile", 0.16f);
            SfxManager.instance.ArcherAtk();
        }

        
    }


    private void SpawnProjectile()                                  // 투사체 생성
    {
        GameObject temp = Instantiate(projectile, spawnPoint.position, Quaternion.identity);
        temp.GetComponent<Projectile>().SetUp(targetEnemy, damage); // 투사체 값 설정
    }

    private void OnTriggerEnter2D(Collider2D collision)             // 사거리 안의 적을 리스트에 추가
    {
        if (collision.tag == "Monster")
        {
            towerEnemyList.Add(collision.GetComponent<Enemy>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)              // 사거리 밖 or 몬스터처치시 리스트에서 삭제
    {
        if (collision.tag == "Monster")
        {
            if (targetEnemy == collision.transform)                 // 목표 몬스터가 사거리 밖 or 몬스터처치시 NULL로 변경
            {
                targetEnemy = null;
            }
            towerEnemyList.Remove(collision.GetComponent<Enemy>());
        }
    }
}
