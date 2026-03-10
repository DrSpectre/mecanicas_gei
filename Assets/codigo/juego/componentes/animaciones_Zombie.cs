using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimacionesZombie: MonoBehaviour{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Animator control_animacion;
    void Start(){
        control_animacion = GetComponent<Animator>();

        var control_de_movimiento = GetComponent<ZerebroEnemigo>();
        control_de_movimiento.componentes_escuhando_el_estado += al_cambiar_de_estado_del_zombie;
    }

    void al_cambiar_de_estado_del_zombie(ZombieEstados nuevo_estado) {
        switch (nuevo_estado) {
            case ZombieEstados.quieto:
                control_animacion.SetBool("esta_caminando", false);
                break;
        }
    }
    
}
