using UnityEngine;
using System.Linq;
public class CoinBounce : MonoBehaviour
{
    [SerializeField] private float rango = 5f; // Rango para detectar al jugar

    [SerializeField] private float alturaSalto = 0.2f; // alturix de rebote

    [SerializeField] private float velocidadSalto = 5f; // velocidad de rebote

    private Vector3 posicionBase;

    private bool jugadorCerca = false; // si esta cerca el player

    private static Contenedor<Player> jugadores = new Contenedor<Player>(); //contenedor estatio para guardar referencia a jugar

    private SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {

        posicionBase = transform.position; //posicion original

           //recoje los jugadores que hay esto evita tener repetidos. 
        if (jugadores.ObtenerTodos().Count == 0)
        {
            var jugadoresEncontrados = FindObjectsOfType<Player>();
            foreach (var jugador in jugadoresEncontrados)
            {
                jugadores.Agregar(jugador);
            }
        }

    }
    void Update()
    {
        // Uso Linq para filtrar los jugadores que están dentro del rango y ordenarlos por distancia
        var jugadorCercano = jugadores
            .Filtrar(j => Vector2.Distance(j.transform.position, transform.position) < rango) // lambda para filtrar jugadores en rango
            .OrderBy(j => Vector2.Distance(j.transform.position, transform.position)) // lambda para ordenar por distancia
            //orderby filtrar lambda que define bool
            .FirstOrDefault(); // agarro el jugador más cercano o null si no hay ninguno

        jugadorCerca = jugadorCercano != null;
        // Definimos si hay jugador cerca o no

        if (jugadorCerca)
        {
            // Si hay jugador cerca, hacemos que la moneda rebote con una función seno para suavidad
            float y = posicionBase.y + Mathf.Sin(Time.time * velocidadSalto) * alturaSalto;
            transform.position = new Vector3(posicionBase.x, y, posicionBase.z);
        }
        else
        {
            // Si no hay jugador cerca regresamos la moneda a su posición original
            transform.position = posicionBase;
        }
    }


}
