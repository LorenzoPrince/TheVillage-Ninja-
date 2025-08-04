using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject cajaDialogo;
    [SerializeField] private TMP_Text textoDialogo;

    public void ActivarCajaDialogo(bool activar)
    {
        cajaDialogo.SetActive(activar);
    }

    public void MostrarTexto(string texto)
    {
        textoDialogo.text = texto.ToString();
        Debug.Log("Mostrando texto: " + texto);
    }
}
