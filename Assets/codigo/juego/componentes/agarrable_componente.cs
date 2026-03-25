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


    public delegate void siendo_observado(bool si_lo_estoy);
    public event siendo_observado saber_si_soy_observado;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        tipo = TipoInteraccion.recogible;

        nombre = nombre_actual;

        fisicas = GetComponent<SeguirRigidbody>();
    }

    public void marcar_como_observado(){
        saber_si_soy_observado?.Invoke(true);
    }

    public void desamrcar_como_obervado() { 
         saber_si_soy_observado?.Invoke(false);
    }
    public void colocar_en(Transform ubicacion){
        fisicas?.desactviar();
        
        transform.SetParent(ubicacion);
    }

    public void soltar() {
        fisicas?.activar();
        
        transform.SetParent(null);

        transform.position = transform.position;
        transform.rotation = transform.rotation;
        estoy_siendo_sostenido = false;

    }

    public void arrojar(float fuerza) {
        fisicas.agregar_fuerza(fuerza);
        
        soltar();
    }
}
