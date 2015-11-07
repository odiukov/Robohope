using UnityEngine;
using System.Collections;

public class Saw : Obstacles
{
    public float speed = 1;

    protected override void Start()
    {
        base.Start();
        damage = 1;
    }
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0,0,1 * speed));
	}
}
