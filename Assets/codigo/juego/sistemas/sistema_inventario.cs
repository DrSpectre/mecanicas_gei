using System;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;

public class SistemaInventario: MonoBehaviour{
    private PlayerInput entradas_del_jugador;
    private InputAction interactuar;
    private InputAction atacar_derecha;
    private InputAction atacar_izquierda;

    private InputAction accion_derecha;
    private InputAction accion_izquierda;


    private UbicacionInventario mano_derecha; 
    private UbicacionInventario cabeza;
    private UbicacionInventario mano_izquierda;
    private InteractuableComportamiento puedo_tomar_esto;
    private GameObject objeto_activo_para_recoger;

    private List<GameObject> cosas_que_me_estoy_robando;
    void Start(){
        cosas_que_me_estoy_robando = new List<GameObject>();
        
        entradas_del_jugador = GetComponent<PlayerInput>();

        interactuar = entradas_del_jugador.actions.FindAction("interactuar");

        atacar_derecha = entradas_del_jugador.actions.FindAction("atacar_derecha");
        atacar_izquierda = entradas_del_jugador.actions.FindAction("atacar_izquierda");

        accion_derecha = entradas_del_jugador.actions.FindAction("accion_derecha");
        accion_izquierda = entradas_del_jugador.actions.FindAction("accion_izquierda");

        interactuar.performed += realizar_interaccion;
        atacar_derecha.performed += accion_atacar_derecha;
        atacar_izquierda.performed += accion_atacar_izquierda;
        accion_izquierda.performed += accion_interaccion_izquierda;
        accion_derecha.performed += accion_interaccion_derecha;

        var ubicaciones_inventario = GetComponentsInChildren<UbicacionInventario>();

        foreach (var ubicacion in ubicaciones_inventario) {
            if (ubicacion.lugar == NombreUbicacion.cabeza) {
                cabeza = ubicacion;
            }

            else if (ubicacion.lugar == NombreUbicacion.mano_derecha) {
                mano_derecha = ubicacion;
            }

            else {
                mano_izquierda = ubicacion;
            }
        } 
    }

    void realizar_interaccion(InputAction.CallbackContext _) {
        Debug.Log("Relaizando interaccion");
        if (puedo_tomar_esto == null) {
            return;
        }

        if (puedo_tomar_esto != null) {
            switch (puedo_tomar_esto.tipo) {
                case TipoInteraccion.recogible:
                    if (mano_derecha.GetComponent<UbicacionInventario>().ocupada) {
                        // soltar_lo_que_tengo();
                    }
                    else{
                        //colocar_en_mi_mano();
                    }
                    break;
            }
        }
    }

    void accion_interaccion_derecha(InputAction.CallbackContext _){
        if(puedo_tomar_esto != null && puedo_tomar_esto.tipo == TipoInteraccion.recogible){
            mano_derecha.interaccion(puedo_tomar_esto);
            puedo_tomar_esto = null;
        }

    }

    void accion_interaccion_izquierda(InputAction.CallbackContext _){
        if(puedo_tomar_esto != null && puedo_tomar_esto.tipo == TipoInteraccion.recogible){
            mano_izquierda.interaccion(puedo_tomar_esto);
            puedo_tomar_esto = null;
        }
    }

    void accion_atacar_derecha(InputAction.CallbackContext _) {
        Debug.Log("[SistemaInventario] Accion atacar derecha");
        mano_derecha.usar();
    }

    void accion_atacar_izquierda(InputAction.CallbackContext _) {
        Debug.Log("[SistemaInventario] Accion atacar izquierda");
        mano_izquierda.usar();
    }

    void OnTriggerEnter(Collider chocamos_con_algo){
        Debug.Log($"Estamos llegando con {chocamos_con_algo.gameObject.name}");

        var tipo_interactuable = chocamos_con_algo.GetComponent<InteractuableComportamiento>();

        if (tipo_interactuable != null) {
            puedo_tomar_esto = tipo_interactuable;

            var manejador_de_objeto_agarrable = chocamos_con_algo.GetComponent<AgarrableComponente>();
            manejador_de_objeto_agarrable?.marcar_como_observado();
        }
    }


    void OnTriggerExit(Collider abandonamos_algo){
        Debug.Log($"Estamos abandonando a {abandonamos_algo.gameObject.name}");
        var tipo_interactuable = abandonamos_algo.GetComponent<InteractuableComportamiento>();

        if (tipo_interactuable != null) {
            if (tipo_interactuable == puedo_tomar_esto) {
                puedo_tomar_esto = null;
            }
            
            var manejador_de_objeto_agarrable = abandonamos_algo.GetComponent<AgarrableComponente>();
            manejador_de_objeto_agarrable?.desamrcar_como_obervado();
        }
    }

}
