using UnityEngine;
using System.Collections;

public class Saw : MonoBehaviour {

    public float speed = 1;
	
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            PlayerStats.Instance.ChangeHp(-1);
        }
    }
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0,0,1 * speed));
	}
}
