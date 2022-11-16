using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUp : MonoBehaviour
{
    //public PowerUpEffect powerUpEffect;
    
    [SerializeField] public float scoreAmount;
    [SerializeField] private Score score;
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        //powerUpEffect.Apply(collision.gameObject);
        Score.Instance.PlusScore(scoreAmount);
        Destroy(gameObject);
            
    }
}
