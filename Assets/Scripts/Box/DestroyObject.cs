using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 2f); // Destroys the object after 2 seconds
    }
}
