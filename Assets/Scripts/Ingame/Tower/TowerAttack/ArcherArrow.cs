using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherArrow : Projectile
{
    private GameObject tempEffect;
    private void Update()
    {
        if (Target != null)         // target�� �����ϸ�
        {
            MoveDirection = (Target.position - transform.position).normalized;
            LookAtTarget(MoveDirection);
        }
        else                        // target�� ���ٸ�
        {
            Destroy(gameObject);
        }

        transform.position += MoveDirection * MoveSpeed * Time.deltaTime;
    }

    protected void LookAtTarget(Vector3 dir)                // ȭ���� ���� ������ ���ϵ��� ����
    {
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Monster")) return;   // ���� �ƴ� ���� �ε����� 
        if (collision.transform != Target) return;      // ���� Target�� ���� �ƴ� ��

        tempEffect = Instantiate(effect, transform.position - new Vector3(0, -0.1f, 0), transform.rotation);
        Destroy(tempEffect, 1f);

        Enemy tempEnemy = collision.GetComponent<Enemy>();
        tempEnemy.co_hit = tempEnemy.Hit(Damage);
        tempEnemy.StartCoroutine(tempEnemy.co_hit);

        Destroy(gameObject);
    }

}
