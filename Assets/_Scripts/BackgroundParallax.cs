using UnityEngine;
using System.Collections.Generic;

public class BackgroundParallax : MonoBehaviour {

    public Transform[] backgrounds;
    public float smoothing = 2;

    private float[] scales;
    private Vector2 startCameraPosition;
    private float paralax;

	void Start () {
        scales = new float[backgrounds.Length];
        for (int i = 0; i < backgrounds.Length; i++)
        {
            scales[i] = backgrounds[i].position.z * -1;
        }
        startCameraPosition = Camera.main.transform.position;
    }
	
	void Update () {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            paralax = (startCameraPosition.x - Camera.main.transform.position.x) * scales[i];
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, new Vector3(backgrounds[i].position.x + paralax, backgrounds[i].position.y, backgrounds[i].position.z), Time.deltaTime * smoothing);
        }
        startCameraPosition = Camera.main.transform.position;
    }
}
