using UnityEngine;
using UnityEngine.SceneManagement;  // Asegúrate de incluir este namespace si planeas cambiar de escena

public class TanqueCollisiones : MonoBehaviour
{
    private int hitCount = 0;
    public int maxHits = 3;
    public GameObject gameOverPanel;  // Referencia al panel que contiene el mensaje de Game Over
    public GameObject winPanel;  // Referencia al panel que contiene el mensaje de victoria


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            hitCount++;
            Destroy(collision.gameObject); // Destruye el enemigo
            if (hitCount >= maxHits)
            {
                GameOver();
            }
        }
        else if (collision.gameObject.CompareTag("Delimitacion"))
        {
            GameOver();
        }
        else if (collision.gameObject.CompareTag("Diamond"))
        {
            WinGame(collision.gameObject); // Maneja la victoria
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over! El juego ha terminado.");
        gameOverPanel.SetActive(true);  // Activa el panel de Game Over
        Time.timeScale = 0;  // Detiene el tiempo de juego, pausando cualquier otra acción en el juego
        DestroyTank();
    }

    private void DestroyTank()
    {
        Debug.Log("El tanque ha sido destruido.");
        Destroy(gameObject);
    }

    private void WinGame(GameObject diamond)
    {
        Debug.Log("You win the game!");
        Destroy(diamond);  // Destruye el diamante
        Destroy(gameObject);  // Destruye el tanque también

        winPanel.SetActive(true);  // Activa el panel de victoria

        // Opcional: Puedes detener el tiempo de juego si lo deseas
        Time.timeScale = 0;
    }
}
