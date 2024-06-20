using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public int Health;
    public Player_Move player;

    public Bullet bullet;

    public GameObject[] Stages;

    public Image[] UIhealth;
    public TextMeshProUGUI UIPoint;
    public TextMeshProUGUI UIStage;
    public GameObject UIRestartButton;



    private void Update()
    {
        {
            UIPoint.text = (totalPoint + stagePoint).ToString();
        }
    }
    public void NextStage()
    {   
        //�� ����
        if(stageIndex < Stages.Length -1)
        {
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);
            PlayerReposition();
            bullet.DestroyBullet();

            UIStage.text = "STAGE" + (stageIndex + 1);
        }
        else
        {
            //GameClear

            //�ð� ����
            Time.timeScale = 0;

            //�����
            Debug.Log("Clear");

            bullet.DestroyBullet();
        }


       


        //�������
        totalPoint += stagePoint;
        stagePoint = 0;
    }

    public void HealthDown()
    {
        if (Health > 1)
        {
            Health--;
            UIhealth[Health].color = new Color(1, 0, 0, 0.4f);
        }
           

        else
        {
            //player die effect
            player.OnDie();
            //��� UI
            Debug.Log("�� ����");
            // �ٽ��ϱ� ��ư

            UIRestartButton.SetActive(true);
        }

    }    

    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
           
            if(Health > 1)
            {
               PlayerReposition();
            }


            HealthDown();
        }           
    }

    void PlayerReposition()
    {
        player.transform.position = new Vector3(0, 0, 1);
        player.VelocityZero();
    }
}
