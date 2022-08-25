using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    protected GameObject effect;                        // 이펙트 프리팹

    [SerializeField]
    private float moveSpeed;                            // 투사체 속도
    private Vector3 moveDirection = Vector3.zero;       // 투사체 발사방향
    private int damage;                                 // 투사체 공격력
    private Transform target;                           // 목표몬스터

    public float MoveSpeed
    { get { return moveSpeed; } set { moveSpeed = value; } }
    public Vector3 MoveDirection
    { get { return moveDirection; } set { moveDirection = value; } }    
    public int Damage
    { get { return damage; } set { damage = value; } }
    public Transform Target
    { get { return target; } set { target = value; } }

    public virtual void SetUp(Transform target, int damage)
    {
        this.target = target;
        this.damage = damage;
    }

}
