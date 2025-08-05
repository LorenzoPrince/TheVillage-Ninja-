using UnityEngine;
using System.Collections;
public class Boss : CombatEntity //hereda de la clase abstract
{
    private enum EstadoBoss //define estados del boss
    {
        Idle,
        Fase1, // Movimiento entre puntos + disparos random
        Fase2, // Carrera hacia el jugador
        Fase3, // Carrera + disparos directos
        Fase4, // a full a matar al jugador
        Muerto
    }

    private int dañoDeCarga = 10; //daño que inflinge al chocarse con el boss

    [Header("Referencias")]
    [SerializeField] private Transform puntoA; //para que se mueva en fase 1
    [SerializeField] private Transform puntoB;
    [SerializeField] private GameObject proyectilPrefab;

    [Header("Stats")] //Header es para que en el inspector muestre ese encambezado y no haya confusiones.
    [SerializeField] private float velocidadMovimiento = 2f;// Velocidad de movimiento en fase 1
    [SerializeField] private float tiempoEntreDisparos = 2f; //en fase 1 yt 3
    [SerializeField] private float tiempoEsperaFase2 = 1.5f;  // Tiempo que dura la carga en fase 2
    [SerializeField] private float velocidadCarga = 4f; //que tan rapido carga contra el jugador
    [SerializeField] private float velocidadFase4 = 8f; //velocidad ultima fase
    [SerializeField] private float cadenciaFase4 = 0.7f; //tiempo de disparos constantes.
    private bool yaLanzandoCarga = false;
    public int faseActual = 1;

    private EstadoBoss estadoActual = EstadoBoss.Fase1; //inicia en fase 1
    private Transform jugador;
    private Vector3 destinoActual;
    private float tiempoProximoDisparo;

    protected override void Awake()
    {
        base.Awake();
        jugador = GameObject.FindGameObjectWithTag("Player")?.transform;
        destinoActual = puntoB.position;
    }

    void Update()
    {
        if (!Vivo)
        {
            estadoActual = EstadoBoss.Muerto; 
            return;
        }

        VerificarTransicionesDeFase();

        switch (estadoActual)
        {
            case EstadoBoss.Fase1:
                Fase1();
                break;
            case EstadoBoss.Fase2:
                if (!yaLanzandoCarga)
                    StartCoroutine(Fase2());
                break;
            case EstadoBoss.Fase3:
                Fase3();
                break;
            case EstadoBoss.Fase4:
                Fase4();
                break;
            case EstadoBoss.Muerto:
                Morir();
                break;
        }
    }

    // FASE 1 
    private void Fase1()
    {
        //mueve al boss al destino actual
        Mover(destinoActual - transform.position, velocidadMovimiento);
        //va y viene 
        if (Vector2.Distance(transform.position, destinoActual) < 0.1f)
        {
            destinoActual = destinoActual == puntoA.position ? puntoB.position : puntoA.position;
        }
        //si paso el tiempo para disparar y tambien dispara de forma random
        if (Time.time >= tiempoProximoDisparo)
        {
            DispararARandom(); //llamo al metodo
            tiempoProximoDisparo = Time.time + tiempoEntreDisparos;
        }
    }

    private void DispararARandom()
    {
        Vector3 direccionRandom = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        GameObject proyectil = Instantiate(proyectilPrefab, transform.position, Quaternion.identity);
        proyectil.GetComponent<Rigidbody2D>().linearVelocity = direccionRandom * 5f;
    }

    //  FASE 2 
    private IEnumerator Fase2()
    {
        yaLanzandoCarga = true;

        float tiempoCargado = 0f;
        Vector2 direccion = (jugador.position - transform.position).normalized; //direccion hacia el jugador


        while (tiempoCargado < tiempoEsperaFase2 && Vivo) //mientras este vivo y el boos te live
        {
            Mover(direccion, velocidadCarga); //mueve con la velocidad de la carga
            tiempoCargado += Time.deltaTime;
            yield return null; // espera un frame y sigue
        }

        // repite o ve
        if (Vivo)
        {
            estadoActual = EstadoBoss.Fase2; // para repetir la carga
        }
        yaLanzandoCarga = false;
    }
    // FASE 3
    private void Fase3()
    {
        if (jugador == null) return;

        Vector2 direccion = jugador.position - transform.position;
        Mover(direccion, velocidadCarga);

        if (Time.time >= tiempoProximoDisparo)
        {
            DispararDirectoAlJugador();
            tiempoProximoDisparo = Time.time + tiempoEntreDisparos;
        }
    }

    private void DispararDirectoAlJugador()
    {
        if (jugador == null) return;

        Vector2 direccion = (jugador.position - transform.position).normalized;
        GameObject proyectil = Instantiate(proyectilPrefab, transform.position, Quaternion.identity);
        proyectil.GetComponent<Rigidbody2D>().linearVelocity = direccion * 6f;
    }

    //  FASE 4 
    private void Fase4()
    {
        if (jugador == null) return;

        Vector2 direccion = jugador.position - transform.position;
        Mover(direccion, velocidadFase4);// Mueve mas rapido hacia el jugador

        if (Time.time >= tiempoProximoDisparo)  // Disparos  con cadencia más veloz
        {
            DispararDirectoAlJugador();
            tiempoProximoDisparo = Time.time + cadenciaFase4;
        }
    }

    //  Transiciones de fase por porcentaje de vida
    private void VerificarTransicionesDeFase()
    {
        float porcentaje = (float)Vida / 100f; //vida normalizada

        if (porcentaje <= 0.25f && faseActual < 4) //verifica si seguir o no en la fase
        {
            Debug.Log("? Fase 4 activada");
            estadoActual = EstadoBoss.Fase4;
            faseActual = 4;
        }
        else if (porcentaje <= 0.5f && estadoActual != EstadoBoss.Fase3 && estadoActual != EstadoBoss.Fase4)
        {
            Debug.Log("? Fase 3 activada");
            estadoActual = EstadoBoss.Fase3;
            faseActual = 3;

        }
        else if (porcentaje <= 0.75f && estadoActual != EstadoBoss.Fase2 && estadoActual != EstadoBoss.Fase3 && estadoActual != EstadoBoss.Fase4)
        {
            Debug.Log("? Fase 2 activada");
            estadoActual = EstadoBoss.Fase2;
            faseActual = 2;
        }
    }

    public override void Morir()
    {
        Debug.Log("?? Boss derrotado.");
        base.Morir();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player jugador = collision.gameObject.GetComponent<Player>();
        if (jugador != null)
        {
            jugador.RecibirDaño(dañoDeCarga); // Le inflige daño accediendo al metodo
            Debug.Log("Boss infligió daño por contacto en la carga.");
        }
    }
}
