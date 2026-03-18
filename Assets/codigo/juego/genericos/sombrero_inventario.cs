using Mono.Cecil.Cil;
using UnityEngine;

public enum TiposInventario { 
    mano_derecha,
    mano_izquierda,
    espalda,
    cabeza_sombrero 
}
public class TipoDeInventario: MonoBehaviour{
    public TiposInventario tipo;
}
