using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public enum EstadosMovimiento { 
    quieto,
    caminando,
    saltando,
}

public class JugadorMovimientoBase: MonoBehaviour{
    public float velocidad_movimiento = 5f;
    public float velocidad_rotacion = 1.0f;
    private float _velocidad_por_fotograma = 0.0f;

    public delegate void cambio_estado_evento(EstadosMovimiento estado_nuevo);

    public event cambio_estado_evento hay_gente_escuchando_el_estado;
    
    private EstadosMovimiento estado_actual = EstadosMovimiento.quieto;

    private Rigidbody rigid_body;
    private PlayerInput entradas_del_jugador;
    private InputAction movimiento;

    private InputAction stick_rotacion;

    private InputAction saltar;

    private Transform indicacion_direccion;
    void Start(){
        entradas_del_jugador = GetComponent<PlayerInput>();
        rigid_body = GetComponent<Rigidbody>();
        // Para tener una referencia a las pulsaciones de horizzontal y vertical del jugador en movimiento
        movimiento = entradas_del_jugador.actions.FindAction("movimiento");
        stick_rotacion = entradas_del_jugador.actions.FindAction("rotacion");


        _velocidad_por_fotograma = velocidad_movimiento / 60;

        indicacion_direccion = Camera.main.gameObject.transform;  // Jalamos la infomraicon de direccion y rotacion de la camara

        saltar = entradas_del_jugador.actions.FindAction("saltar");
        saltar.performed += salta_jugador_salta;
    }


    void salta_jugador_salta(InputAction.CallbackContext _) {
        bool estamos_tocando_suelo = false;
        Ray rayo_hacia_el_suelo = new Ray(transform.position, transform.TransformDirection(Vector3.down));

        Debug.Log($"El rayo tiene de ifnormacion {rayo_hacia_el_suelo}");

        RaycastHit chocamos_con;

        if (Physics.Raycast(rayo_hacia_el_suelo, out chocamos_con, 1.1F)){
            Debug.Log($"El rayo choco con {chocamos_con.collider.name}");
            Debug.Log($"El rayo choco con {chocamos_con.distance}");

            
            
            if (chocamos_con.collider.CompareTag("suelo")) {
                estamos_tocando_suelo = true;
            }
        }

        if (estamos_tocando_suelo) { 
            rigid_body.AddForce(Vector3.up * 15000f);
        }
    }

    // Update is called once per frame
    void FixedUpdate(){
        Vector2 direccion = movimiento.ReadValue<Vector2>();
        Vector2 rotacion = stick_rotacion.ReadValue<Vector2>();
        
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down), Color.green);

        if (direccion.magnitude > 0.1f) {
            cambiar_estado(EstadosMovimiento.caminando);
        }
        else{
            cambiar_estado(EstadosMovimiento.quieto);
        }

        // Debug.Log($"El estado actual del controlador de movimeitno es: {estado_actual}");

        if (direccion.magnitude > 0.1f){
            avanzar(direccion);
        }
        
        
        if (rotacion.magnitude > 0.1f){
            mirar(rotacion);
        }
    }
    void avanzar(Vector2 direccion_joystick){
        Vector3 adelante = indicacion_direccion.forward;
        Vector3 derecha = indicacion_direccion.right;

        adelante.y = 0f;
        derecha.y = 0f;
        
        Vector3 hacia_adelante = (adelante * direccion_joystick.y + derecha * direccion_joystick.x).normalized;
        hacia_adelante = hacia_adelante.normalized;

        rigid_body.MovePosition(transform.position + ((hacia_adelante * direccion_joystick.magnitude) * _velocidad_por_fotograma));

        mirar(direccion_joystick);
    }

    void mirar(Vector2 direccion_joystick){
        Vector3 adelante = indicacion_direccion.forward;
        Vector3 derecha = indicacion_direccion.right;

        adelante.y = 0f;
        derecha.y = 0f;

        Vector3 hacia_adelante = (adelante * direccion_joystick.y + derecha * direccion_joystick.x).normalized;
        hacia_adelante = hacia_adelante.normalized;


        Quaternion rotacion = Quaternion.LookRotation(hacia_adelante);
        rigid_body.MoveRotation(rotacion);
    }

    void cambiar_estado(EstadosMovimiento estado_nuevo) {
        estado_actual = estado_nuevo;

        if (hay_gente_escuchando_el_estado != null) {
            hay_gente_escuchando_el_estado.Invoke(estado_nuevo);
        }
    }
}
