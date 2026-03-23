using UnityEngine;
using UnityEngine.Events;

public enum EstadosSistemaSalud { 
    recibi_daño,
    listo_para_otra_patada
}

[RequireComponent(typeof(MonitorMuerte))]
public class SistemaSalud: MonoBehaviour{
    public float tiempo_espera = 3f;

    private EstadosSistemaSalud estado = EstadosSistemaSalud.listo_para_otra_patada;

    private float _tiempo_de_daño = 0f;
    public int salud = 300;
    public UnityEvent evento;
    private int salud_restante { 
        get => _salud_restante;
        set {
            _salud_restante = value;

            if (_salud_restante <= 0){
                quienes_quieren_saber_de_la_salud?.Invoke(0);
            }
            else{
                int porcentaje_salud = (_salud_restante * 100) / salud; // PAra saber cuanta salud tiene mi personaje
                quienes_quieren_saber_de_la_salud?.Invoke(porcentaje_salud);
            }
        }
    }
    private int _salud_restante = 0;
    public delegate void cambio_salud(int cantidad_actual_salud);

    public event cambio_salud quienes_quieren_saber_de_la_salud;
    
    void Start(){
        salud_restante = salud;
    }

    void FixedUpdate(){
        if (estado == EstadosSistemaSalud.recibi_daño) {
            _tiempo_de_daño -= Time.deltaTime;

            if (_tiempo_de_daño < 0f) {
                estado = EstadosSistemaSalud.listo_para_otra_patada;
            }
        }
    }

    public void restar_salud(int cantidad){
        if (estado == EstadosSistemaSalud.listo_para_otra_patada){
            salud_restante = salud_restante - cantidad;

            estado = EstadosSistemaSalud.recibi_daño;
            _tiempo_de_daño = tiempo_espera;
        }
    }
}


