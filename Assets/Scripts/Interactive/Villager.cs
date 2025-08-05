using UnityEngine;
using System;
using TMPro;
public class Villager : MonoBehaviour, IInteractuable
{
    [SerializeField, TextArea(3, 10)] private string[] dialogos;  //creamos array con todos los texto que le voy a poner tambien el text sirve para que podamos poner de una el texto en la array
    [SerializeField] private UIManager uiManager;
    [SerializeField] private TMP_Text pressEText;
    private bool esperarUnFrame = false; //evito mismo input en un frame

    [SerializeField] private Ability habilidadParaDar; // habilidad que da este aldeano un scritable object
    private bool habilidadEntregada = false;

    private int dialogoIndex = 0;
    private bool dialogoActivo = false;

    public static event Action OnDialogoIniciado; //aca se comunicara el evento.
    public static event Action OnDialogoTerminado;


    private Player jugador;
    private Rigidbody2D jugadorRb;

    private float distanciaInteraccion = 2f;
    private void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        jugadorRb = jugador.GetComponent<Rigidbody2D>();

        pressEText.gameObject.SetActive(false);
    }
    private void Update()
    {
        float distancia = Vector2.Distance(transform.position, jugador.transform.position);

        if (distancia < distanciaInteraccion && !dialogoActivo)
        {
            pressEText.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                pressEText.gameObject.SetActive(false);
                Interactuar(jugador);
            }
        }
        else if (!dialogoActivo)
        {
            pressEText.gameObject.SetActive(false);
        }

        if (dialogoActivo && Input.GetKeyDown(KeyCode.E))
        {
            if (esperarUnFrame)
            {
                esperarUnFrame = false; // salta este frame
            }
            else
            {
                MostrarSiguienteDialogo();
            }
        }

    }

    public void Interactuar(Player jugador)
    {
        dialogoActivo = true;
        dialogoIndex = 0;
        jugadorRb.constraints = RigidbodyConstraints2D.FreezeAll;
        uiManager.ActivarCajaDialogo(true);
        OnDialogoIniciado?.Invoke();

        esperarUnFrame = true;
        MostrarSiguienteDialogo();
    }

    private void MostrarSiguienteDialogo()
    {
        if (dialogoIndex < dialogos.Length) //recorro para ir poniendo el texto
        {
        
            uiManager.MostrarTexto(dialogos[dialogoIndex]);// mostrar elemento texto del indice
            dialogoIndex++;
        }
        else
        {
            TerminarDialogo();
        }
    }

    private void TerminarDialogo()
    {
        dialogoActivo = false; //desativa 
        uiManager.ActivarCajaDialogo(false);
        jugadorRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        OnDialogoTerminado?.Invoke();

        if (habilidadParaDar != null && !habilidadEntregada) // entregamos habilidad si el aldeano da habilidad
        {
            jugador.DesbloquearHabilidad(habilidadParaDar);
            habilidadEntregada = true; // para que no se repita cuando abra de nuevo
        }
    }
}


