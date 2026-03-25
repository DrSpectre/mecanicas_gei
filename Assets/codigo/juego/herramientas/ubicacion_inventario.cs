using UnityEngine;

public enum NombreUbicacion{
    cabeza,
    mano_derecha,
    mano_izquierda
}

public class UbicacionInventario: MonoBehaviour{
    public NombreUbicacion lugar = NombreUbicacion.mano_derecha;

    public bool ocupada = false;
}
