using UnityEngine;

public class Coin : Item
{
    public override void Usar(Player jugador)
    {
        jugador.AgarrarMoneda(); // Llama a tu método ya hecho
        Debug.Log("Moneda usada. +1 moneda.");
        Destroy(gameObject);
    }

    public override void Collect(Player jugador)
    {
        base.Collect(jugador);
        Usar(jugador); // Se usa al ser recolectada
    }
}