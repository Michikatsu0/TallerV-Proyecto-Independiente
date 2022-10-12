using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerUpEffect powerUpEffect;
    
    [SerializeField] private float scoreAmount;
    [SerializeField] private Score score;
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        powerUpEffect.Apply(collision.gameObject);
        score.PlusScore(scoreAmount);
        Destroy(gameObject);
            
    }
}
