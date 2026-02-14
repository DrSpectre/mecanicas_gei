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

        mi_manita.GetComponent<MeshRenderer>().enabled = false; 
    }

    void realizar_interaccion(InputAction.CallbackContext _) {
        if (puedo_tomar_esto == null) {
            return;
        }
        
        var que_tipo_de_interaccion_tiene = puedo_tomar_esto.GetComponent<InteractuableComportamiento>();

        if (que_tipo_de_interaccion_tiene != null) {
            switch (que_tipo_de_interaccion_tiene.tipo) {
                case TipoInteraccion.obtenible:
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
            
            puedo_tomar_esto.transform.parent = mi_manita.transform;
            puedo_tomar_esto.transform.position = mi_manita.transform.position;
        }
    }

    void soltar_lo_que_tengo() { 
        tengo_algo_en_mi_mano = false;
            
        puedo_tomar_esto.transform.parent = null;
        puedo_tomar_esto.transform.position = puedo_tomar_esto.transform.position;
    }


    void OnTriggerEnter(Collider chocamos_con_algo){
        Debug.Log($"Estamos llegando con {chocamos_con_algo.gameObject.name}");

        var que_tipo_de_interaccion_tiene = chocamos_con_algo.GetComponent<InteractuableComportamiento>();

        if (que_tipo_de_interaccion_tiene != null) {
            switch (que_tipo_de_interaccion_tiene.tipo) {
                case TipoInteraccion.obtenible:
                    puedo_tomar_esto = chocamos_con_algo.gameObject;
                    break;
            }
        }
    }


    void OnTriggerExit(Collider abandonamos_algo){
        Debug.Log($"Estamos abandonando a {abandonamos_algo.gameObject.name}");
    }



}
