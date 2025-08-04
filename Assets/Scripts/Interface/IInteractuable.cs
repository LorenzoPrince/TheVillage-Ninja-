using UnityEngine;

public interface IInteractuable // Buena practica I antes del nombre del interface
{
    void Interactuar(Player player); // obliga que la clase que tenga este interface deba tener el metodo interactuar y que reciba el jugador 
}
