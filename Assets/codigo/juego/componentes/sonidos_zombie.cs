using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SonidosZombie: MonoBehaviour{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public AudioClip estando_quieto;
    private AudioSource fuente_sonido;
    void Start(){
        fuente_sonido = GetComponent<AudioSource>();

        var control_de_movimiento = GetComponent<ZerebroEnemigo>();
        control_de_movimiento.componentes_escuhando_el_estado += al_cambiar_de_estado_del_zombie;
    }

    void al_cambiar_de_estado_del_zombie(ZombieEstados nuevo_estado) {
        switch (nuevo_estado) {
            case ZombieEstados.quieto:
                reproducir(estando_quieto);
                break;
            case ZombieEstados.patrullando:
                reproducir(estando_quieto);
                break;
        }
    }

    void reproducir(AudioClip sonido) {
        fuente_sonido.loop = true;
        fuente_sonido.PlayOneShot(sonido);
    }
}
