using System;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;

public class SistemaInventario: MonoBehaviour{
    private PlayerInput entradas_del_jugador;
    private InputAction interactuar;
    private InputAction atacar;
    private bool tengo_algo_en_mi_mano = false;


    private GameObject mano_derecha; 
    private GameObject cabeza;
    private GameObject mano_izquierda;
    private GameObject puedo_tomar_esto;

    private List<GameObject> cosas_que_me_estoy_robando;
    void Start(){
        cosas_que_me_estoy_robando = new List<GameObject>();
        
        entradas_del_jugador = GetComponent<PlayerInput>();
        interactuar = entradas_del_jugador.actions.FindAction("interactuar");
        atacar = entradas_del_jugador.actions.FindAction("atacar");

        interactuar.performed += realizar_interaccion;
        atacar.performed += atacar_con_arma;

        var ubicaciones_inventario = GetComponentsInChildren<UbicacionInventario>();

        foreach (var ubicacion in ubicaciones_inventario) {
            if (ubicacion.lugar == NombreUbicacion.cabeza) {
                cabeza = ubicacion.gameObject;
            }

            else if (ubicacion.lugar == NombreUbicacion.mano_derecha) {
                mano_derecha = ubicacion.gameObject;
            }

            else {
                mano_izquierda = ubicacion.gameObject;
            }
        } 
    }

    void realizar_interaccion(InputAction.CallbackContext _) {
        Debug.Log("Relaizando interaccion");
        if (puedo_tomar_esto == null) {
            return;
        }
        
        var que_tipo_de_interaccion_tiene = puedo_tomar_esto.GetComponent<InteractuableComportamiento>();

        if (que_tipo_de_interaccion_tiene != null) {
            switch (que_tipo_de_interaccion_tiene.tipo) {
                case TipoInteraccion.recogible:
                    if (mano_derecha.GetComponent<UbicacionInventario>().ocupada) {
                        soltar_lo_que_tengo();
                    }
                    else{
                        colocar_en_mi_mano();
                    }
                    break;
            }
        }
    }
    
    void colocar_en_mi_mano() {
        if (puedo_tomar_esto != null) {
            var objeto = puedo_tomar_esto.GetComponent<InteractuableComportamiento>();

            if (!mano_derecha.GetComponent<UbicacionInventario>().ocupada) {
                objeto.colocar_en(mano_derecha.transform);
                mano_derecha.GetComponent<UbicacionInventario>().ocupada = true;
            }
        }
    }

    void soltar_lo_que_tengo() { 
        tengo_algo_en_mi_mano = false;

        var objeto = puedo_tomar_esto.GetComponent<InteractuableComportamiento>();

        // objeto.soltar();
        objeto.arrojar(250.0f);
    }

    void atacar_con_arma(InputAction.CallbackContext _) {
        Debug.Log("[SistemaInventario] mandnado a ejecutar un ataque");
        
        if (tengo_algo_en_mi_mano) {
            var arma = puedo_tomar_esto.GetComponent<ArmaComponente>();

            arma.dañar();
        }
    }

    void OnTriggerEnter(Collider chocamos_con_algo){
        Debug.Log($"Estamos llegando con {chocamos_con_algo.gameObject.name}");

        var que_tipo_de_interaccion_tiene = chocamos_con_algo.GetComponent<InteractuableComportamiento>();

        if (que_tipo_de_interaccion_tiene != null && !tengo_algo_en_mi_mano) {
            puedo_tomar_esto = chocamos_con_algo.gameObject;
        }
    }


    void OnTriggerExit(Collider abandonamos_algo){
        Debug.Log($"Estamos abandonando a {abandonamos_algo.gameObject.name}");
        var que_tipo_de_interaccion_tiene = abandonamos_algo.GetComponent<InteractuableComportamiento>();

        if (que_tipo_de_interaccion_tiene != null && !tengo_algo_en_mi_mano) {
            if (abandonamos_algo.gameObject == puedo_tomar_esto) {
                puedo_tomar_esto = null;
            }
        }
    }



}
