using UnityEngine;

public class DestrucableBox : MonoBehaviour
{
    public GameObject[] pieces; // kéo 4 mảnh vào
    public float force = 5f;

    public void Break()
    {
        foreach (GameObject piece in pieces)
        {
            GameObject p = Instantiate(piece, transform.position, Quaternion.identity);

            Rigidbody2D rb = p.GetComponent<Rigidbody2D>();

            // hướng bay random
            Vector2 dir = new Vector2(Random.Range(-1f, 1f), Random.Range(0.5f, 1f));

            rb.AddForce(dir * force, ForceMode2D.Impulse);
            // rb.AddTorque(Random.Range(-200f, 200f));
        }

        Destroy(gameObject); // xoá box gốc
    }
}
