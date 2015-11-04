using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float Speed = 10, JumpVelocity = 10;
    public LayerMask WhatIsGround;
    public bool CanMoveInAir = true;

    private Transform _myTrans;
    [SerializeField]
    private GameObject _tagGround;
    private Rigidbody2D _myBody;
    private bool _isGrounded = false;
    private bool _isFacingRight = true;
    private IControl _controller;
    private Animator _animator;

    private Collider2D _tagGroundCol;
    void Start()
    {
#if !UNITY_ANDROID && !UNITY_IPHONE && !UNITY_BLACKBERRY && !UNITY_WINRT || UNITY_EDITOR
        _controller = new DesktopControl();
#else
        controller = new MobileControl();
#endif
        _controller.Init();
        _animator = GetComponent<Animator>();
        _myBody = GetComponent<Rigidbody2D>();
        _myTrans = transform;
        _tagGroundCol = _tagGround.GetComponent<Collider2D>();
    }
    void FixedUpdate()
    {
        _isGrounded = Physics2D.IsTouchingLayers(_tagGroundCol, WhatIsGround);
        _animator.SetBool("isGrounded", _isGrounded);
        _animator.SetFloat("vSpeed", _myBody.velocity.y);
       

        float hSpeed = _controller.GetHorizontal();
        Move(hSpeed);
        _animator.SetFloat("hSpeed", Mathf.Abs(hSpeed));
    }

    void Update()
    {
        if (_controller.Jump() && _isGrounded)
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
