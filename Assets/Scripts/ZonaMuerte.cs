using UnityEngine;

public class ZonaMuerte : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("El player murio por noob");
            collision.GetComponent<PlayerController>().RecibeDanio(Vector2.zero, 99);
        }
    }
}
