using UnityEngine;
using System.Collections.Generic; //para las listas
public class Player : MonoBehaviour, IDamageable // implemento interface para asegurar que tenga el metodo recibir da�o
{
    private int _vida = 100; // el guion bajo se usa para decir que la variable es interna a la clase, y no se debe cambiar desde fuera.
    private float _velocidad = 5f;
    private int _totalMonedas = 0;
    private Vector2 _posicionActual;
    private bool _vivo = true;
    public int Vida
    {
        get => _vida;  // aplicamos getters y setters para proteger la integridad de las clases y que se modifiquen por dentro de la clase. 
        private set
        {
            _vida = Mathf.Clamp(value, 0, 100); // evitamos valores invalidos. 
            if (_vida == 0)
                Morir();
        }

    }
    public float Velocidad
    {
        get => _velocidad;
        set
        {
            if (value >= 0)
                _velocidad = value;
        }
    }
    public int TotalMonedas
    {
        get => _totalMonedas; // ver que tengo
        private set // con set puedo cambiar la variable solo dentro de esta clase.
        {
            if (value >= 0)
                _totalMonedas = value;
        }
    }
    public Vector2 PosicionActual
    {
        get => _posicionActual;
        private set => _posicionActual = value;
    }

    public List<Item> Inventario { get; private set; } = new List<Item>();
    public List<Ability> HabilidadesDesbloqueadas { get; private set; } = new List<Ability>();

    public bool Vivo // propiedad para solo lectura
    {
        get { return _vivo; }
    }

    private void Update()
    {
        Vector2 direccion = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Mover(direccion);
    }

    public void Mover(Vector2 direccion)
    {
        if (!_vivo) return;
        transform.Translate(direccion * _velocidad * Time.deltaTime);
        PosicionActual = new Vector2(transform.position.x, transform.position.y);
    }
    public void RecibirDa�o(int cantidad) //cumplo con la interface
    {
        if (!_vivo) 
            return;
        Vida -= cantidad;
    }
    public void Morir()
    {
        _vivo = false;
        // recordar implmentar que se reinicie escena o algo
    }
    public void Atacar()
    {

    }
    public void AgarrarMoneda()
    {
        TotalMonedas++;
       //sumo moneda. Dps ver si una moneda vale mas o algo. 
    }
    public void AgarrarItem(Item item)
    {
        if (!Inventario.Contains(item))
        {
            Inventario.Add(item);
            item.Usar(this); 
        }
    }
    public void UsarItem(Item item)
    {
        if (Inventario.Contains(item))
        {
            item.Usar(this);
            Inventario.Remove(item);
        }
    }
}
