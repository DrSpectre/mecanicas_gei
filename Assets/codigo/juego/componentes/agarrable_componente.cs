using System;
using UnityEngine;

// [RequireComponent(typeof(Rigidbody))]
public class AgarrableComponente: MonoBehaviour, InteractuableComportamiento{
    public TipoInteraccion tipo { get; set; }
    // public string nombre { get => nombre_actual; set; }

    private bool estoy_siendo_sostenido = false;
    private SeguirRigidbody fisicas;

    public String nombre { get; set; }

    public String nombre_actual;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        tipo = TipoInteraccion.recogible;

        nombre = nombre_actual;

        fisicas = GetComponent<SeguirRigidbody>();
    }
    public void colocar_en(Transform ubicacion){
        fisicas.desactviar();
        
        transform.SetParent(ubicacion);
    }

    public void soltar() {
        fisicas.activar();
        
        transform.SetParent(null);

        transform.position = transform.position;
        transform.rotation = transform.rotation;
        estoy_siendo_sostenido = false;

    }

    public void arrojar(float fuerza) {
        // fisicas.AddForce(transform.parent.forward * fuerza);
        
        soltar();
    }
}
