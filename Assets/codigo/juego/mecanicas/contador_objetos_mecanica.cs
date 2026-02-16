using System;
using UnityEngine;

public class ContadorObjetosMecanica : MonoBehaviour{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public String nombre_de_objeto_a_contar;
    public int cantidad_requerida;
    public GameObject puerta_a_desaparecer;

    private int _cantidad_actual = 0;

    void OnTriggerEnter(Collider quien_entra){
        var objeto = quien_entra.GetComponent<InteractuableComportamiento>();

        if (objeto != null) {
            if (objeto.nombre == nombre_de_objeto_a_contar) {
                _cantidad_actual = _cantidad_actual + 1;

                if (_cantidad_actual >= cantidad_requerida) {
                    puerta_a_desaparecer.SetActive(false);
                }
            }
        }

    }

    void OnTriggerExit(Collider quien_sale){
        var objeto = quien_sale.GetComponent<InteractuableComportamiento>();

        if (objeto != null) {
            if (objeto.nombre == nombre_de_objeto_a_contar) {
                _cantidad_actual = _cantidad_actual - 1;
            }
        }
    }


}
