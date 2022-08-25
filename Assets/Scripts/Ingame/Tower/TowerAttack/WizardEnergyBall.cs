using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardEnergyBall : Projectile
{
    private GameObject tempEffect;

    private void Update()
    {
        if (Target != null)         // target이 존재하면
        {
            MoveDirection = (Target.position - transform.position).normalized;
        }
        else                        // target이 없다면
        {
            Destroy(gameObject);
        }

        transform.position += MoveDirection * MoveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Monster")) return;   // 적이 아닌 대상과 부딪히면 
        if (collision.transform != Target) return;      // 현재 Target이 적이 아닐 때
        
        tempEffect = Instantiate(effect, transform.position - new Vector3(0, -0.1f, 0), transform.rotation);
        Destroy(tempEffect,1f);

        Enemy tempEnemy = collision.GetComponent<Enemy>();
        tempEnemy.co_hit = tempEnemy.WizardHit(Damage);
        tempEnemy.StartCoroutine(tempEnemy.co_hit);

        Destroy(gameObject);
    }


}
