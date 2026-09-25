using UnityEngine;

using UnityEngine;
using System.Collections;

public class FlashSelfDestruct : MonoBehaviour
{
    [SerializeField] private Light flashLight;
    [SerializeField] private ParticleSystem flashParticles;
    [SerializeField] private float lightDuration = 0.15f;
    [SerializeField] private float maxIntensity = 10f;

    private void Start()
    {
        flashParticles.Play();
        StartCoroutine(FlashLightRoutine());
    }

    private IEnumerator FlashLightRoutine()
    {
        flashLight.intensity = maxIntensity;

        float t = 0f;
        while (t < lightDuration)
        {
            t += Time.deltaTime;
            flashLight.intensity = Mathf.Lerp(maxIntensity, 0f, t / lightDuration);
            yield return null;
        }

        Destroy(gameObject);
    }
}
