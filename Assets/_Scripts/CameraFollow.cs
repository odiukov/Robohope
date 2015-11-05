using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    private Vector3 velocity = Vector3.zero;
    public float offsetX = 0f;
    public float offsetY = 2.5f;

    public float smoothing = 0.15f;

    void Awake()
    {
        transform.position = new Vector3(target.position.x + offsetX, target.position.y + offsetY, transform.position.z);
    }
    void Update()
    {
        if (target)
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(target.position.x + offsetX, target.position.y + offsetY, transform.position.z), ref velocity, smoothing);
        }
    }
}
