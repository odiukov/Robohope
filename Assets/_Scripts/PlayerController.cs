using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Transform _myTrans;
    private Rigidbody2D _myBody;
    private bool _isGrounded = false;
    private bool _isFacingRight = true;
    private IControl _controller;
    private Animator _animator;
    [SerializeField]
    private Collider2D _tagGroundCol;
    void Start()
    {
#if !UNITY_ANDROID && !UNITY_IPHONE && !UNITY_BLACKBERRY && !UNITY_WINRT || UNITY_EDITOR
        _controller = new DesktopControl();
#else
        _controller = new MobileControl();
#endif
        _controller.Init();
        _animator = GetComponent<Animator>();
        _myBody = GetComponent<Rigidbody2D>();
        _myTrans = transform;
    }
    void FixedUpdate()
    {
        _isGrounded = Physics2D.IsTouchingLayers(_tagGroundCol, PlayerStats.Instance.WhatIsGround);
        _animator.SetBool("isGrounded", _isGrounded);
        _animator.SetFloat("vSpeed", _myBody.velocity.y);
       

        float hSpeed = _controller.GetHorizontal();
        _animator.SetFloat("hSpeed", Mathf.Abs(hSpeed));
        Move(hSpeed);
    }

    void Update()
    {
        if (_controller.Jump() && _isGrounded)
            Jump();
    }

    void Move(float horizonalInput)
    {
        if (horizonalInput > 0 && !_isFacingRight)
            Flip();
        else if (horizonalInput < 0 && _isFacingRight)
            Flip();
        _myBody.velocity = new Vector2(horizonalInput * PlayerStats.Instance.Speed, _myBody.velocity.y);
    }
    public void Jump()
    {
        _animator.SetBool("isGrounded", false);
        _myBody.velocity += PlayerStats.Instance.JumpForce * Vector2.up;
    }

    void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 scale = _myTrans.localScale;
        scale.x *= -1;
        _myTrans.localScale = scale;
    }
}
