using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorAttackBasic : MonoBehaviour
{
    public Warrior war;
    public HeroMovement heroMoveScript;
    private Animator animator;
    
    float targetDist;   //타겟 몬스터와의 거리
    
    GameObject targetMonster;
    List<GameObject> monstersAround = new List<GameObject>();   //몬스터 리스트
    
    bool isAttack = false;
    int resultDamage;       //데미지에 랜덤값 추가한 값


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //기본공격
        FindTargetMonster();
        GoToTargetandAttack();
        WarriorAttackAni();
        

        if (Input.GetMouseButtonDown(1)|| targetDist > 0.5f)        //공격 중 멈추기
        {
            isAttack = false;
            StopAllCoroutines();
            heroMoveScript.heroSpeed = 1.5f;    //속도 초기화
        }
         

    }
   

    private void OnTriggerEnter2D(Collider2D collision) //몬스터 영역안에 들어오면 리스트에 넣기 
    {
        if (collision.CompareTag("Monster"))
            monstersAround.Add(collision.gameObject);
    }
    private void OnTriggerExit2D(Collider2D collision)  //몬스터가 영역에서 나가면 리스트에서 제거
    {
        if (collision.CompareTag("Monster"))
            monstersAround.Remove(collision.gameObject);
    }

    void FindTargetMonster()
    {
        if (monstersAround.Count != 0 )
        {
            float TargetDistCheck = -1f;            //타겟몬스터와의 거리 - 거리 감지용
            int TargetIndex = -1;                   //타겟팅 할 인덱스

            for (int i = 0; i < monstersAround.Count; i++)
            {
                float currentDist = Vector2.Distance(transform.position, monstersAround[i].transform.position);     //i번째 몬스터와의 거리

                if (TargetDistCheck == -1)               //첫 몬스터를 타겟으로 정하고 타겟 거리도 저장
                {
                    TargetDistCheck = currentDist;
                    TargetIndex = i;
                }
                else
                {
                    if (TargetDistCheck >= currentDist)  //타겟거리 비교해서 더 가까운놈을 인덱스로 저장
                    {
                        TargetIndex = i;
                        TargetDistCheck = currentDist;
                    }
                }
                targetMonster = monstersAround[TargetIndex];
            }
            TargetDistCheck = -1f;  //초기화   
        }
    }
    void GoToTargetandAttack()
    {
        if (monstersAround.Count != 0 && !heroMoveScript.isHeroCursorMove)
        {
            heroMoveScript.isHeroMove = true;

            heroMoveScript.heroMoveDir = (targetMonster.transform.position - transform.position).normalized;
            transform.position += heroMoveScript.heroMoveDir * heroMoveScript.heroSpeed * Time.deltaTime;

            targetDist = Vector2.Distance(transform.position, targetMonster.transform.position); //타겟몬스터와의거리(움직이는 동안)
            if (targetDist <= 0.5f * heroMoveScript.heroSpeed)
            {
                heroMoveScript.isHeroMove = false;
                heroMoveScript.heroSpeed = 0;
                if (isAttack == false)
                    StartCoroutine(HeroAttackCo());
            }
        }
    }
    
    IEnumerator HeroAttackCo()
    {
        animator.SetTrigger("Attack");

        while (targetMonster != null && targetDist <= 0.5f)
        {
            isAttack = true;

            resultDamage = Random.Range(GameManager.instance.Atk - 4, GameManager.instance.Atk + 5);
            targetMonster.GetComponent<Enemy>().nowHp -= resultDamage;    //Atk
            
            if (targetMonster.GetComponent<Enemy>().nowHp <= 0)
            {
                monstersAround.Remove(targetMonster);
                isAttack = false;
                heroMoveScript.isHeroMove = false;
            }

            yield return new WaitForSeconds(1f);
        }
        isAttack = false;
        heroMoveScript.heroSpeed = 1.5f;    //속도 초기화
    }

    void WarriorAttackAni()
    {
        if (isAttack == true)
        {
            animator.SetBool("isAttack", true);
            if (targetMonster != null)
            {
                heroMoveScript.heroMoveDir = (targetMonster.transform.position - transform.position).normalized;
                //영웅-목적지 방향값(정규화시켜줌)
                animator.SetFloat("inputX", heroMoveScript.heroMoveDir.x);
                animator.SetFloat("inputY", heroMoveScript.heroMoveDir.y);
            }
        }
        else
            animator.SetBool("isAttack", false);        
    }



 


}

