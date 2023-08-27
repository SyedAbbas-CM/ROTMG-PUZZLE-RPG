using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GenericEnemyMovement : MonoBehaviour
{
    Rigidbody2D enemyRB;
    public int Speed = 5;
    float randomMove = 0;
    private void Awake()
    {
        enemyRB = GetComponent<Rigidbody2D>();
    }


    private void Start()

    {
        randomMove = Random.Range(1, 4);
        if(randomMove == 0)
        {
            enemyRB.velocity = Vector2.right * Speed;
        }
        else if(randomMove == 1)
        {
            enemyRB.velocity = Vector2.left * Speed;

        }
        if (randomMove == 3)
        {
            enemyRB.velocity = Vector2.up * Speed;
        }
        else if (randomMove == 4)
        {
            enemyRB.velocity = Vector2.down * Speed;

        }
    }
}
