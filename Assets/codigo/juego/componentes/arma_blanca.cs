using System.Collections.Generic;
using UnityEngine;

public class ArmaBlanca: MonoBehaviour{
    public int daño = 100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private List<GameObject> a_quienes_me_rajo;
    void Start(){
        a_quienes_me_rajo = new List<GameObject>();
    }

    void OnTriggerEnter(Collider acuchillar){
        SistemaSalud salud_de_a_aquin_acuchillo = acuchillar.GetComponent<SistemaSalud>();

        if (salud_de_a_aquin_acuchillo != null) {
            if (acuchillar.gameObject.tag == "jugador") {
                return;
            }
            salud_de_a_aquin_acuchillo.restar_salud(daño);
        }
    }

    void OnTriggerExit(Collider acuchillar){
        SistemaSalud salud_de_a_aquin_acuchillo = acuchillar.GetComponent<SistemaSalud>();

        if (salud_de_a_aquin_acuchillo != null) {
            salud_de_a_aquin_acuchillo.restar_salud(daño);
        }
    }
}
