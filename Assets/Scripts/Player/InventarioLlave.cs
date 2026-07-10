using UnityEngine;

public class InventarioLlave : MonoBehaviour
{
    [Header("Interfaz")]
    [SerializeField] private GameObject iconoLlave;

    public bool TieneLlave { get; private set; }

    private void Awake()
    {
        TieneLlave = false;

        if (iconoLlave != null)
        {
            iconoLlave.SetActive(false);
        }
    }

    public void RecogerLlave()
    {
        TieneLlave = true;

        if (iconoLlave != null)
        {
            iconoLlave.SetActive(true);
        }
    }

    public bool UsarLlave()
    {
        if (!TieneLlave)
        {
            return false;
        }

        TieneLlave = false;

        if (iconoLlave != null)
        {
            iconoLlave.SetActive(false);
        }

        return true;
    }
}