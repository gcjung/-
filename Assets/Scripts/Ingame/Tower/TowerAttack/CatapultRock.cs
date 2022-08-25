using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultRock : Projectile
{
    private Collider2D collider2d;

    private GameObject tempEffect;
    private bool isEffectOccur;

    private void Update()
    {
        if (Target != null)             // target이 존재하면
        {
            MoveDirection = (Target.position - transform.position).normalized;
            if (Vector3.Distance(Target.position, transform.position) <= 0.2f)
            {
                collider2d.enabled = true;
                if (isEffectOccur == false)     // 적과 부딪힐때 이펙트 1번만 실행되도록
                {
                    isEffectOccur = true;
                    tempEffect = Instantiate(effect, transform.position - new Vector3(0, -0.1f, 0), transform.rotation);
                    Destroy(tempEffect, 1f);
                }
            }
        }
        else                            // target이 없다면
        {
            Destroy(gameObject);
        }

        transform.position += MoveDirection * MoveSpeed * Time.deltaTime;
    }

    public override void SetUp(Transform target, int damage)
    {
        collider2d = GetComponent<Collider2D>();
        
        Target = target;
        Damage = damage;
        isEffectOccur = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Monster")) return;   // 적이 아닌 대상과 부딪히면 
        //if (collision.transform != Target) return;    // 현재 Target이 적이 아닐 때

        Enemy tempEnemy = collision.GetComponent<Enemy>();
        tempEnemy.co_hit = tempEnemy.Hit(Damage);

        tempEnemy.StartCoroutine(tempEnemy.co_hit);

        Destroy(gameObject);
    }
}
