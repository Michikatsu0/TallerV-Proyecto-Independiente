using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour
{
    

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
    private bool watchingMe = false;

    [Header("Player Dash")]
    [SerializeField] TrailRenderer _trailRenderer;
    [SerializeField] private float _dashPower = 24f;
    [SerializeField] private float _dashTime = 0.2f;
    [SerializeField] private float _dashCooldown = 1f;
    private bool _canDash = true;
    private bool _isDashing;



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

    public void PlayerMechanics()
    {
        MovementMechanic();

        HiddenMechanic();
        DashMechanic();

    }

    private void MovementMechanic()
    {
        if (!_canHide || _isDashing) return;
        _direction = new Vector3(Input.GetAxis("Horizontal1"), Input.GetAxis("Vertical1"), 0).normalized;
        _rigidbody2D.velocity = new Vector3(_direction.x * _speed, _direction.y * _speed);
    }


    private void DashMechanic()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift) && _canDash)
        {
            StartCoroutine(Dash());
        }
    }
    private IEnumerator Dash()
    {
        _canDash = false;
        _isDashing = true;
        Vector2 originalVelocity = _rigidbody2D.velocity;
        _rigidbody2D.velocity = originalVelocity * _dashPower;
        _trailRenderer.emitting = true;
        yield return new WaitForSeconds(_dashTime);
        _trailRenderer.emitting = false;
        _rigidbody2D.velocity = originalVelocity;
        _isDashing = false;
        yield return new WaitForSeconds(_dashCooldown);
        _canDash = true;
    }


    private void HiddenMechanic()
    {
        if (watchingMe)
        {
            if (Input.GetKeyDown(KeyCode.Space) && _canHide)
            {
                _rigidbody2D.velocity = Vector2.zero;
                _canHide = false;
                _renderer.color = _isInvisible;

            }
            else if(Input.GetKeyUp(KeyCode.Space))
            {
                _canHide = true;
                _renderer.color = _isVisible;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && _canHide)
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
            else if (Input.GetKeyUp(KeyCode.Space))
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
