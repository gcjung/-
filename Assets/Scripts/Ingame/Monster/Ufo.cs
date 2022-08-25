using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ufo : MonoBehaviour
{
    private CircleCollider2D circleCollider2D;
    public Animator animator;
    public GameObject point;
    public InGameManager inGameManager;
    public float moveSpeed;
    [SerializeField]
    private Vector3 moveDirection = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        point = GameObject.Find("UfoPoint");
        inGameManager = FindObjectOfType<InGameManager>();

        Vector2 direction = (point.transform.position - transform.position).normalized;
        MoveTo(direction);
        StartCoroutine(TimeDie());
    }
    void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    private void OnMouseDown()
    {
        inGameManager.Gold += 120;
        StartCoroutine(ClickDie());
    }

    IEnumerator ClickDie()
    {
        moveSpeed = 0;
        Destroy(circleCollider2D);
        animator.SetBool("isDie", true);  

        yield return new WaitForSeconds(0.4f);

        Destroy(gameObject);
    }

    IEnumerator TimeDie()
    {
        yield return new WaitForSeconds(25);

        Destroy(gameObject);
    }
    public void MoveTo(Vector3 direction)
    {
        moveDirection = direction;
    }
}
