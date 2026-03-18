// using System;
using System;
using UnityEngine;

public class ChecadorComponentes: MonoBehaviour{
    public int cantidad_requerida;
    public string cosa_requerida;
    private ControladorGeneral controlador;
    private Animator gestoranimacion;
    public bool estado_abierta;
    
    void Start(){
        controlador = FindAnyObjectByType<ControladorGeneral>();

        controlador.quienes_estan_al_pendiente += actualizar_puerta;

        gestoranimacion = GetComponent<Animator>();
    
    }

    void actualizar_puerta(string nombre, string valor) {
        if (nombre == cosa_requerida && (Convert.ToInt32(valor) >= cantidad_requerida)) {
            gestoranimacion.SetBool("abrida", true);
            estado_abierta = true;
        }
    }

}
