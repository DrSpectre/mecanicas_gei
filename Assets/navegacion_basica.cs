using UnityEngine;
using UnityEngine.AI;

public class NavegacionBasica: MonoBehaviour{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject objetivo;

    private NavMeshAgent navegacion_ia;
    void Start(){

        navegacion_ia = GetComponent<NavMeshAgent>();

        navegacion_ia.destination = objetivo.transform.position;
        
    }

    // Update is called once per frame
    void FixedUpdate(){
        navegacion_ia.destination = objetivo.transform.position;
    }
}
