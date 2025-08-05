using UnityEngine;
using System;
public static class GameEvents 
{
    public static Action<int> OnMonedaRecogida;
    public static Action<int> OnVidaCambiada;
    public static Action<Ability> OnHabilidadDesbloqueada;
}