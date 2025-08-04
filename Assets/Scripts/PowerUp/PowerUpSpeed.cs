using UnityEngine;
using System.Collections;
public class PowerUpSpeed : PowerUp
{
    [SerializeField] private float multiplicador = 2f;
    [SerializeField] private float duracion = 5f;

    public override void Collect(Player jugador)
    {
        jugador.StartCoroutine(AumentarVelocidadTemporal(jugador));
    }

    private IEnumerator AumentarVelocidadTemporal(Player jugador)
    {
        float velocidadOriginal = jugador.Velocidad;
        jugador.Velocidad *= multiplicador;
        Debug.Log("Velocidad aumentada");

        yield return new WaitForSeconds(duracion);

        jugador.Velocidad = velocidadOriginal;
        Debug.Log("Velocidad restaurada");
    }
}
