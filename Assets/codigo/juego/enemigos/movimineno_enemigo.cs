using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MoviminentoEnemigo : MonoBehaviour{
    public GameObject a_quien_seguir;

    private NavMeshAgent control_movimiento;
    void Start(){
        control_movimiento = GetComponent<NavMeshAgent>();
    }

    void Update(){
        if (a_quien_seguir != null){
            Debug.Log($"[MoviminentoEnemigo][{gameObject.name}] siguiendo a {a_quien_seguir.name}");
            control_movimiento.destination = a_quien_seguir.transform.position;
        }
        
        else{
            control_movimiento.destination = gameObject.transform.position;
        }
        
    }
}
