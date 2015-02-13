using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public float Speed = 10, JumpVelocity = 10;
    public LayerMask PlayerMask;
    public bool CanMoveInAir = true;
    Transform _myTrans, _tagGround;
    Rigidbody2D _myBody;
    bool _isGrounded = false;
    private IControl _controller;

    void Start()
    {
#if !UNITY_ANDROID && !UNITY_IPHONE && !UNITY_BLACKBERRY && !UNITY_WINRT || UNITY_EDITOR
        _controller = new DesktopControl();
#else
        controller = new MobileControl();
#endif
       // controller = new MobileControl();
        _controller.Init();
        _myBody = rigidbody2D;
        _myTrans = transform;
        _tagGround = GameObject.Find(this.name + "/groundChecker").transform;
    }
    void FixedUpdate()
    {
        _isGrounded = Physics2D.Linecast(_myTrans.position, _tagGround.position, PlayerMask);
        Move(_controller.GetHorizontal());

        if (_controller.Jump())
            Jump();
    }

    void Move(float horizonalInput)
    {
        if (!CanMoveInAir && !_isGrounded)
            return;

        Vector2 moveVel = _myBody.velocity;
        moveVel.x = horizonalInput * Speed;
        _myBody.velocity = moveVel;
    }
    public void Jump()
    {
        if (_isGrounded)
            _myBody.velocity += JumpVelocity * Vector2.up;
    }

}
