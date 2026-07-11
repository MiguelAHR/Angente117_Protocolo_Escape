using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverText;
    public Button reiniciarButton;
    public Button menuButton;

    [Header("Checkpoint")]
    public Vector3 checkpointPosicion;
    private Checkpoint checkpointActual;
    
    private bool gameOverActivo = false;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if(gameOverPanel !=null)
           gameOverPanel.SetActive(false);

        if(reiniciarButton != null)
           reiniciarButton.onClick.AddListener(ReiniciarEscena);
        
        if(menuButton !=null)
           menuButton.onClick.AddListener(IrAlMenu);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            checkpointPosicion = player.transform.position;
        }

    }

    void Update()
    {
        if(gameOverActivo)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                ReiniciarEscena();
            }

            if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.M))
            {
                IrAlMenu();
            }
        }
    }

    public void GameOver()
    {
        if (gameOverActivo) return;

        gameOverActivo = true;

        if(gameOverPanel !=null)
        {
            gameOverPanel.SetActive(true);
        }

        if(gameOverText !=null)
        {
            gameOverText.text = "GAME OVER\n\nR- Reiniciar\nESC - Menu Principal";
        }
    }

    public void ReiniciarEscena()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IrAlMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void ActualizarCheckpoint(Vector3 nuevaPosicion, Checkpoint nuevoCheckpoint)
    {
        checkpointPosicion = nuevaPosicion;
        checkpointActual = nuevoCheckpoint;
    }

    public void RespawnJugador()
    {
        StartCoroutine(RespawnCoroutine());
    }

    IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(1f);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerController pc = player.GetComponentInChildren<PlayerController>();
            if (pc != null)
            {
                pc.Respawn(checkpointPosicion);
            }
            else
            {
                Debug.LogWarning("No se encontró PlayerController en el objeto con tag Player");
            }
        }
    }
}
