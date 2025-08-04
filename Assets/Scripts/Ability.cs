using UnityEngine;

public abstract class Ability : ScriptableObject //lo utilizo para crear datos
{
    [SerializeField] private string nombre;
    [SerializeField] private string descripcion; //por si hago explicacion cuando se agarre. A futuro
    [SerializeField] private float duracion;
    [SerializeField] private int nivelRequerido;

    public string Nombre => nombre; //para verlo.
    public string Descripcion => descripcion;
    public float Duracion => duracion;
    public int NivelRequerido => nivelRequerido;

    public abstract void Activar(Player jugador);
}
