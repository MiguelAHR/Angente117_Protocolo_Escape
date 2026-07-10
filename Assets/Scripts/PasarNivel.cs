using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PasarNivel : MonoBehaviour
{
    [Header("Configuración de la puerta")]
    [SerializeField] private Animator animadorPuerta;

    // Evita que la puerta se active varias veces.
    private bool abriendoPuerta;

    private void Awake()
    {
        // Si no se asignó manualmente, busca el Animator
        // en el mismo objeto de la puerta.
        if (animadorPuerta == null)
        {
            animadorPuerta = GetComponent<Animator>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // No continuar si ya se está abriendo.
        if (abriendoPuerta)
        {
            return;
        }

        // Solo reaccionar al jugador.
        if (!collision.CompareTag("Player"))
        {
            return;
        }

        // Buscar el inventario del jugador.
        InventarioLlave inventario =
            collision.GetComponentInParent<InventarioLlave>();

        // Si no existe inventario o no tiene llave,
        // la puerta permanece cerrada.
        if (inventario == null || !inventario.UsarLlave())
        {
            Debug.Log("La puerta está cerrada. Necesitas la llave.");
            return;
        }

        abriendoPuerta = true;

        // Comenzar animación y cambio de nivel.
        StartCoroutine(AbrirPuertaYCambiarNivel());
    }

    private IEnumerator AbrirPuertaYCambiarNivel()
    {
        // 1. Encender la puerta: cambia a color celeste.
        animadorPuerta.Play(
            "Base Layer.exit_on_closed",
            0,
            0f
        );

        // Esperar un frame para que Unity cambie de estado.
        yield return null;

        // Esperar a que termine una reproducción de la animación.
        float duracionEncendido =
            animadorPuerta.GetCurrentAnimatorStateInfo(0).length;

        yield return new WaitForSeconds(duracionEncendido);

        // 2. Reproducir la apertura de la puerta.
        animadorPuerta.Play(
            "Base Layer.exit_on_open",
            0,
            0f
        );

        yield return null;

        float duracionApertura =
            animadorPuerta.GetCurrentAnimatorStateInfo(0).length;

        yield return new WaitForSeconds(duracionApertura);

        // 3. Cargar el siguiente nivel.
        int siguienteNivel =
            SceneManager.GetActiveScene().buildIndex + 1;

        if (siguienteNivel < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(siguienteNivel);
        }
        else
        {
            Debug.LogError(
                "No existe un siguiente nivel en la lista de escenas."
            );
        }
    }
}