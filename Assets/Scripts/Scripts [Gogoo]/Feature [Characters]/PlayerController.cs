using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum CharacterStates { Normal, Stuned }

    [Header("Player Controller")]
    [SerializeField] public CharacterStates _characterState;
    private Player _player;

    void Start()
    {
        _player = GetComponent<Player>();
        _characterState = CharacterStates.Normal;
    }

    // Update is called once per frame
    void Update()
    {

        switch (_characterState)
        {
            case CharacterStates.Normal:
                if (_player != null)
                {
                    _player.PlayerMechanics();
                }
                break;
            case CharacterStates.Stuned:
                if (_player != null)
                {
                    _player._direction = Vector3.zero;
                    _player._speed = 0;
                    _player._rigidbody2D.velocity = Vector3.zero;
                }    
                break;
        }
    }
}
