using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.2f;
    public float shakeMagnitude = 0.02f;
    public SmoothCameraFollow cameraFollow;

    public void TriggerShake()
    {
        StopAllCoroutines();
        StartCoroutine(ShakeRoutine());
    }

    IEnumerator ShakeRoutine()
    {
        float t = 0f;
        while (t < shakeDuration)
        {
            t += Time.deltaTime;
            cameraFollow.shakeOffset = Random.insideUnitSphere * shakeMagnitude * (1f - t / shakeDuration);
            yield return null;
        }
        cameraFollow.shakeOffset = Vector3.zero;
    }
}