using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaing_mon : MonoBehaviour
{
    public GameManager gameManager;

  
    private GameObject player;
    private SpriteRenderer spriteRenderer;

    Animator anim;
    public bool Move = false;

    public int HP;

    //이펙트
    public GameObject Effect;
    public Transform Effect_pos;


    public Transform startingPoint;
    public float distance;
    public LayerMask isLayer;
    public float speed;
    Rigidbody2D rigid;
    public float curTime;
    public float maxTime;

    private float currenttime;


    // Start is called before the first frame update
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }






    private void FixedUpdate()
    {
        //플레이어 쫓아감

        Debug.DrawRay(rigid.position, Vector3.left, new Color(0, distance, 0));
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, transform.right * -1, distance, isLayer);
        if (raycast.collider != null)
        {
            curTime = 0;
            transform.position = Vector3.MoveTowards(transform.position, raycast.collider.transform.position, Time.deltaTime * speed);
            anim.SetBool("run", true);
            currenttime -= Time.deltaTime;
            spriteRenderer.flipX = false;
        }

        //되돌아감
        else
        {
            currenttime += Time.deltaTime;
            anim.SetBool("run", false);
            if (currenttime >= maxTime)
            {
                transform.position = Vector3.MoveTowards(transform.position, startingPoint.position, Time.deltaTime * speed);
            }
            spriteRenderer.flipX = true;

        }

     
    }

    //private void Flip()
    //{
        

    //    if (transform.position.x > player.transform.position.x)
    //    {
    //        spriteRenderer.flipX = false;
    //    }

    //    else
    //        spriteRenderer.flipX = true;
    //}



    public void TakeDamage(int damage)
    {
        HP = HP - damage;


        if (HP <= 0)
        {
            gameManager.stagePoint += 100;

            DestroyEnemy();
            GameObject Effectcopy = Instantiate(Effect, Effect_pos.transform.position, transform.rotation);
            Destroy(Effectcopy, 0.45f); // n초 후에 이펙트 삭제
        }

    }

    void DestroyEnemy()
    {
        Destroy(gameObject);
    }

}