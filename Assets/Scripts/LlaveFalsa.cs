using UnityEngine;

public class LlaveFalsa : MonoBehaviour
{
    [Header("Sonido")]
    [SerializeField] private AudioClip sonidoFalso;
    [Range(0f, 1f)]
    [SerializeField] private float volumen = 1f;

    private bool recogida;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Evita activarla dos veces.
        if (recogida)
        {
            return;
        }

        // Verifica que sea el jugador (usa el mismo componente como referencia,
        // aunque no se use el inventario, para saber que fue el Player quien entró).
        InventarioLlave inventario =
            other.GetComponentInParent<InventarioLlave>();

        // Si no es el jugador, no hace nada.
        if (inventario == null)
        {
            return;
        }

        recogida = true;

        // NO se llama a inventario.RecogerLlave() ni se muestra nada en el Canvas.

        // Reproduce el sonido falso aunque el objeto sea destruido.
        if (sonidoFalso != null)
        {
            Vector3 posicionSonido =
                Camera.main != null
                ? Camera.main.transform.position
                : transform.position;
            AudioSource.PlayClipAtPoint(
                sonidoFalso,
                posicionSonido,
                volumen
            );
        }

        // Elimina la llave falsa del escenario.
        Destroy(gameObject);
    }
}