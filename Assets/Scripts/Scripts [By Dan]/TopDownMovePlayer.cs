using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovePlayer : MonoBehaviour
{
    [SerializeField] private float speedMove = 5f;
    [SerializeField] private Vector2 direction;

    private Rigidbody2D rb2D;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }
    //Displacement in the "X" axes and "Y" axes
    private void FixedUpdate()
    {
        rb2D.MovePosition(rb2D.position + direction * speedMove * Time.fixedDeltaTime);
    }
}
