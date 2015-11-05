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
    public bool CanMoveInAir = true;

    private SpriteRenderer healthBar;			// Reference to the sprite renderer of the health bar.
    private Vector3 healthScale;				// The local scale of the health bar initially (with full health).

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

    void Start()
    {
        healthBar = transform.Find("HealthBar").GetComponent<SpriteRenderer>();
        healthScale = healthBar.transform.localScale;
    }

    public void ChangeHp(float value)
    {
        _hp += value;

        if (_hp > _maxHP)
            _hp = 100;
        else if (_hp < 0)
        {
            _hp = 0;
            Death();
        }
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - _hp * 0.01f);
        healthBar.transform.localScale = new Vector3(healthScale.x * _hp * 0.01f, 1, 1);
    }

    public void Death()
    {
        Debug.Log("You dead");
        Application.LoadLevel(0);
    }
}
