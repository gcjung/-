using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{

    
    
    //[SerializeField]
    private float moveSpeed = 0.0f;
    [SerializeField]
    private Vector3 moveDirection = Vector3.zero;

    public float MoveSpeed;

    private void Start()
    {
        moveSpeed = MoveSpeed;
    }

    private void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
        
        if(GetComponent<Enemy>().animator.GetBool("RightAttack") == true)
        {
            moveSpeed = 0.0f;
        }
        else if(GetComponent<Enemy>().animator.GetBool("LeftAttack") == true)
        {
            moveSpeed = 0.0f;
        }
        else
        {
            moveSpeed = MoveSpeed;
        }

        if (GetComponent<Enemy>().isSlow == true)
        {
            moveSpeed = moveSpeed * 0.7f;
        }
        else if (GetComponent<Enemy>().isSlow == false)
        {
        }
    }

    public void MoveTo(Vector3 direction)
    {
        moveDirection = direction;
    }
}
