using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlatformerPlayer : MonoBehaviour
{
    [SerializeField] private float _speed = 250.0f;
    [SerializeField] private float _jumpForce = 12.0f;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private float _deltaX;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        _deltaX = Input.GetAxis("Horizontal") * _speed * Time.fixedDeltaTime;
        Vector2 movement = new Vector2(_deltaX, _rigidbody.velocity.y);
        _rigidbody.velocity = movement;

        _animator.SetFloat("speed", Mathf.Abs(_deltaX));
        if (!Mathf.Approximately(_deltaX, 0))
        {
            transform.localScale = new Vector3(Mathf.Sign(_deltaX), 1, 1);
        }
    }
}
