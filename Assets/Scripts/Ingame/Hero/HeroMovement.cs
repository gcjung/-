using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    public float heroSpeed = 1.5f;    //영웅 이동속도

    private Animator animator;
    

    private CircleCollider2D heroCollider;

    public Vector3 cursorInput, cursorFix;
    private Camera mainCamera;

    private Ray ray;
    private RaycastHit hit;

    public bool isHeroMove;
    public bool isHeroCursorMove;

    public Vector3 heroMoveDir;



    void Awake()
    {
        heroCollider = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        CursorPoint();
        HeroMoveToCursor();
        WarriorWalkAni(); 
    }
    void CursorPoint()      //마우스 포인트 입력
    {
        if (Input.GetMouseButtonDown(1))
        {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.CompareTag("Clickable"))
                {
                    isHeroMove = true;
                    isHeroCursorMove = true;
                    heroCollider.enabled = false;
                    SetCursorPos();         
                }
            }

        }
    }
    void SetCursorPos()   //목적지 설정 함수
    {
        cursorInput = Camera.main.ScreenToWorldPoint(Input.mousePosition); //계속 바뀌는 마우스값
        cursorFix = new Vector3(cursorInput.x, cursorInput.y, 0);
        heroMoveDir = (cursorFix - transform.position).normalized;
    }
    public void HeroMoveToCursor()        //우클릭자리로 이동
    {
        if (isHeroMove && isHeroCursorMove)
        {

            heroMoveDir = (cursorFix - transform.position).normalized;
            
            transform.position += (heroMoveDir * heroSpeed * Time.deltaTime);

            float distance = Vector2.Distance(cursorFix , transform.position);
            if (distance <= 0.01f)
            {
                heroCollider.enabled = true;
                isHeroMove = false;
                isHeroCursorMove = false;
            }
        }
       
    }

    public void WarriorWalkAni()       
    {
        if (isHeroMove == true)
            animator.SetBool("isMove", true);

        else
            animator.SetBool("isMove", false);
  
            //영웅-목적지 방향값(정규화시켜줌)
        animator.SetFloat("inputX", heroMoveDir.x);
        animator.SetFloat("inputY", heroMoveDir.y);
        
    }


}
