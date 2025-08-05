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
    [SerializeField] private float _rangoAtaque = 2f;
    [SerializeField] private float _velocidad = 2f;

    private int da�o = 5;
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
        {
            Debug.Log("Atacando al jugador...");

            if (_jugador != null)
            {
                Player jugadorScript = _jugador.GetComponent<Player>();
                if (jugadorScript != null)
                {
                    // el da�o que se aplicara
                    jugadorScript.RecibirDa�o(da�o);
                    Debug.Log($"Da�o infligido al jugador: {da�o}");
                }
            }

            Invoke(nameof(FinalizarAtaque), 1.5f);
        }
    }

    private void Morir()
    {
        Debug.Log("El enemigo muri�.");
        base.Morir(); // usa el m�todo de la clase base CombatEntity
    }

    private void FinalizarAtaque()
    {
        if (Vivo)
            _estadoActual = MaquinaEstadoEnemigos.Idle;
    }
}

