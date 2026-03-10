using System;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;

public class SistemaInventario: MonoBehaviour{
    private PlayerInput entradas_del_jugador;
    private InputAction interactuar;
    private bool tengo_algo_en_mi_mano = false;

    private GameObject mi_manita;
    private GameObject puedo_tomar_esto;

    private List<GameObject> cosas_que_me_estoy_robando;
    void Start(){
        cosas_que_me_estoy_robando = new List<GameObject>();
        
        entradas_del_jugador = GetComponent<PlayerInput>();
        interactuar = entradas_del_jugador.actions.FindAction("interactuar");

        interactuar.performed += realizar_interaccion;

        mi_manita = GameObject.Find("manita");

        Debug.Log($"Estamos buscando otra cosa: {GameObject.Find("Enemigo")}");

        mi_manita.GetComponent<MeshRenderer>().enabled = false; 
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
                    if (tengo_algo_en_mi_mano) {
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
            tengo_algo_en_mi_mano = true;

            var objeto = puedo_tomar_esto.GetComponent<InteractuableComportamiento>();

            objeto.colocar_en(mi_manita.transform);
        }
    }

    void soltar_lo_que_tengo() { 
        tengo_algo_en_mi_mano = false;

        var objeto = puedo_tomar_esto.GetComponent<InteractuableComportamiento>();

        // objeto.soltar();
        objeto.arrojar(250.0f);
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
