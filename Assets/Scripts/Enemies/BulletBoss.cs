using UnityEngine;

public class BulletBoss : MonoBehaviour
{
    [SerializeField] private int daño = 5;
    [SerializeField] private float tiempoVida = 5f;
    private void Start()
    {
        // Se destruye solo después de un tiempo, aunque no choque con nada
        Destroy(gameObject, tiempoVida);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        Player jugador = collision.gameObject.GetComponent<Player>(); // Verifica que lo que colisiona es el player

        if (jugador != null)
        {
            jugador.RecibirDaño(daño);
            Debug.Log("El proyectil del boss daño al jugador.");
            Destroy(gameObject); 
        }

    }

}
