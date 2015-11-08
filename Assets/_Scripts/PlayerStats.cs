using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
    private static PlayerStats _instance;

    [SerializeField]
    private float _speed = 10, _jumpForce = 7;
    [SerializeField]
    private float _hp = 100;
    private float _maxHP = 100;
    public float Speed { get { return _speed; } }
    public float JumpForce { get { return _jumpForce; } }

    public LayerMask WhatIsGround;
    private Animator _animator;
    public Animator Animator { get { return _animator; } }
    private Rigidbody2D _myBody;
    public Rigidbody2D Body { get { return _myBody; } }
    private SpriteRenderer healthBar;
    private Vector3 healthScale;
    public bool isDead { get; set; }

    public static PlayerStats Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<PlayerStats>();
            }
            return _instance;
        }
    }

    void Awake()
    {
        _myBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        healthBar = transform.Find("HealthBar").GetComponent<SpriteRenderer>();
        healthScale = healthBar.transform.localScale;
    }

    public void ChangeHp(float value)
    {
        _hp += value;

        if (_hp > _maxHP)
            _hp = 100;
        else if (_hp <= 0)
        {
            _hp = 0;
            _myBody.isKinematic = true;
            isDead = true;
            Animator.SetBool("isGrounded", true);
            Animator.Play("death");
            StartCoroutine("Death", 1f); ;
        }
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - _hp * 0.01f);
        healthBar.transform.localScale = new Vector3(healthScale.x * _hp * 0.01f, 1, 1);
    }

    IEnumerator Death(float time)
    {
        yield return new WaitForSeconds(time);
        Application.LoadLevel(0);
    }
    /*public void Death()
    {
        Debug.Log("You dead");
        Application.LoadLevel(0);
    }*/
}
