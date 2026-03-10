using System.Collections.Generic;
using UnityEngine;

public class ArmaBlanca: MonoBehaviour{
    public int daño = 100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private List<GameObject> a_quienes_me_rajo;
    void Start(){
        a_quienes_me_rajo = new List<GameObject>();
    }

    void hacer_daño(GameObject a_quien) {
        SistemaSalud salud_de_a_aquin_acuchillo = a_quien.GetComponent<SistemaSalud>();
        
        if (salud_de_a_aquin_acuchillo != null) {
            if (a_quien.gameObject.CompareTag("jugador")) {
                return;
            }
            salud_de_a_aquin_acuchillo.restar_salud(daño);
        }
    }
    void OnTriggerEnter(Collider acuchillar){
        hacer_daño(acuchillar.gameObject);
    }

    void OnTriggerExit(Collider acuchillar){
        hacer_daño(acuchillar.gameObject);
    }
}
