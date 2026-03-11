using TMPro;
using UnityEngine;

public abstract class MonitorSalud : MonoBehaviour{
    public void inicializar(){
        var sistema_salud = transform.parent.GetComponent<SistemaSalud>();
        sistema_salud.quienes_quieren_saber_de_la_salud += actualizacion_salud;
    }

    abstract protected void actualizacion_salud(int cantidad_salud_nueva);
}