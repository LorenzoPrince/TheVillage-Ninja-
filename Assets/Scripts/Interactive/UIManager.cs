using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [Header("UI de Monedas")]
    [SerializeField] private TMP_Text textoMonedas;

    [Header("UI de Vida")]
    [SerializeField] private Slider barraVida;
    [SerializeField] private int vidaMaxima = 100;

    [Header("UI de Diálogo")]
    [SerializeField] private GameObject cajaDialogo;
    [SerializeField] private TMP_Text textoDialogo;
 
    private void OnEnable()
    {
        GameEvents.OnMonedaRecogida += MostrarMonedas; //se suscribe al evento
        GameEvents.OnVidaCambiada += MostrarVida;
        GameEvents.OnHabilidadDesbloqueada += MostrarHabilidad;
    }

    private void OnDisable()
    {
        GameEvents.OnMonedaRecogida -= MostrarMonedas; //se desuscribe
        GameEvents.OnVidaCambiada -= MostrarVida;
        GameEvents.OnHabilidadDesbloqueada -= MostrarHabilidad;
    }

    //ui de dialogo
    public void ActivarCajaDialogo(bool activar)
    {
        cajaDialogo.SetActive(activar);
    }

    public void MostrarTexto(string texto)
    {
        textoDialogo.text = texto.ToString();
        Debug.Log("Mostrando texto: " + texto);
    }
    //eventos ui

    private void MostrarMonedas(int total)
    {
        textoMonedas.text = $"Monedas: {total}";

    }

    private void MostrarVida(int vida)
    {
        if (barraVida != null)
        {
            barraVida.value = (float)vida / vidaMaxima;
        }
        Debug.Log($"Vida actual: {vida}");

    }

    private void MostrarHabilidad(Ability habilidad)
    {
        Debug.Log($"¡Nueva habilidad desbloqueada! {habilidad.Nombre}");
      
        MostrarTexto($"¡Nueva habilidad desbloqueada!\n{habilidad.Nombre}: {habilidad.Descripcion}");
    }
}
