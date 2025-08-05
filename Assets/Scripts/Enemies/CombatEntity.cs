using UnityEngine;

public abstract class CombatEntity : MonoBehaviour,IDamageable //esto permite que sea un molde en el cual se basaran los otros enemigos.
{
    [SerializeField] private int _vidaMaxima = 100;
    private int _vidaActual;
    private bool _vivo = true;

    public int Vida => _vidaActual; // permite que lo lean pero no que cambien.
    public bool Vivo => _vivo;
    protected virtual void Awake() // antes que inicie el juego
    {
        _vidaActual = _vidaMaxima;
    }
    public virtual void RecibirDaño(int cantidad) // el virtual permite que con override se pueda modificar la clase que hija y tambien puedo llamar lo que puse. Recordar no es obligatorio sobre escribirlo. 
    {
        if (!_vivo) return;

        _vidaActual -= cantidad;
        _vidaActual = Mathf.Max(0, _vidaActual); // que sea preciso el numero y no haya errores
        Debug.Log($"Vida bajó en {cantidad}. Vida actual: {Vida}");
        if (_vidaActual == 0) 
            Morir();
    }
    public virtual void Morir()
    {
        _vivo = false;
        Destroy(gameObject); 
    }

    public virtual void Mover(Vector2 direccion, float velocidad)
    {
        if (_vivo)
            transform.Translate(direccion.normalized * velocidad * Time.deltaTime);
    }
}
