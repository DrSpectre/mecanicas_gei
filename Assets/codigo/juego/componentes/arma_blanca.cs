using System.Collections.Generic;
using UnityEngine;

public class ArmaBlanca: ArmaComponente{
    public override void dañar(){
        foreach (var enemigo in a_quienes_dañar){
            hacer_daño(enemigo);
        }

        Debug.Log($"[ArmaBlanca] llamadno a dañar");
    }

    void hacer_daño(GameObject a_quien) {
        SistemaSalud salud_de_a_aquin_acuchillo = a_quien.GetComponent<SistemaSalud>();
        
        Debug.Log($"[ArmaBlanca] dañando a {a_quien.name}");
        
        if (salud_de_a_aquin_acuchillo != null) {
            if (a_quien.gameObject.CompareTag("jugador")) {
                return;
            }
            salud_de_a_aquin_acuchillo.restar_salud(daño);
        }
    }
    void OnTriggerEnter(Collider acuchillar){
        if (!a_quienes_dañar.Contains(acuchillar.gameObject)) { 
            a_quienes_dañar.Add(acuchillar.gameObject);
        }
    }

    void OnTriggerExit(Collider acuchillar){
        if (a_quienes_dañar.Contains(acuchillar.gameObject)) {
            a_quienes_dañar.Remove(acuchillar.gameObject);
        }
    }
}
