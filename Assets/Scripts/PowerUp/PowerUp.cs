using UnityEngine;

public abstract class PowerUp : MonoBehaviour, ICollectible
{
    public abstract void Collect(Player jugador); //obliga a todas las clases hijas a implementarlo. Ya que tiene comportamiento unico cada powerUp
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player jugador = collision.GetComponent<Player>();
        if (jugador != null)
        {
            Collect(jugador);
            Destroy(gameObject); // Se elimina al recoger
        }
    }
}
