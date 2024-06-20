using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Bullet : MonoBehaviour
{
    public float speed;
    public float distance;
    public LayerMask isLayer;
    private SpriteRenderer spriteRenderer;
    private Vector2 moveDirection;
    private bool isParried = false;

    //이펙트
    public GameObject Effect;
    public Transform Effect_pos;
    public Vector3 startPosition;


    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyBullet", 10);
        spriteRenderer = GetComponent<SpriteRenderer>();
        moveDirection = transform.right;
    }

    // Update is called once per frame
    void Update()
    {
        MoveBullet();
        CheckCollision();
    }


    private void MoveBullet()
    {
        float currentSpeed = isParried ? speed * 2.0f : speed;
        transform.Translate(moveDirection * -1f * currentSpeed * Time.deltaTime);
    }



    private void CheckCollision()
    {
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, moveDirection, distance, isLayer);
        if (raycast.collider != null)
        {
            HandleCollision(raycast.collider);
        }
    }



    private void HandleCollision(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            collider.GetComponent<Player_Move>().OnDamaged(transform.position); // 플레이어의 OnDamaged 메서드 호출
            GameObject Effectcopy = Instantiate(Effect, Effect_pos.transform.position, transform.rotation);
            Destroy(Effectcopy, 0.4f); // 0.2초 후에 이펙트 삭제
            DestroyBullet();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ShootEnemy"))
        {
            Debug.Log("쾅");
            collision.GetComponent<Enemy>().TakeDamage(2);
            GameObject Effectcopy = Instantiate(Effect, Effect_pos.transform.position, transform.rotation);
            Destroy(Effectcopy, 0.4f); // 0.2초 후에 이펙트 삭제
            DestroyBullet();
        }

        if (collision.CompareTag("Floor"))
        {

            GameObject Effectcopy = Instantiate(Effect, Effect_pos.transform.position, transform.rotation);
            Destroy(Effectcopy, 0.4f); // 0.2초 후에 이펙트 삭제
            DestroyBullet();
        }

    }




    public void DestroyBullet()
    {
        Destroy(gameObject);
    }


    public void Parry()
    {
        moveDirection *= -1;
        if (spriteRenderer.flipX == true)
            spriteRenderer.flipX = false;
        else if (spriteRenderer.flipX == false)
            spriteRenderer.flipX = true;

        isParried = true;
    }



}
