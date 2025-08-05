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

    public List<T> Filtrar(System.Func<T, bool> predicado) => _elementos.Where(predicado).ToList(); //filtra elemento segun algo y devuelve bool

    public List<T> OrdenarPor(System.Func<T, float> criterio) => _elementos.OrderBy(criterio).ToList(); //ordena segun criteio devuelve float
}
