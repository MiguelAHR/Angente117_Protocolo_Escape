using UnityEngine;

public class TrampaPinchos : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Replicamos exactamente tu lógica de dirección del "EnemyController"
            Vector2 direccionDanio = new Vector2(transform.position.x, 0);

            // Llamamos a tu PlayerController usando tus mismos parámetros
            collision.GetComponent<PlayerController>().RecibeDanio(direccionDanio, 1);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
