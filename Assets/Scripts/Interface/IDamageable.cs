using UnityEngine;

public interface IDamageable // Buena Practica Agregar I adelante de el nombre de la interface. 
{
    void RecibirDa�o(int cantidad); // Todas las clases que tenga esta interface tendran un contrato el cual las oblga a implementar el metodo de recibir da�o
}
