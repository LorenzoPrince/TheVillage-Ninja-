using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class Contenedor<T> //Clase generica para manejar listas de cualquier tipo t
                            //la vamos a usar para coin dps
{
    private List<T> _elementos = new List<T>();   // Lista interna que almacena elementos de tipo T

    public void Agregar(T elemento) => _elementos.Add(elemento); //Agregar elementos 

    public void Remover(T elemento) => _elementos.Remove(elemento); //remover elementos

    public List<T> ObtenerTodos() => _elementos; //da todos los elementos de una lista

    public List<T> Filtrar(System.Func<T, bool> predicado)
    {
        // Previene errores por objetos destruidos
        return _elementos
            .Where(e => e != null && !e.Equals(null) && predicado(e))
            .ToList();
    }

    public List<T> OrdenarPor(System.Func<T, float> criterio)
    {
        // También evita errores al ordenar
        return _elementos
            .Where(e => e != null && !e.Equals(null))
            .OrderBy(criterio)
            .ToList();
    }

    //  Limpia todos los elementos destruidos
    public void LimpiarElementosDestruidos()
    {
        _elementos = _elementos
            .Where(e => e != null && !e.Equals(null))
            .ToList();
    }
}
