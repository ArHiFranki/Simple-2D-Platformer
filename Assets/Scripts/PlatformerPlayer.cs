using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(BoxCollider2D))]
public class PlatformerPlayer : MonoBehaviour
{
    [SerializeField] private float _speed = 250.0f;
    [SerializeField] private float _jumpForce = 12.0f;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private BoxCollider2D _boxCollider;
    private float _deltaX;
    private bool _isGrounded;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        _deltaX = Input.GetAxis("Horizontal") * _speed * Time.fixedDeltaTime;
        Vector2 movement = new Vector2(_deltaX, _rigidbody.velocity.y);
        _rigidbody.velocity = movement;

        Vector3 max = _boxCollider.bounds.max;
        Vector3 min = _boxCollider.bounds.min;

        Vector2 corner1 = new Vector2(max.x, min.y - 0.1f);
        Vector2 corner2 = new Vector2(min.x, min.y - 0.2f);
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);

        _isGrounded = false;
        if (hit != null)
        {
            _isGrounded = true;
        }

        _animator.SetFloat("speed", Mathf.Abs(_deltaX));
        if (!Mathf.Approximately(_deltaX, 0))
        {
            transform.localScale = new Vector3(Mathf.Sign(_deltaX), 1, 1);
        }

        _rigidbody.gravityScale = _isGrounded && _deltaX == 0 ? 0 : 1;
    }
}
