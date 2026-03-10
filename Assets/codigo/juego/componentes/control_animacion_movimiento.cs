using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ControlAnimacionMovimiento: MonoBehaviour{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Animator control_animacion;
    void Start(){
        control_animacion = GetComponent<Animator>();

        var control_de_movimiento = GetComponent<JugadorMovimientoBase>();
        control_de_movimiento.hay_gente_escuchando_el_estado += al_cambiar_de_estado_del_control_de_movimiento;
    }

    void al_cambiar_de_estado_del_control_de_movimiento(EstadosMovimiento nuevo_estado) {
        switch (nuevo_estado) {
            case EstadosMovimiento.quieto:
                control_animacion.SetBool("esta_caminando", false);
                break;

            case EstadosMovimiento.caminando:
                control_animacion.SetBool("esta_caminando", true);
                break;
                
            case EstadosMovimiento.saltando:
                break;
                
        }
    }
}
