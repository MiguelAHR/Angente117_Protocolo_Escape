using UnityEngine;

public class RayoLaser : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector2 direccionDanio = Vector2.zero;

            // Usamos "lossyScale" para leer la escala REAL del objeto en el mundo,
            // solucionando el problema de si el objeto es hijo de otro.
            if (transform.lossyScale.x > 0)
            {
                // Escala 1 -> Manda a la IZQUIERDA.
                // Sumamos +50f para crear un punto de daño lejano a la derecha y garantizar la dirección.
                direccionDanio = new Vector2(collision.transform.position.x + 50f, 0);
            }
            else if (transform.lossyScale.x < 0)
            {
                // Escala -1 -> Manda a la DERECHA.
                // Restamos -50f para crear el punto de daño lejano a la izquierda.
                direccionDanio = new Vector2(collision.transform.position.x - 50f, 0);
            }

            // Enviamos la orden final a tu PlayerController
            collision.GetComponent<PlayerController>().RecibeDanio(direccionDanio, 1);
        }
    }
}
