using UnityEngine;
using System.Collections.Generic; //para las listas
public class Player : MonoBehaviour, IDamageable // implemento interface para asegurar que tenga el metodo recibir daño
{
    private int _vida = 100; // el guion bajo se usa para decir que la variable es interna a la clase, y no se debe cambiar desde fuera.
    private float _velocidad = 5f;
    private int _totalMonedas = 0;
    private Vector2 _posicionActual;
    private bool _vivo = true;
    private Rigidbody2D _rb;
    public bool EsInvulnerable { get; set; } = false;
    public int Vida
    {
        get => _vida;  // aplicamos getters y setters para proteger la integridad de las clases y que se modifiquen por dentro de la clase. 
        private set
        {
            _vida = Mathf.Clamp(value, 0, 100); // evitamos valores invalidos. 
            GameEvents.OnVidaCambiada?.Invoke(_vida);
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
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 direccion = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Mover(direccion);

        if (Input.GetKeyDown(KeyCode.Q))
        {
           
            if (HabilidadesDesbloqueadas.Exists(h => h.Nombre == "Curacion")) // si tengo la habilidad desbloqueada la usa 
            {
                UsarHabilidad("Curacion");
            }
            else
            {
                Debug.Log("No tenés la habilidad de curación desbloqueada.");
            }
        }
    }

    public void Mover(Vector2 direccion)
    {
        if (!_vivo) return;
        _rb.linearVelocity = direccion.normalized * _velocidad;
        PosicionActual = _rb.position; 

        if (direccion != Vector2.zero)
        {
            float angle = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
            float offset = 90f;
            transform.rotation = Quaternion.Euler(0, 0, angle + offset);
        }
    }
    public void RecibirDaño(int cantidad) //cumplo con la interface
    {
        if (!_vivo || EsInvulnerable)
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
        GameEvents.OnMonedaRecogida?.Invoke(TotalMonedas); // mando el evento al game event.
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
    public void DesbloquearHabilidad(Ability habilidad) //aca desbloquea la habilidad.
    {
        if (!HabilidadesDesbloqueadas.Contains(habilidad))
        {
            HabilidadesDesbloqueadas.Add(habilidad);
            GameEvents.OnHabilidadDesbloqueada?.Invoke(habilidad);
            Debug.Log($"Habilidad desbloqueada: {habilidad.Nombre}");
        }
        else
        {
            Debug.Log($"Ya tenés desbloqueada la habilidad: {habilidad.Nombre}");
        }
    }

    public void UsarHabilidad(string nombre)
    {
        Ability habilidad = HabilidadesDesbloqueadas.Find(h => h.Nombre == nombre);
        if (habilidad != null)
        {
            habilidad.Activar(this);
        }
        else
        {
            Debug.Log("No tenés esa habilidad desbloqueada.");
        }
    }

    public void Curar(int cantidad)
    {
        Vida += cantidad;
        Debug.Log($"Jugador curado por {cantidad}. Vida actual: {_vida}");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Item item = other.GetComponent<Item>();
        if (item != null)
        {
            item.Collect(this); // esto hace que el objeto se use automáticamente
        }
    }
}
