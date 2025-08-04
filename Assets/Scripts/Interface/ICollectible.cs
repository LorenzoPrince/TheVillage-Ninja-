using UnityEngine;

public interface ICollectible // Buena practica I antes del nombre del interface
{
    void Collect(Player jugador); // obliga que la clase que tenga este interface deba tener el metodo collect y que reciba el jugador 
}
 