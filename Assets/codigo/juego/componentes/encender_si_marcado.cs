using System;
using UnityEngine;

public class EncenderSiMarcado : MonoBehaviour{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject objeto_a_activar;

    void Start() {
        GameObject padre = transform.parent.gameObject;

        padre.GetComponent<AgarrableComponente>().saber_si_soy_observado += activar;

        objeto_a_activar.SetActive(false);
    }

    private void activar(bool encender) {
        objeto_a_activar.SetActive(encender);
    }
}
