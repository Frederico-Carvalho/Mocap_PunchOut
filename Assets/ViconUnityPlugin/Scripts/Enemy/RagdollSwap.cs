using UnityEngine;

public class RagdollSwap : MonoBehaviour
{
    public Transform[] mocapBones;
    public Transform[] ragdollBones; // mesma ordem que mocapBones
    public GameObject mocapMesh;
    public GameObject ragdollObject;    

    public void TriggerRagdoll()
    {
        for (int i = 0; i < mocapBones.Length; i++)
        {
            ragdollBones[i].position = mocapBones[i].position;
            ragdollBones[i].rotation = mocapBones[i].rotation;
        }

        mocapMesh.SetActive(false);
        ragdollObject.SetActive(true);

        foreach (Rigidbody rb in ragdollObject.GetComponentsInChildren<Rigidbody>())
            rb.isKinematic = false;
    }
}
