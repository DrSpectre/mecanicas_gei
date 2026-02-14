using UnityEngine;
using UnityEngine.InputSystem;

public class GeneradorJugadores: MonoBehaviour{

    public Transform punto_reaparicion_1;
    public GameObject jugador;
    private void Awake(){
        Instantiate(jugador, punto_reaparicion_1.position, punto_reaparicion_1.rotation);
    }

    public void OnPlayerJoined(PlayerInput control_jugador) {
        Debug.Log("Se ha escuchado este mensaje");
        control_jugador.transform.position = punto_reaparicion_1.transform.position;
        Debug.Log($"el jugador {control_jugador} ha entrado");
    }
    
}
