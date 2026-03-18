using UnityEngine;

public abstract class MonitorMuerte: MonoBehaviour {
    void Start(){
        var sistema_salud = GetComponent<SistemaSalud>();
        sistema_salud.quienes_quieren_saber_de_la_salud += actualizacion_salud;
    }
    
    protected void actualizacion_salud(int cantidad_salud_nueva){
        Debug.Log($"[MonitorMuerte] La salud actual es {cantidad_salud_nueva}");
        Debug.Log($"[MonitorMuerte] es hora de morir {cantidad_salud_nueva <= 0}");
        if (cantidad_salud_nueva <= 0) {
            procesar_muerte();
        }
    }
    
    abstract public void procesar_muerte();
}
