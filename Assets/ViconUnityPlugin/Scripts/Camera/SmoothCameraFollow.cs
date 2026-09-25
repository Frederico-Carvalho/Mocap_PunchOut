using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    public Transform headBone;
    [Range(0f, 1f)] public float smoothing = 0.3f;
    [HideInInspector] public Vector3 shakeOffset;

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, headBone.position, smoothing) + shakeOffset;
        transform.rotation = Quaternion.Slerp(transform.rotation, headBone.rotation, smoothing);
    }
}