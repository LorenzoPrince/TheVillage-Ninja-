using UnityEngine;
using System.Collections;
public class PowerUpImmunity : PowerUp
{
    [SerializeField] private float duracion = 5f;

    public override void Collect(Player jugador)
    {
        jugador.StartCoroutine(ActivarInmunidad(jugador));
    }

    private IEnumerator ActivarInmunidad(Player jugador) //Agregue corrutinas.
    {
        jugador.EsInvulnerable = true;
        Debug.Log("Jugador invulnerable");

        yield return new WaitForSeconds(duracion);

        jugador.EsInvulnerable = false;
        Debug.Log("Jugador vulnerable nuevamente");
    }
}