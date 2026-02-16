using System;
using UnityEngine;

public enum TipoInteraccion{
    obtenible,
    recogible,
    presionable
}
public interface InteractuableComportamiento{
    TipoInteraccion tipo { get; set; }
    String nombre { get; set; }

    void colocar_en(Transform ubicacion);

    void soltar();
    void arrojar(float fuerza);
}
