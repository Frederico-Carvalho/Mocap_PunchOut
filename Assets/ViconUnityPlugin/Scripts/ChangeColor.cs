using UnityEngine;

public class ChangeColorOnCollision : MonoBehaviour
{
    private Renderer rend;
    public Color hitColor = Color.red;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Colidiu com: " + collision.gameObject.name);
        rend.material.color = hitColor;
    }
}