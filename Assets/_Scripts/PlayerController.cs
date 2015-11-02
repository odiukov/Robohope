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

    void Start()
    {
#if !UNITY_ANDROID && !UNITY_IPHONE && !UNITY_BLACKBERRY && !UNITY_WINRT || UNITY_EDITOR
        _controller = new DesktopControl();
#else
        controller = new MobileControl();
#endif
        _controller = new MobileControl();
        _controller.Init();
        _myBody = GetComponent<Rigidbody2D>();
        _myTrans = transform;
        _tagGround = GameObject.Find(this.name + "/groundChecker").transform;
    }
    void FixedUpdate()
    {
        _isGrounded = Physics2D.Linecast(_myTrans.position, _tagGround.position, WhatIsGround);
        Move(_controller.GetHorizontal());
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
        Vector2 moveVel = _myBody.velocity;
        moveVel.x = horizonalInput * Speed;
        _myBody.velocity = moveVel;
    }
    public void Jump()
    {
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
