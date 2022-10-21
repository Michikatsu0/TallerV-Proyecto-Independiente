using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum CharacterStates { Normal, Stuned }

    [Header("Character Controller")]
    [SerializeField] private CharacterStates _characterState;
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
                    _player.DirectionMovement();
                }
                break;
            case CharacterStates.Stuned:
                if (_player != null)
                {

                }    
                break;
        }
    }
}
