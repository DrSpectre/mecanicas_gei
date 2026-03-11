using UnityEngine;
using UnityEngine.UI;

public class BarraSalud: MonitorSalud {
    private Slider barra;

    void Start(){
        base.inicializar();

        barra = GetComponentInChildren<Slider>();
    }

    public override void actualizacion_salud(int cantidad_salud_nueva){
        barra.value = cantidad_salud_nueva;
    }
}
