using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
    private static PlayerStats _instance;

    public static PlayerStats Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<PlayerStats>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    public float HP = 100;
    private float _maxHP = 100;

    // Use this for initialization
    void Start()
    {
        HP = 100;
    }

    public void ChangeHp(float value)
    {
        if (HP + value > _maxHP)
            HP = 100;
        else if (HP + value < 0)
        {
            HP = 0;
            Death();
        }
        else
            HP += value;
    }

    public void Death()
    {
        Debug.Log("You dead");
    }
}
