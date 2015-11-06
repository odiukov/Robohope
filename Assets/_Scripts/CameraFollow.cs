using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {


    public Collider2D target;
    public float verticalOffset;
    public Vector2 focusAreaSize;
    private Vector3 velocity = Vector3.zero;
    public float offsetX = 0f;
    public float offsetY = 0f;

    public float smoothing = 0.15f;
    FocusArea focusArea;
    
    Bounds bounds;
    void Start()
    {
        bounds = target.bounds;
        focusArea = new FocusArea(bounds, focusAreaSize);
    }

    void LateUpdate()
    {
        bounds = target.bounds;
        focusArea.Update(bounds);

        Vector2 focusPosition = focusArea.centre + Vector2.up * verticalOffset;
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(focusPosition.x + offsetX, focusPosition.y + offsetY, transform.position.z), ref velocity, smoothing);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, .5f);
        Gizmos.DrawCube(focusArea.centre, focusAreaSize);
    }

    struct FocusArea
    {
        public Vector2 centre;
        public Vector2 velocity;
        float left, right;
        float top, bottom;


        public FocusArea(Bounds targetBounds, Vector2 size)
        {
            left = targetBounds.center.x - size.x / 2;
            right = targetBounds.center.x + size.x / 2;
            bottom = targetBounds.min.y;
            top = targetBounds.min.y + size.y;

            velocity = Vector2.zero;
            centre = new Vector2((left + right) / 2, (top + bottom) / 2);
        }

        public void Update(Bounds targetBounds)
        {
            float shiftX = 0;
            if (targetBounds.min.x < left)
            {
                shiftX = targetBounds.min.x - left;
            }
            else if (targetBounds.max.x > right)
            {
                shiftX = targetBounds.max.x - right;
            }
            left += shiftX;
            right += shiftX;

            float shiftY = 0;
            if (targetBounds.min.y < bottom)
            {
                shiftY = targetBounds.min.y - bottom;
            }
            else if (targetBounds.max.y > top)
            {
                shiftY = targetBounds.max.y - top;
            }
            top += shiftY;
            bottom += shiftY;
            centre = new Vector2((left + right) / 2, (top + bottom) / 2);
            velocity = new Vector2(shiftX, shiftY);
        }
    }
}
