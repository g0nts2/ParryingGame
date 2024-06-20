using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_F : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    public int nextMove;
    public int TimeDelay;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Think();

        Invoke("Think", TimeDelay);
    }

    // Update is called once per frame
    void FixedUpdate()
    {   //기본 움직임
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //지형 체크
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        // RaycastHit2D rayhit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));
        //if(rayhit.collider == null)
        //{
        //    nextMove = nextMove * -1;
        //    CancelInvoke();
        //    Invoke("Think", TimeDelay);
        //}
    }

    //재귀 함수
    void Think()
    {
        nextMove = Random.Range(-1, 2);

        Invoke("Think", TimeDelay);
        if (nextMove != 0)
            spriteRenderer.flipX = nextMove == 1;
    }

    public int HP;

    public void TakeDamage(int damage)
    {
        HP = HP - damage;
    }
}
