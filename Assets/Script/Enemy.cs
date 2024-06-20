using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{
    public int HP;

    public GameManager gameManager;

    public float distance;
    public LayerMask isLayer;
    Rigidbody2D rigid;

    public GameObject bullet;
    public Transform pos;

    public Vector3 startPosition;
    public float maxTime;
    public float curTime;
    SpriteRenderer spriteRenderer;


    //이펙트
    public GameObject Effect;
    public Transform Effect_pos;


    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        currenttime = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public float cooltime;
    private float currenttime;

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, transform.right * -1, distance, isLayer);
       if(raycast.collider != null)
       {
            curTime = 0;

            if (currenttime <= 0)
            {
                GameObject bulletcopy = Instantiate(bullet, pos.transform.position, transform.rotation);

                currenttime = cooltime;
            }
            
            currenttime -= Time.deltaTime;
        }
 

    }


   
    public void TakeDamage(int damage)
    {
        HP -= damage;

        StopCoroutine(HitColorAnimation());
        StartCoroutine(HitColorAnimation());

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

    private IEnumerator HitColorAnimation()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

}
