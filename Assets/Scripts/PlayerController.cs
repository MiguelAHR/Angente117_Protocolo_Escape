using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocidad = 5f;

    public float fuerzaSalto = 10f;
    public float fuerzaRebote = 10f;
    public float longitudRaycast = 0.1f;
    public LayerMask capaSuelo;

    private bool enSuelo;
    private bool recibiendoDanio;
    private Rigidbody2D rb;

    public Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float velocidadX = Input.GetAxis("Horizontal") * Time.deltaTime * velocidad;

        animator.SetFloat("movement", velocidadX * velocidad);

        if (velocidadX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (velocidadX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        Vector3 posicion = transform.position;

        if(!recibiendoDanio)
            transform.position = new Vector3(velocidadX + posicion.x, posicion.y, posicion.z);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, longitudRaycast, capaSuelo);
        enSuelo = hit.collider != null;

        if (enSuelo && Input.GetKeyDown(KeyCode.Space) && !recibiendoDanio)
        {
            rb.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse);
        }

        animator.SetBool("ensuelo", enSuelo);
        animator.SetBool("recibeDanio", recibiendoDanio);
    }

    public void RecibeDanio(Vector2 direccion, int cantDanio)
    {
        if(!recibiendoDanio)
        {
            recibiendoDanio = true;
            Vector2 rebote = new Vector2(transform.position.x - direccion.x, 0.5f).normalized;
            rb.AddForce(rebote*fuerzaRebote, ForceMode2D.Impulse);
        }
    }

    public void DesactivaDanio()
    {
        recibiendoDanio = false;
        // VER ESTA PARTE SE CAMBIA SOLO Video 10 min 6:25
        rb.linearVelocity = Vector2.zero; 
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * longitudRaycast);
    }
}
