using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseControll : MonoBehaviour
{
    public Flying_Chase[] enemyArray;
    public Chaing_mon[] enemyArray2;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (Flying_Chase enemy in enemyArray)
            {
                enemy.chase = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (Flying_Chase enemy in enemyArray)
            {
                enemy.chase = false;
            }
        }
    }

}

