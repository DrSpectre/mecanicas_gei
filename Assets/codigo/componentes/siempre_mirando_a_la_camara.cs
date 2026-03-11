using UnityEngine;

public class SiempreMirandoALaCamara: MonoBehaviour{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    GameObject camara;
    void Start(){
        camara = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update() {
        transform.rotation = Quaternion.LookRotation(transform.position - camara.transform.position);
    }
}
