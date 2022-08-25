using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStatus : MonoBehaviour
{
    
    public float hp;
    public float atk;
    public float moveSpeed;
    // Start is called before the first frame update

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Monster")
        {
            collision.GetComponent<Enemy>().nowHp -= 10;
        }
        else
        {
            Debug.Log("몬스터가 아님");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(new Vector3(0, moveSpeed*Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector3(moveSpeed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(new Vector3(0, -moveSpeed * Time.deltaTime, 0));
        }


        if (hp<=0)
        {
            Debug.Log("파괴되었습니다");
            Destroy(gameObject);
        }
    }
}
