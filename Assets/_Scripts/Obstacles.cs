using UnityEngine;
using System.Collections;

public class Obstacles : MonoBehaviour {
    
    [SerializeField]
    protected float damage = 25;

    protected virtual void Start()
    {

    }

    protected void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerStats.Instance.ChangeHp(-damage);
        }
    }
}
