using Unity.Cinemachine;
using UnityEngine;

public class CamaraControlBasico: MonoBehaviour{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private CinemachineCamera camara;

    public GameObject[] paredes_a_ocultar;
    
    void Start(){
        camara = GetComponentInChildren<CinemachineCamera>();
        Debug.Log($"La camra se llama {camara.name}");
    }

    void OnTriggerEnter(Collider quien_entra){
        if (quien_entra.tag == "jugador") {
            camara.Priority = 100;

            foreach (var pared in paredes_a_ocultar) {
                var render_pared = pared.GetComponent<MeshRenderer>();
                render_pared.enabled = false;
            }
        }
    }

    void OnTriggerExit(Collider quien_sale){
        if (quien_sale.tag == "jugador") {
            camara.Priority = 0;
            
            foreach (var pared in paredes_a_ocultar) {
                var render_pared = pared.GetComponent<MeshRenderer>();
                render_pared.enabled = true;
            }
        }
    }

}
