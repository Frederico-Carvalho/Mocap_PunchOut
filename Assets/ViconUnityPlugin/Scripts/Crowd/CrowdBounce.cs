using UnityEngine;

public class CrowdBounce : MonoBehaviour
{
    [SerializeField] float amplitude = 0.3f;
    [SerializeField] float speed = 2f;

    Vector3 startPos;
    float offset;

    void Start()
    {
        startPos = transform.position;
        offset = Random.Range(0f, 100f);
    }

    void Update()
    {
        float y = Mathf.Abs(Mathf.Sin((Time.time + offset) * speed)) * amplitude;
        transform.position = startPos + Vector3.up * y;
    }
}