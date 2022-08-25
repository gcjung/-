using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRange : MonoBehaviour
{

    private List<FriendlyUnit> unitList;
    private Enemy enemy;
    public Warrior Target;

    private void Awake()
    {
        unitList = new List<FriendlyUnit>();
        enemy = transform.parent.GetComponent<Enemy>();
    }

    private void Update()
    {
        if (enemy.attackTarget == null)
        {
            float closeDistsqr = Mathf.Infinity;

            for (int i = 0; i < unitList.Count; i++)
            {
                float distance = Vector3.Distance(unitList[i].transform.position, transform.position);

                if (distance <= closeDistsqr)
                {
                    closeDistsqr = distance;
                    enemy.attackTarget = unitList[i].transform;
                }

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "Knight")
        {
            unitList.Add(other.GetComponent<FriendlyUnit>());
            enemy.attackTarget = other.transform;
            Target = other.GetComponent<Warrior>();
            Vector3 direction = (other.transform.position - transform.position).normalized;
            enemy.isChase = true;
            enemy.isMove = false;
            enemy.StopCoroutine(enemy.onMove);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "Knight")
        {
            if (enemy.isChase == true)
            {
                //enemy.attackTarget = other.transform;
                //Vector3 direction = (other.transform.position - transform.position).normalized;
                //enemy.movement2D.MoveTo(direction);

                if (other.transform.position.x > enemy.transform.position.x)
                {

                    enemy.animator.SetBool("RightWalk", true);
                    enemy.animator.SetBool("LeftWalk", false);

                }
                else if (other.transform.position.x < transform.position.x)
                {

                    enemy.animator.SetBool("RightWalk", false);
                    enemy.animator.SetBool("LeftWalk", true);

                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "Knight")
        {
            unitList.Remove(other.GetComponent<FriendlyUnit>());

            if(enemy.attackTarget == other.transform)
                enemy.attackTarget = null;

            if (unitList.Count == 0)
            {
                enemy.animator.SetBool("RightAttack", false);
                enemy.animator.SetBool("LeftAttack", false);
                enemy.isChase = false;
                enemy.isMove = true;
                enemy.currentIndex--;
                enemy.onMove = enemy.OnMove();
                enemy.StartCoroutine(enemy.onMove);
            }
        }
    }
}