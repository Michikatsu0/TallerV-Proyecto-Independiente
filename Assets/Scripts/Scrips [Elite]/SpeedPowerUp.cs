using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/SpeedPowerUp")]
public class SpeedPowerUp : PowerUpEffect
{
    public float buff;
    

    public override void Apply(GameObject target)
    {
        target.GetComponent<TopDownPlayer>().speed += buff;
    }
}
