using UnityEngine;

[RequireComponent(typeof(MonitorMuerte))]
public class SistemaSalud: MonoBehaviour{
    public int salud = 300;
    private int salud_restante = 0;
    private MonitorMuerte al_morir;
    void Start(){
        salud_restante = salud;
        al_morir = GetComponent<MonitorMuerte>();
    }

    public void restar_salud(int cantidad){
        salud_restante = salud_restante - cantidad;

        if (salud_restante < 0){
            al_morir.procesar_muerte();
        }
    }
}


