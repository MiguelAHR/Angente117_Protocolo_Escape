using UnityEngine;

public class LlaveRecolectable : MonoBehaviour
{
    [Header("Sonido")]
    [SerializeField] private AudioClip sonidoRecoger;

    [Range(0f, 1f)]
    [SerializeField] private float volumen = 1f;

    private bool recogida;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Evita recoger la misma llave dos veces.
        if (recogida)
        {
            return;
        }

        // Busca el inventario en el objeto que entro o en su objeto padre.
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

        // Reproduce el sonido aunque la llave sea destruida.
        if (sonidoRecoger != null)
        {
            Vector3 posicionSonido =
                Camera.main != null
                ? Camera.main.transform.position
                : transform.position;

            AudioSource.PlayClipAtPoint(
                sonidoRecoger,
                posicionSonido,
                volumen
            );
        }

        // Elimina la llave del escenario.
        Destroy(gameObject);
    }
}
