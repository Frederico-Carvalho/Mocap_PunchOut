using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HitFeedback : MonoBehaviour
{
    public Image flashImage;
    public float flashDuration = 0.2f;
    public CameraShake cameraShake;

    public void TriggerHit()
    {
        StopAllCoroutines();
        StartCoroutine(FlashRoutine());
        cameraShake.TriggerShake();
    }

    IEnumerator FlashRoutine()
    {
        Color c = flashImage.color;
        c.a = 0.5f;
        flashImage.color = c;

        float t = 0f;
        while (t < flashDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(0.5f, 0f, t / flashDuration);
            flashImage.color = c;
            yield return null;
        }
    }
}