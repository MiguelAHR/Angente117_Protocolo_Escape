using UnityEngine;

public class EnemySoundController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip sonidoRecibirDanio;
    public AudioClip sonidoMuerte;

    public void playRecibirDanio()
    {
        audioSource.PlayOneShot(sonidoRecibirDanio);
    } 

    public void playMuerte()
    {
        audioSource.PlayOneShot(sonidoMuerte);
    }

}
