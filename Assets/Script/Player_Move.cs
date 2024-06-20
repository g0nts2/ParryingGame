using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Move : MonoBehaviour
{
    public GameManager gameManager;

    public float maxSpeed;
    public float jumpPower;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    bool isjump;
    private bool ismove = true;

    

    public Bullet[] bulletArray;

    public static Action player;

    //�и� ����Ʈ
    public GameObject Effect;
    public Transform Effect_pos_R;
    public Transform Effect_pos_L;
    public Vector3 startPosition;

    //���
    public float DashForce;
    public float StartDashTimer;
    private float CurrentDashTimer;
    private bool isDashing;
    //��� ����
    private int DashDirection;


    private float curTime;
    public float coolTime = 0.5f;
    public Transform pos1;
    public Transform pos2;
    public Transform pos3;
    public Vector2 boxSize_R;
    public Vector2 boxSize_L;
    public Vector2 boxSize_U;



    //���� ������ ���� Ȯ��
    private bool isAttacking = false;



    private void Awake()
    {
        //maxHp = 50;
        //nowHp = 50;
        //atkDmg = 10;


        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        isjump = false;
       
    }


    private void Update()
    {
        // �߰�: ���� ���̸� ������ ����
        if (isAttacking) 
            return;


        // ��ð� �ƴ� �� ������
        if (!isDashing)
        {
            // stop speed
            if (ismove == true)
            {
                if (Input.GetButtonUp("Horizontal"))
                {
                    rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
                }

                if (Input.GetButton("Horizontal"))
                {
                    spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
                }
            }
        }


        // ����
        if (Input.GetButtonDown("Jump") && !anim.GetBool("IsJump") && ismove == true && !anim.GetBool("IsDash"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("IsJump", true);
            isjump = true;

        }

        // �ٴڿ� ���� ������ IsJump �ִϸ��̼� ����
        if (isjump)
        {
            anim.SetBool("IsJump", true);
        }

        //���
        if(Input.GetKeyDown(KeyCode.LeftShift) && ismove == true  && !anim.GetBool("IsJump") && !anim.GetBool("IsDash"))
        {
            isDashing = true;

            CurrentDashTimer = StartDashTimer;
            DashDirection = spriteRenderer.flipX ? -1 : 1; ;
            rigid.velocity = Vector2.zero;
            
        }

        if(isDashing)
        {
            rigid.velocity = (Vector2.right * DashDirection * DashForce * maxSpeed);
            
            CurrentDashTimer -= Time.deltaTime;

            gameObject.layer = 17;

            spriteRenderer.color = new Color(1, 1, 1, 0.5f);

            //Animation
            anim.SetBool("IsDash", true);

            Invoke("OffDash", 0.35f);

            if (CurrentDashTimer <= 0)
            {
                isDashing=false;
                anim.SetBool("IsDash", false);
                rigid.velocity = Vector2.zero;
            }
        }


        if (rigid.velocity.normalized.x == 0)
            anim.SetBool("IsRun", false);
        else
            anim.SetBool("IsRun", true);




        // Z ��ư ����
        if(ismove == true)
            if (curTime <= 0)
            {
                if (Input.GetKey(KeyCode.Z))
                {
                    if (Input.GetKey(KeyCode.UpArrow))
                    {
                        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos3.position, boxSize_U, 0);

                        foreach (Collider2D collider in collider2Ds)
                        {

                            if (collider.tag == "ShootEnemy")
                            {
                                collider.GetComponent<Enemy>().TakeDamage(1);
                            }

                            else if (collider.tag == "Enemy")
                            {
                                collider.GetComponent<Enemy_S>().TakeDamage(1);
                                
                            }

                            else if (collider.tag == "FlyingEnemy")
                            {
                                collider.GetComponent<Flying_Chase>().TakeDamage(1);
                                collider.GetComponent<Chaing_mon>().TakeDamage(1);
                            }
                            else if (collider.tag == "ChasingEnemy")
                            {
                                collider.GetComponent<Chaing_mon>().TakeDamage(1);
                            }

                            else if (collider.tag == "bullet")
                            {
                                collider.GetComponent<Bullet>().Parry();
                                GameObject Effectcopy = Instantiate(Effect, Effect_pos_L.transform.position, transform.rotation);
                                Destroy(Effectcopy, 0.22f); // 0.2�� �Ŀ� ����Ʈ ����
                            }
                        }
                    }
                    else
                    {
                        if (spriteRenderer.flipX == false)
                        {
                            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos1.position, boxSize_R, 0);
                            foreach (Collider2D collider in collider2Ds)
                            {

                                if (collider.tag == "ShootEnemy")
                                {
                                    collider.GetComponent<Enemy>().TakeDamage(1);                                 
                                }

                                else if (collider.tag == "Enemy")
                                {
                                    collider.GetComponent<Enemy_S>().TakeDamage(1);
                                    
                                }

                                else if (collider.tag == "FlyingEnemy")
                                {
                                    collider.GetComponent<Flying_Chase>().TakeDamage(1);                          
                                }

                                else if (collider.tag == "ChasingEnemy")
                                {
                                    collider.GetComponent<Chaing_mon>().TakeDamage(1);
                                }

                                else if (collider.tag == "bullet")
                                {
                                    collider.GetComponent<Bullet>().Parry();
                                    GameObject Effectcopy = Instantiate(Effect, Effect_pos_R.transform.position, transform.rotation);
                                    Destroy(Effectcopy, 0.22f);
                                }


                            }
                        }
                        else if (spriteRenderer.flipX == true)
                        {
                            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos2.position, boxSize_L, 0);

                            foreach (Collider2D collider in collider2Ds)
                            {

                                if (collider.tag == "ShootEnemy")
                                {
                                    collider.GetComponent<Enemy>().TakeDamage(1);                                   
                                }

                                else if (collider.tag == "Enemy")
                                {
                                    collider.GetComponent<Enemy_S>().TakeDamage(1);
                                    
                                }

                                else if (collider.tag == "FlyingEnemy")
                                {
                                    collider.GetComponent<Flying_Chase>().TakeDamage(1);  
                                }

                                else if (collider.tag == "ChasingEnemy")
                                {
                                    collider.GetComponent<Chaing_mon>().TakeDamage(1);
                                }

                                else if (collider.tag == "bullet")
                                {
                                    collider.GetComponent<Bullet>().Parry();
                                    GameObject Effectcopy = Instantiate(Effect, Effect_pos_L.transform.position, transform.rotation);
                                    Destroy(Effectcopy, 0.22f); // 0.2�� �Ŀ� ����Ʈ ����
                                }
                            }
                        }


                    }
                   
                    //�޸��� ����
                    if (Input.GetButton("Horizontal") && !anim.GetBool("IsJump") && ismove == true)
                    {
                        anim.SetTrigger("R_atk");
                    }


                    //��������
                    else if (anim.GetBool("IsJump"))
                    {
                        anim.SetTrigger("J_atk");
                    }


                    //�Ϲݰ���
                    else if (!Input.GetButton("Horizontal") && !anim.GetBool("IsJump"))
                    {
                        //������
                        if (Input.GetKey(KeyCode.UpArrow))
                        {
                            anim.SetTrigger("U_atk");
                            StartCoroutine(DisableMovementDuringAttack());
                        }
                        else
                        {
                            anim.SetTrigger("S_atk");
                            StartCoroutine(DisableMovementDuringAttack()); // �߰�: ���� ���� ������ ����
                        }
                    }
                    curTime = coolTime;
                }
            }

            else
            curTime -= Time.deltaTime;
    }



    //�ִϸ��̼� �̺�Ʈ�� ȣ��
    public void OnAttackAnimationEnd()
    {
        // ������ ������ ������
        isAttacking = false;
    }

    IEnumerator DisableMovementDuringAttack()
    {
        // ���� �� ������ ����
        isAttacking = true; 
        
        //�ִϸ��̼��� ���� ������ ��ٸ�
        yield return new WaitForSeconds(0.2f);

        isAttacking = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // ���� ���̸� ������ ����
        if (isAttacking)
            return;

        //��� �� ������ ����
        if (!isDashing)
        {
            if (ismove == true)
            {
                float h = Input.GetAxisRaw("Horizontal");
                rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

                if (rigid.velocity.x > maxSpeed)
                    rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
                else if (rigid.velocity.x < maxSpeed * (-1))
                    rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
            }
        }



        //����
        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1.5f, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1.5f, LayerMask.GetMask("Floor", "Platform"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 1.5f)
                {
                    anim.SetBool("IsJump", false);
                    //anim.SetTrigger("Landing");
                    isjump = false;
                }
                
            }
            else
            {
                // �ٴڿ� ���� �ʾ��� �� IsJump �ִϸ��̼� ����
                anim.SetBool("IsJump", true);
            }
        }


    }


    void OffDash()
    {
        gameObject.layer = 10;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }


    //�÷��̾� ���� ����
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos1.position, boxSize_R);
        Gizmos.DrawWireCube(pos2.position, boxSize_L);
        Gizmos.DrawWireCube(pos3.position, boxSize_U);
    }

    //���� �÷��̾� ����
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            OnDamaged(collision.transform.position);           
        }

        else if (collision.gameObject.tag == "FlyingEnemy")
        {
            OnDamaged(collision.transform.position);           
        }

        else if (collision.gameObject.tag == "Obstacle")
        {
            OnDamaged_Ob(collision.transform.position);            
        }

        else if (collision.gameObject.tag == "ShootEnemy")
        {
            OnDamaged(collision.transform.position);       
        }

        else if (collision.gameObject.tag == "ChasingEnemy")
        {
            OnDamaged(collision.transform.position);
        }

        else if (collision.gameObject.tag == "bullet")
        {
            OnDamaged(collision.transform.position);          
        }

    }
    
    //���� �� ������ ����
    public void OnDamaged(Vector2 targetPos)
    {
        gameManager.Health--;
        gameObject.layer = 17;

        rigid.velocity = Vector2.zero;
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector3(dirc, 0.1f) * 6, ForceMode2D.Impulse);
        ismove = false;


        //Animation
        anim.SetTrigger("IsDamaged");

        Invoke("OffDamaged", 0.7f);
    }

    public void OnDamaged_Ob(Vector2 targetPos)
    {
        gameManager.HealthDown();
        gameObject.layer = 17;

        rigid.velocity = Vector2.zero;
        int dirc = transform.position.x - targetPos.x >= 0 ? 1 : -1;
        rigid.AddForce(new Vector3(dirc, 2) * 6, ForceMode2D.Impulse);
        ismove = false;


        //Animation
        anim.SetTrigger("IsDamaged");

        Invoke("OffDamaged", 0.7f);
    }


    //������ ���� ���
    void OffDamaged()
    {
        gameObject.layer = 10;
        
        ismove = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //�� ȹ��
        if(collision.gameObject.tag == "Item")
        {
            //point +1
            gameManager.stagePoint += 100;

            collision.gameObject.SetActive(false);
        }

        else if(collision.gameObject.tag =="Finish")
        {
            //Next Stage
            gameManager.NextStage();
        }
    }

    public void OnDie()
    {

    }

    public void VelocityZero()
    {
        rigid.velocity = Vector2.zero;
    }
}