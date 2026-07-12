using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool checkpointActivo = false;
    public Sprite spriteActivo;
    private SpriteRenderer spriteRenderer;

    private Animator animator;

    [Header("Audio")]
    public AudioSource audioSourceCerca;
    public AudioClip sonidoCerca;
    [Range(0f, 1f)]
    public float volumenCerca = 1f;

    public AudioSource audioSourceActivacion;
    public AudioClip sonidoActivacion;
    [Range(0f, 1f)]
    public float volumenActivacion = 1f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        if (audioSourceCerca != null)
        {
            audioSourceCerca.clip = sonidoCerca;
            audioSourceCerca.loop = true;
            audioSourceCerca.playOnAwake = false;
            audioSourceCerca.volume = volumenCerca;
        }

        if (audioSourceActivacion != null)
        {
            audioSourceActivacion.playOnAwake = false;
            audioSourceActivacion.volume = volumenActivacion;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (audioSourceCerca != null && sonidoCerca != null && !audioSourceCerca.isPlaying)
            {
                audioSourceCerca.volume = volumenCerca;
                audioSourceCerca.Play();
            }

            if (!checkpointActivo)
            {
                ActivarCheckpoint();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (audioSourceCerca != null && audioSourceCerca.isPlaying)
            {
                audioSourceCerca.Stop();
            }
        }
    }

    void ActivarCheckpoint()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ActualizarCheckpoint(transform.position, this);
        }
        checkpointActivo = true;

        if (spriteRenderer != null && spriteActivo != null)
        {
            spriteRenderer.sprite = spriteActivo;
        }

        if (animator != null)
        {
            animator.SetBool("activado", true);
        }

        if (audioSourceActivacion != null && sonidoActivacion != null)
        {
            audioSourceActivacion.volume = volumenActivacion;
            audioSourceActivacion.PlayOneShot(sonidoActivacion, volumenActivacion);
        }
    }
}