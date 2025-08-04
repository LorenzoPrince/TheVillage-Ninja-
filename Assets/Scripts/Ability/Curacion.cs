using UnityEngine;

[CreateAssetMenu(fileName = "NuevaCuracion", menuName = "Habilidad/Curaci�n")]
public class Curacion : Ability
{
    [SerializeField] private int cantidadCuracion = 20;

    public override void Activar(Player jugador)
    {
        jugador.Curar(cantidadCuracion);
        Debug.Log($"Se activ� {Nombre}: Cur� {cantidadCuracion} de vida.");
    }
}
