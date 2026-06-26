using UnityEngine;

public class Moneda : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip sonidoGetKey;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playGetKey();
            Destroy(gameObject);
        }
    }

    public void playGetKey()
    {
        audioSource.PlayOneShot(sonidoGetKey);
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
