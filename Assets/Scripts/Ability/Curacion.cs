using UnityEngine;

[CreateAssetMenu(fileName = "NuevaCuracion", menuName = "Habilidad/Curación")]
public class Curacion : Ability
{
    [SerializeField] private int cantidadCuracion = 20;

    public override void Activar(Player jugador)
    {
        jugador.Curar(cantidadCuracion);
        Debug.Log($"Se activó {Nombre}: Curó {cantidadCuracion} de vida.");
    }
}
