using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KnightRecognitionRange : MonoBehaviour
{
    public List<Enemy> monsterList;

    private void Awake()
    {
        monsterList = new List<Enemy>(); 
    }


    private void OnTriggerEnter2D(Collider2D collision) //몬스터 영역안에 들어오면 리스트에 넣기 
    {
        if (collision.CompareTag("Monster"))
        {
            monsterList.Add(collision.GetComponent<Enemy>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)  //몬스터가 영역에서 나가면 리스트에서 제거
    {
        if (collision.CompareTag("Monster"))
        {
            Enemy collisionEnemy = collision.GetComponent<Enemy>();

            if (transform.parent.GetComponent<knight>().TargetMonster == collisionEnemy)
            {
                transform.parent.GetComponent<knight>().TargetMonster = null;
            }

            monsterList.Remove(collisionEnemy);
        }
    }


}
