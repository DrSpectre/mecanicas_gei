using UnityEngine;

public class AgarrableComponente: MonoBehaviour, InteractuableComportamiento{
    public TipoInteraccion tipo { get; set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        tipo = TipoInteraccion.obtenible;
    }
}
