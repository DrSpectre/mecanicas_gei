
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class JugadorMovimientoTanque: MonoBehaviour{
    public float velocidad_movimiento = 5f;
    public float velocidad_rotacion = 1.0f;
    private float _velocidad_por_fotograma = 0.0f;

    private Rigidbody rigid_body;
    private PlayerInput entradas_del_jugador;
    private InputAction movimiento;

    void Start(){
        entradas_del_jugador = GetComponent<PlayerInput>();
        rigid_body = GetComponent<Rigidbody>();
        // Para tener una referencia a las pulsaciones de horizzontal y vertical del jugador en movimiento
        movimiento = entradas_del_jugador.actions.FindAction("movimiento");

        _velocidad_por_fotograma = velocidad_movimiento / 60;
    }

    void FixedUpdate(){
        Vector2 direccion = movimiento.ReadValue<Vector2>();

        avanzar(direccion);
        rotar(direccion);

    }

    void avanzar(Vector2 direccion_joystick){
        Vector3 hacia_adelante = transform.forward * direccion_joystick.y;

        rigid_body.MovePosition(transform.position + ((hacia_adelante * direccion_joystick.magnitude) * _velocidad_por_fotograma));
    }

    void rotar(Vector2 direccion_joystick){
        float voltear = velocidad_rotacion * direccion_joystick.x;

        Quaternion rotacion = Quaternion.Euler(0f, voltear, 0f);
        rigid_body.MoveRotation(transform.rotation * rotacion);
    }

}
