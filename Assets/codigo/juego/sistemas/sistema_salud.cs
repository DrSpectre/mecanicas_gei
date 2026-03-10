using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MonitorMuerte))]
public class SistemaSalud: MonoBehaviour{
    public int salud = 300;
    public UnityEvent evento;
    private int salud_restante = 0;
    private MonitorMuerte al_morir;
    void Start(){
        salud_restante = salud;
    }

    public void restar_salud(int cantidad){
        salud_restante = salud_restante - cantidad;

        if (salud_restante < 0){
            // al_morir.procesar_muerte();
            Debug.Log("Legamos al final de la vida del jugador");
        }
    }
}


