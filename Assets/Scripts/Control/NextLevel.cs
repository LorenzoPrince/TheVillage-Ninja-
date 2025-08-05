using UnityEngine;
using UnityEngine.SceneManagement;
public class NextLevel : MonoBehaviour
{
    //listap
    [SerializeField] private string[] sceneOrder;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        int totalEnemies = CountEnemies();
        Debug.Log("Jugador entró al tiger. Enemigos vivos: " + totalEnemies);

        if (totalEnemies == 0)
        {
            CargarSiguienteEscena();
        }
        else
        {
            Debug.Log("Aún quedan enemigos.");
        }
    }

    private int CountEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        return enemies.Length;
    }

    private void CargarSiguienteEscena()
    {
        string escenaActual = SceneManager.GetActiveScene().name;

        for (int i = 0; i < sceneOrder.Length - 1; i++)
        {
            if (sceneOrder[i] == escenaActual)
            {
                string siguienteEscena = sceneOrder[i + 1];
                Debug.Log("Cargando siguiente escena: " + siguienteEscena);
                SceneManager.LoadScene(siguienteEscena);
                return;
            }
        }


    }
}