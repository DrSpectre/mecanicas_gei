using UnityEngine;

public class ArmaFuego: ArmaComponente{
    public override void dañar() {
        foreach (var enemigo in a_quienes_dañar) {
            hacer_daño(enemigo);
        }
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


    void OnTriggerEnter(Collider quien_entra){
        if (!a_quienes_dañar.Contains(quien_entra.gameObject)) { 
            a_quienes_dañar.Add(quien_entra.gameObject);
        }
    }

    void OnTriggerExit(Collider quien_sale){
        if (a_quienes_dañar.Contains(quien_sale.gameObject)) {
            a_quienes_dañar.Remove(quien_sale.gameObject);
        }
    }
}
