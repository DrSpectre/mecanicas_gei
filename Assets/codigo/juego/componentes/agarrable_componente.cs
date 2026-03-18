using System;
using UnityEngine;

// [RequireComponent(typeof(Rigidbody))]
public class AgarrableComponente: MonoBehaviour, InteractuableComportamiento{
    public TipoInteraccion tipo { get; set; }
    // public string nombre { get => nombre_actual; set; }

    private Rigidbody fisicas;
    private bool estoy_siendo_sostenido = false;

    public String nombre { get; set; }

    public String nombre_actual;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        tipo = TipoInteraccion.recogible;

        fisicas = GetComponentInChildren<Rigidbody>();
        nombre = nombre_actual;
    }

    void FixedUpdate(){
        if (estoy_siendo_sostenido){
            fisicas.angularVelocity = Vector3.zero;
            fisicas.linearVelocity = Vector3.zero;

            transform.position = transform.parent.position;
            transform.rotation = transform.parent.rotation;
        }

        else {
            transform.position = fisicas.transform.position;
            transform.rotation = fisicas.transform.rotation;

            fisicas.transform.position = transform.position;
            fisicas.transform.rotation = transform.rotation;
        }
    }
    
    public void colocar_en(Transform ubicacion){
        transform.SetParent(ubicacion);

        transform.position = ubicacion.position;
        transform.rotation = ubicacion.rotation;

        fisicas.useGravity = false;
        fisicas.detectCollisions = false;

        estoy_siendo_sostenido = true;
        fisicas.isKinematic = true;
    }

    public void soltar() { 
        transform.SetParent(null);

        transform.position = transform.position;
        transform.rotation = transform.rotation;
        estoy_siendo_sostenido = false;
        
        fisicas.useGravity = true;
        fisicas.detectCollisions = true;
        fisicas.isKinematic = false;

    }

    public void arrojar(float fuerza) {
        fisicas.AddForce(transform.parent.forward * fuerza);
        
        soltar();
    }
}
