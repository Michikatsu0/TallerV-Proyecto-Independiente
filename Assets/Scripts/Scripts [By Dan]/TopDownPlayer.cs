using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownPlayer : MonoBehaviour
{
    
    [SerializeField] private float speed = 5f;

    [SerializeField] private int health = 1;
    [SerializeField] private Vector3 direction;

    private Vector3 moveDir;

    private Rigidbody2D rb2D;

    private State state;
    
    private enum State
    {
        Normal,
    }

    private void Awake()
    { 
        rb2D = gameObject.GetComponent<Rigidbody2D>();

        SetStateNormal();
    }

    private void Update()
    {
        switch (state)
        {
            case State.Normal:
                NormalMovement();
                break;
        }
    }
    private void FixedUpdate()
    {
        rb2D.MovePosition(transform.position + moveDir * speed * Time.fixedDeltaTime);        
    }


    private void SetStateNormal()
    {
        state = State.Normal;
    }

    private void NormalMovement()
    {
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            moveY = +1f;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            moveY = -1f;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveX = -1f;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveX = +1f;
        }

        moveDir = new Vector3(moveX, moveY).normalized;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public bool Defeat()
    {
        return health <= 0;
    }
}