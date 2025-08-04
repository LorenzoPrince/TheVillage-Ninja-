using UnityEngine;

public class HealingItem : Item
{ 
    public override void Usar(Player jugador)
    {
        jugador.Curar(Valor); // Usa el valor como cantidad de curación
        Debug.Log($"Curado con {Valor} puntos de vida.");
        Destroy(gameObject);
    }

    public override void Collect(Player jugador)
    {
        base.Collect(jugador);
        Usar(jugador); // Se usa al recogerlo
    }
}
