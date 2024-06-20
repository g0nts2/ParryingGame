using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying_Chase : MonoBehaviour
{
    public GameManager gameManager;

    public float speed;
    public bool chase = false;
    public Transform startingPoint;
    private GameObject player;
    private SpriteRenderer spriteRenderer;


    //이펙트
    public GameObject Effect;
    public Transform Effect_pos;
 


    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            return;
        if(chase == true)
            Chase();
        else
           ReturnStartPoint();
        Flip();

        if (HP <= 0)
        {
            gameManager.stagePoint += 100;

            DestroyEnemy();
            GameObject Effectcopy = Instantiate(Effect, Effect_pos.transform.position, transform.rotation);
            Destroy(Effectcopy, 0.45f); // n초 후에 이펙트 삭제
        }
    }

    private void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    private void ReturnStartPoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, startingPoint.position, speed * Time.deltaTime);
    }

    private void Flip()
    {
        if (transform.position.x > player.transform.position.x)
            spriteRenderer.flipX = false;
        else
            spriteRenderer.flipX = true;
    }

    public int HP;

    public void TakeDamage(int damage)
    {
        HP = HP - damage;
    }

    void DestroyEnemy()
    {
        Destroy(gameObject);
    }

}
