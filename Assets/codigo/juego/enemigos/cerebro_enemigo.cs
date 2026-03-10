using System.Runtime.CompilerServices;
using Mono.Cecil.Cil;
using UnityEngine;

public enum ZombieEstados{
    quieto,
    patrullando,
    perseguir_jugador,
    atacar,
}

[RequireComponent(typeof(MoviminentoEnemigo))]
public class ZerebroEnemigo: MonoBehaviour{
    private ZombieEstados estado_actual{
        get => _estado_actual;
        set {
            _estado_actual = value;
            componentes_escuhando_el_estado?.Invoke(_estado_actual);
        }
    }

    [SerializeField]
    private ZombieEstados _estado_actual = ZombieEstados.quieto;
    private GameObject[] puntos_patrullaje;
    public GameObject comida_rapida;

    private GameObject objetivo_movimiento;

    private MoviminentoEnemigo control_movimiento;
    
    public delegate void cambio_estado(ZombieEstados estado_nuevo);

    public event cambio_estado componentes_escuhando_el_estado;

    private float reloj = 0;
    
    
    void Start(){
        control_movimiento = GetComponent<MoviminentoEnemigo>();

        control_movimiento.a_quien_seguir = null;

        puntos_patrullaje = GameObject.FindGameObjectsWithTag("punto_patrullaje");
    }

    // Update is called once per frame
    void Update(){
        switch (estado_actual){
            case ZombieEstados.quieto:
                estado_quieto();
                break;

            case ZombieEstados.patrullando:
                estado_patrullando();
                break;

            case ZombieEstados.perseguir_jugador:
                estado_perseguir_jugador();
                break;

            case ZombieEstados.atacar:
                estado_atacar();
                break;
        }
    }

    void estado_perseguir_jugador() {
        control_movimiento.a_quien_seguir = objetivo_movimiento;
        
        if ((transform.position - objetivo_movimiento.transform.position).magnitude < 1f) {
            estado_actual = ZombieEstados.atacar;
            reloj = -1;
            Debug.Log($"[ZerebroEnemigo][{gameObject.name}] Listo, ahora solo me falta atacar");
        }
    }

    void estado_atacar() {
        if (reloj <= 0.01F){
            Debug.Log($"[ZerebroEnemigo][{gameObject.name}] Estamos ataacndo jefe");
            var salud_enemiga = objetivo_movimiento?.GetComponent<SistemaSalud>();
            salud_enemiga?.restar_salud(150);

            // objetivo_movimiento = null;
            control_movimiento.a_quien_seguir = null;
        }
        
        else if (reloj > 2) {
            estado_actual = ZombieEstados.perseguir_jugador;
            reloj = 0;
        }
        
        reloj = reloj + Time.deltaTime;
        
    }

    void estado_quieto() {
        objetivo_movimiento = null;
        
        reloj = reloj + Time.deltaTime;
        Debug.Log($"La hora actual es {reloj}");

        if (reloj > 1F) {
            reloj = 0;

            var resultado = Random.Range(0, 5);
            
            if(resultado > 1){
                estado_actual = ZombieEstados.patrullando;
            }
        }
    }

    void estado_patrullando() {
        Debug.Log($"Hey, este {gameObject.name} se supone deberia estar patrullando");
        
        if (objetivo_movimiento == null) {
            var punto_de_interes = Random.Range(0, puntos_patrullaje.Length);
            objetivo_movimiento = puntos_patrullaje[punto_de_interes];
        }
        
        Debug.Log($"[ZerebroEnemigo][{gameObject.name}] Hey, este {gameObject.name} esta a una disntica de {(transform.position - objetivo_movimiento.transform.position).magnitude} de su objetivo");

        control_movimiento.a_quien_seguir = objetivo_movimiento;

        if ((transform.position - objetivo_movimiento.transform.position).magnitude < 2f) {
            estado_actual = ZombieEstados.quieto;
            objetivo_movimiento = null;
        }
    }

    void actualizar_estado_por_observacion(Collider verificar_a) { 
                // Debug.Log($"[ZerebroEnemigo]Veo a {parece_que_veo_a.name}");

        Ray rayo_de_vision = new Ray(transform.position + transform.forward, verificar_a.transform.position - transform.position);

        // Ray rayo_de_vision = new Ray(transform.position, transform.TransformDirection(parece_que_veo_a.transform.position));    
        Debug.DrawRay(transform.position + transform.forward, verificar_a.transform.position - transform.position, Color.red, 5f);

        RaycastHit vemos_a;
   

        if (Physics.Raycast(rayo_de_vision, out vemos_a, 10F)){
            GameObject objeto_visto = vemos_a.collider.gameObject;
            
            Debug.Log($"[ZerebroEnemigo][{gameObject.name}] El rayo choco con {vemos_a.collider.name}");
            Debug.Log($"[ZerebroEnemigo][{gameObject.name}] A una distancia de  {vemos_a.distance}");
            
            if (objeto_visto.CompareTag("jugador")) {
                switch (estado_actual) {
                    case ZombieEstados.quieto:
                    case ZombieEstados.patrullando:
                        estado_actual = ZombieEstados.perseguir_jugador;
                        objetivo_movimiento = objeto_visto;
                        break;
                }
            }
        }
    }

    void OnTriggerEnter(Collider parece_que_veo_a){
        actualizar_estado_por_observacion(parece_que_veo_a);
    }

    void OnTriggerStay(Collider parece_sigue_estando_en_vision){
        actualizar_estado_por_observacion(parece_sigue_estando_en_vision);
    }
}
