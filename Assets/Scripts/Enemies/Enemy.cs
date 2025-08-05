using UnityEngine;
using System.Collections.Generic;
public class Enemy : CombatEntity //hereda de la clase padre.
{ 
    private enum MaquinaEstadoEnemigos //Creamos para controlar los estados del enemigo.
    {
        Idle,
        Perseguir,
        Atacar,
        Morir
    }

    [SerializeField] private float _rangoDeteccion = 5f; 
    [SerializeField] private float _rangoAtaque = 3f;
    [SerializeField] private float _velocidad = 2f;
    [SerializeField] private float tiempoEntreAtaques = 1.5f;
    private float _proximoAtaque = 0f;

    private int daño = 10;
    private MaquinaEstadoEnemigos _estadoActual = MaquinaEstadoEnemigos.Idle;

    private Transform _jugador;

    void Start()
    {
        _jugador = GameObject.FindWithTag("Player")?.transform; //obtiene el transform del player. El operador ? verifica que no sea null antes de acceder al transform.
        _estadoActual = MaquinaEstadoEnemigos.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Vivo) //esto es heredado del padre
        {
            _estadoActual = MaquinaEstadoEnemigos.Morir;
        }

        switch (_estadoActual) //remplaza al if y else para que tenga mejor legibilidad
        {
            case MaquinaEstadoEnemigos.Idle:
                Idle();
                break;
            case MaquinaEstadoEnemigos.Perseguir:
                Perseguir();
                break;
            case MaquinaEstadoEnemigos.Atacar:
                Atacar();
                break;
            case MaquinaEstadoEnemigos.Morir:
                Morir();
                break;
        }
    }
    private void Idle()
    {
        if (_jugador == null) return;

        if (Vector2.Distance(transform.position, _jugador.position) < _rangoDeteccion)
        {
            _estadoActual = MaquinaEstadoEnemigos.Perseguir;
        }
    }

    private void Perseguir()
    {
        Debug.Log("Peseguiendo al jugador");
        Vector2 direccion = _jugador.position - transform.position;
        Mover(direccion, _velocidad); //uso el metodo heredado

        float distancia = Vector2.Distance(transform.position, _jugador.position);

        if (distancia < _rangoAtaque)
            _estadoActual = MaquinaEstadoEnemigos.Atacar;
        else if (distancia > _rangoDeteccion)
            _estadoActual = MaquinaEstadoEnemigos.Idle;
    }

    private void Atacar()
    {
        if (_jugador != null && Time.time >= _proximoAtaque) //Arreglo bug de ataque rapido por frame
        {
            Player jugadorScript = _jugador.GetComponent<Player>();
            if (jugadorScript != null)
            {
                jugadorScript.RecibirDaño(daño);
                Debug.Log($"Daño infligido al jugador: {daño}");

                _proximoAtaque = Time.time + tiempoEntreAtaques; // espera al próximo ataque
            }
        }

        Invoke(nameof(FinalizarAtaque), 0.5f);
    }

    private void Morir()
    {
        Debug.Log("El enemigo murió.");
        base.Morir(); // usa el método de la clase base CombatEntity
    }

    private void FinalizarAtaque()
    {
        if (Vivo)
            _estadoActual = MaquinaEstadoEnemigos.Idle;
    }
}

