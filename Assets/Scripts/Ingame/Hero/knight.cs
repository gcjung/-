using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum KnightState { SearchTarget, MoveToTarget, AttackToTarget }

public class knight : FriendlyUnit
{
    
    private KnightState knightState;
    private KnightRecognitionRange ReocgnitionRange;
    private List<Enemy> monsterList;
    private Enemy targetMonster;
    private BarrackKnightSpawn barrackKnightSpawn;
    private Animator animator;

    private float moveSpeed = 0.7f;
    private float attackSpeed = 1f;

    private bool isMoveable = false;

    private bool isWorriorSpawnDead = false;
    private bool isWorriorSpawn = false;
    [HideInInspector]
    public bool isBarrackSpawn = false;
    [HideInInspector]
    public bool isBarrackSpawnDead = false;
    int index = 10;

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 destination;

    public int knightAtk = 10;
    public int knightHPnow;
    public int knightHPmax;
    public float buffPercent= 0.2f;

    public Enemy TargetMonster
    { get { return targetMonster; } set { targetMonster = value; } }

    
    public int KnightHP
    {
        get { return knightHPnow; }
        set
        {
            knightHPnow = value;

            if (isBarrackSpawn && !isBarrackSpawnDead && knightHPnow <= 0)
            {
                isBarrackSpawnDead = true;
                DestroyKnight();
                barrackKnightSpawn.SpawnKnightStart(index,10f);
            }
        }
    }
    public int KnightAtk
    {
        get { return knightAtk; }
        set
        {
            if (value > knightHPmax)
                value = knightHPmax;
            else if (value < 0)
                value = 0;

            knightAtk = value;
        }
    }
    public int KnightHPmax
    {
        get { return knightHPmax; }
        set { knightHPmax = value; }
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        monsterList = new List<Enemy>();
        ReocgnitionRange = transform.GetChild(0).GetComponent<KnightRecognitionRange>();
    }


    private void OnEnable()
    {
        monsterList = ReocgnitionRange.monsterList;

        knightAtk = 10;
        knightHPnow = 30;
        knightHPmax = 30;
        buffPercent = 0.2f;
        moveSpeed = 0.7f;
        attackSpeed = 1f;
        isWorriorSpawnDead = false;
        isBarrackSpawnDead = false;

        knightState = KnightState.SearchTarget;
        StartCoroutine(knightState.ToString());
    }

    void Update()
    {
        if(isMoveable)
        {
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            if (Vector2.Distance(destination, transform.position) <= 0.2f)
            {
                isMoveable = false;
                knightState = KnightState.SearchTarget;
                StartCoroutine(knightState.ToString());
            }
        }

        if (knightHPnow <= 0 && !isWorriorSpawnDead && isWorriorSpawn)
        {
            isWorriorSpawnDead = true;
            DestroyKnight();
        }

    }

    public void MoveToDestination(Vector3 destination)
    {
        StopCoroutine(knightState.ToString());
        isMoveable = true;
        this.destination = destination;

        moveDirection = (destination - transform.position).normalized;
        moveDirection.z = 0f;
    }

    public void BarrackSpawnSetting(BarrackKnightSpawn barrackKnightSpawn, int index, Vector3 destination)
    {
        this.index = index;
        this.barrackKnightSpawn = barrackKnightSpawn;
        isBarrackSpawn = true;
        MoveToDestination(destination);
    }
    public void WorriorSpawnSetting()
    {
        isWorriorSpawn = true;
        Invoke("DestroyKnight", 10f);
    }

    private void DestroyKnight()
    {
        StopAllCoroutines();
        animator.SetBool("isAttack", false);
        ObjectPool.ReturnObject(this);
    }

    public void ChangeState(KnightState newState)           //
    {
        StopCoroutine(knightState.ToString());              //

        knightState = newState;
        StartCoroutine(knightState.ToString());             //
    }

    private IEnumerator SearchTarget()
    {
        while (true)
        {
            float closeDist = Mathf.Infinity;
            for (int i = 0; i < monsterList.Count; i++)         // 사거리내 가장 가까운 몹을 목표몬스터로 지정
            {
                float distance = Vector2.Distance(monsterList[i].transform.position, transform.position);

                if (distance <= closeDist)
                {
                    closeDist = distance;
                    targetMonster = monsterList[i];
                }
            }

            if (targetMonster != null)                         // 
            {
                ChangeState(KnightState.MoveToTarget);
            }

            yield return null;
        }

    }


    private IEnumerator MoveToTarget()
    {
        while (true)
        {
            if (targetMonster != null)
            {
                Vector3 targetKnightDirection = (targetMonster.transform.position - transform.position).normalized;
                targetKnightDirection.z = 0;
                transform.position += targetKnightDirection * moveSpeed * Time.deltaTime;

                float targetDist = Vector2.Distance(transform.position, targetMonster.transform.position); //타겟몬스터와의거리(움직이는 동안)
                if (targetDist <= 0.3f) 
                {
                    ChangeState(KnightState.AttackToTarget);
                }

                yield return null;

            }
            else
            {
                ChangeState(KnightState.SearchTarget);
                yield return null;
            }
        }
    }

    private IEnumerator AttackToTarget()
    {

        while (true)
        {
            if (targetMonster == null)
            {
                animator.SetBool("isAttack", false);
                ChangeState(KnightState.SearchTarget);

                yield return null;
            }
            

            float targetDist = Vector2.Distance(transform.position, targetMonster.transform.position); //타겟몬스터와의거리(움직이는 동안)
            if (targetDist > 0.2f) 
            {
                animator.SetBool("isAttack", false);
                ChangeState(KnightState.MoveToTarget);

                yield return null;
            }


            if (targetMonster.transform.position.x > transform.position.x)
            {
                animator.SetTrigger("RightAttack");
            }
            else
            {
                animator.SetTrigger("LeftAttack");
            }

            targetMonster.co_hit = targetMonster.Hit(knightAtk);
            targetMonster.StartCoroutine(targetMonster.co_hit);

            yield return new WaitForSeconds(attackSpeed);
        }
    }

}



