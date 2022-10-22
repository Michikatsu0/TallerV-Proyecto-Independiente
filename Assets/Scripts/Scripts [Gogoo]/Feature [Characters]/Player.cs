using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour
{
    private bool watchingMe = false;

    [Header("Player Movement")]
    [SerializeField] private float _speed;
    private Rigidbody2D _rigidbody2D;
    private static Vector3 _direction;

    [Header("Player Hide")]
    [SerializeField] private Color _isVisible;
    [SerializeField] private Color _isInvisible;
    //[SerializeField] private GameObject _barrelPrefab;
    private SpriteRenderer _renderer; // temporal hidden mecanic, it's open to a remake
    private ShadowCaster2D _shadowCaster2D;
    private Light2D _light2D;
    private Collider2D _collider2D;
    private bool _canHide = true;
    private bool _isHiding;

    public bool IsHiding { get => _isHiding; set => _isHiding = value; }
    public bool CanHide { get => _canHide; set => _canHide = value; }
    public bool WatchingMe { get => watchingMe; set => watchingMe = value; }

    void Awake()
    {
        //_barrelPrefab.SetActive(false);
        _renderer = GetComponentInChildren<SpriteRenderer>();
        _light2D = GetComponentInChildren<Light2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
        _renderer.color = _isVisible;
        _shadowCaster2D = GetComponentInChildren<ShadowCaster2D>();
    }

    public void PlayerMecanics()
    {
        MovementMecanics();
        HiddenMecanic();

    }

    private void MovementMecanics()
    {
        if (_canHide)
        {
            _direction = new Vector3(Input.GetAxis("Horizontal1"), Input.GetAxis("Vertical1"), 0);
            _rigidbody2D.velocity = new Vector3(_direction.x * _speed, _direction.y * _speed);
        }
    }


    private void HiddenMecanic()
    {
        if (watchingMe)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && _canHide)
            {
                _rigidbody2D.velocity = Vector2.zero;
                _canHide = false;
                _renderer.color = _isInvisible;

            }
            else if(Input.GetKeyUp(KeyCode.LeftShift))
            {
                _canHide = true;
                _renderer.color = _isVisible;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && _canHide)
            {
                //_barrelPrefab.SetActive(true);
                _collider2D.isTrigger = true;
                _light2D.enabled = false;
                _rigidbody2D.velocity = Vector2.zero;
                _shadowCaster2D.castsShadows = false;
                _canHide = false;
                _isHiding = true;
                _renderer.color = _isInvisible;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                //_barrelPrefab.SetActive(false);
                _collider2D.isTrigger = false;
                _light2D.enabled = true;
                _shadowCaster2D.castsShadows = true;
                _canHide = true;
                _isHiding = false;
                _renderer.color = _isVisible;
            }
        }

        
    }

}
