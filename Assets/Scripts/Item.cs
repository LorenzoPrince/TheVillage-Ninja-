using UnityEngine;

public class Item : MonoBehaviour
{
    private string _nombre;
    private string _tipo;
    private int _valor;

    public string Nombre
    {
        get { return _nombre; } // dos formas de aplicar get y getter esta forma un poco mas larga
        set { _nombre = value; }
    }

    public string Tipo
    {
        get => _tipo;  //esta forma mas corta.
        set => _tipo = value;
    }

    public int Valor
    {
        get => _valor; // en vez de corchete agrego la flecha. 
        set
        {
            if (value >= 0)
                _valor = value;
        }
    }


    public virtual void Usar(Player jugador)
    {

    }

    public virtual void Collect(Player jugador)
    {
        jugador.Inventario.Add(this); // Agrega al inventario real del jugador
        Debug.Log($"{Nombre} recolectado por {jugador.name}");


        gameObject.SetActive(false);
    }
}
