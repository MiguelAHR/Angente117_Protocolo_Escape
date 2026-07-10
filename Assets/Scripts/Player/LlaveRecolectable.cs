using UnityEngine;

public class LlaveRecolectable : MonoBehaviour
{
    private bool recogida;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Evita recoger la misma llave dos veces.
        if (recogida)
        {
            return;
        }

        // Busca el inventario en el objeto que entró o en su objeto padre.
        InventarioLlave inventario =
            other.GetComponentInParent<InventarioLlave>();

        // Si no es el jugador, no hace nada.
        if (inventario == null)
        {
            return;
        }

        recogida = true;

        // Guarda la llave y muestra el icono del Canvas.
        inventario.RecogerLlave();

        // Elimina la llave del escenario.
        Destroy(gameObject);
    }
}
