using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// This script takes care of the movement of the player moving between the “X” axis and the “Y” axis.
/// 
/// </summary>
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
    private void FixedUpdate()
    {
        rb2D.MovePosition(rb2D.position + direction * speedMove * Time.fixedDeltaTime);
    }
}
