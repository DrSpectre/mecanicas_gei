using System;
using System.Collections.Generic;
using UnityEngine;

public class ControladorGeneral : MonoBehaviour {
    public SistemaSalud jugador;

    public int cantidad_enemigos;
    private int hamburguesas_recolectadas = 0;
    public int cantidad_hamburguesas_necesarias = 5;

    public Dictionary<string, int> contador_cosas;

    public GameObject puerta_a_abir;

    public delegate void actualizacion(string nombre, string valor);

    public event actualizacion quienes_estan_al_pendiente;

    void Start() { }

    public void actualizar(string nombre, string valor){
        // Aqui deberia escribir algunas cosas mas
        Debug.Log($"[ControladorGeneral] nombre: {nombre} valor: {valor} ");

        switch (nombre) {
            case "hambuerguesa":
                hamburguesas_recolectadas += 1;
                break;
                
            default:
                break;
        }
        
        quienes_estan_al_pendiente?.Invoke(nombre, Convert.ToString(hamburguesas_recolectadas));
    }
    
}
