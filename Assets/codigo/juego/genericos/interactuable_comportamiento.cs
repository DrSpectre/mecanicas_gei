using UnityEngine;

public enum TipoInteraccion{
    obtenible,
    recogible,
    presionable
}
public interface InteractuableComportamiento{
    TipoInteraccion tipo { get; set; }
}
