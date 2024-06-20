using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_S : MonoBehaviour
{
    public GameManager gameManager;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;

    public int nextMove;
    public float Speed;

    //이펙트
    public GameObject Effect;
    public Transform Effect_pos;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Think();

        Invoke("Think", 2);
    }

    void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * Speed, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 0.5f, 0));
        Debug.DrawRay(frontVec, Vector3.right, new Color(0, 0.5f, 0));
        Debug.DrawRay(frontVec, Vector3.left, new Color(0, 0.5f, 0));
        RaycastHit2D rayhit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Floor", "Platform"));
        RaycastHit2D rayhit_R = Physics2D.Raycast(frontVec, Vector3.right, 0.5f, LayerMask.GetMask("Floor", "Platform"));
        RaycastHit2D rayhit_L = Physics2D.Raycast(frontVec, Vector3.left, 0.5f, LayerMask.GetMask("Floor", "Platform"));


        if (rayhit.collider == null || rayhit_R.collider != null || rayhit_L.collider != null)
        {
            Turn();
        }


        if (HP <= 0)
        {
            gameManager.stagePoint += 100;

            DestroyEnemy();
            GameObject Effectcopy = Instantiate(Effect, Effect_pos.transform.position, transform.rotation);
            Destroy(Effectcopy, 0.45f); // n초 후에 이펙트 삭제
        }
    }

    void Think()
    {
        nextMove = Random.Range(-1, 2);
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);

        anim.SetInteger("walk", nextMove);

        if (nextMove != 0)
            spriteRenderer.flipX = nextMove == 1;
    }

    void Turn()
    {
        nextMove = nextMove * -1;
        spriteRenderer.flipX = nextMove == 1;
        CancelInvoke();
        Invoke("Think", 2);
    }

    void DestroyEnemy()
    {
        Destroy(gameObject);
        
    }

    public int HP;

    public void TakeDamage(int damage)
    {
        HP = HP - damage;
    }
}