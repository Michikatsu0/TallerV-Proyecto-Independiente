using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    public Transform _player;
    public float _smooth = 0.3f;
    Vector3 _velocity = Vector3.zero;
    private float sizePosZ = -10;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 posicion = new Vector3();
        posicion.x = _player.position.x;
        posicion.y = _player.position.y;
        posicion.z = _player.position.y + sizePosZ;
        transform.position = Vector3.SmoothDamp(transform.position, posicion, ref _velocity, _smooth);
    }
}
