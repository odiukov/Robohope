using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float Speed = 10, JumpVelocity = 10;
    public LayerMask WhatIsGround;
    public bool CanMoveInAir = true;

    private Transform _myTrans, _tagGround;
    private Rigidbody2D _myBody;
    private bool _isGrounded = false;
    private bool _isFacingRight = true;
    private IControl _controller;
    private Animator _animator;

    void Start()
    {
#if !UNITY_ANDROID && !UNITY_IPHONE && !UNITY_BLACKBERRY && !UNITY_WINRT || UNITY_EDITOR
        _controller = new DesktopControl();
#else
        controller = new MobileControl();
#endif
        //_controller = new MobileControl();
        _controller.Init();
        _animator = GetComponent<Animator>();
        _myBody = GetComponent<Rigidbody2D>();
        _myTrans = transform;
        _tagGround = GameObject.Find(this.name + "/groundChecker").transform;
    }
    void FixedUpdate()
    {
        _isGrounded = Physics2D.Linecast(_myTrans.position, _tagGround.position, WhatIsGround);
        _animator.SetBool("isGrounded", _isGrounded);
        _animator.SetFloat("vSpeed", _myBody.velocity.y);
       

        float hSpeed = _controller.GetHorizontal();
        Move(hSpeed);
        _animator.SetFloat("hSpeed", Mathf.Abs(hSpeed));
    }

    void Update()
    {
        if (_controller.Jump() && _isGrounded && _myBody.velocity.y < 0.01)
            Jump();
    }

    void Move(float horizonalInput)
    {
        if (!CanMoveInAir && !_isGrounded)
            return;
        if (horizonalInput > 0 && !_isFacingRight)
            Flip();
        else if (horizonalInput < 0 && _isFacingRight)
            Flip();

        _myBody.velocity = new Vector2(horizonalInput * Speed, _myBody.velocity.y);
    }
    public void Jump()
    {
        _animator.SetBool("isGrounded", false);
        _myBody.velocity += JumpVelocity * Vector2.up;
    }

    void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 scale = _myTrans.localScale;
        scale.x *= -1;
        _myTrans.localScale = scale;
    }
}
