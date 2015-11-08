using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private Transform _myTrans;
    private bool _isGrounded = false;
    private bool _isFacingRight = true;
    private IControl _controller;
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
        _myTrans = transform;
    }
    void FixedUpdate()
    {
        if (!PlayerStats.Instance.isDead)
        {
            _isGrounded = Physics2D.IsTouchingLayers(_tagGroundCol, PlayerStats.Instance.WhatIsGround);
            PlayerStats.Instance.Animator.SetBool("isGrounded", _isGrounded);
            PlayerStats.Instance.Animator.SetFloat("vSpeed", PlayerStats.Instance.Body.velocity.y);

            float hSpeed = _controller.GetHorizontal();
            PlayerStats.Instance.Animator.SetFloat("hSpeed", Mathf.Abs(hSpeed));
            Move(hSpeed);
        }
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
        PlayerStats.Instance.Body.velocity = new Vector2(horizonalInput * PlayerStats.Instance.Speed, PlayerStats.Instance.Body.velocity.y);
    }
    public void Jump()
    {
        PlayerStats.Instance.Animator.SetBool("isGrounded", false);
        PlayerStats.Instance.Body.velocity += PlayerStats.Instance.JumpForce * Vector2.up;
    }
    void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 scale = _myTrans.localScale;
        scale.x *= -1;
        _myTrans.localScale = scale;
    }
}