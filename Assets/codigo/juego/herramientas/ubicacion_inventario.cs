using System;
using UnityEditor.EditorTools;
using UnityEngine;

public enum NombreUbicacion{
    cabeza,
    mano_derecha,
    mano_izquierda
}

public class UbicacionInventario: MonoBehaviour{
    public NombreUbicacion lugar = NombreUbicacion.mano_derecha;
    public float fuerza_para_arrojar = 250.0f;

    public bool ocupada = false;

    public InteractuableComportamiento objeto_agarrado;

    public void interaccion(InteractuableComportamiento objeto){
        if (ocupada){
            soltar();
        }
        else{
            agarrar(objeto);
        }
    }

    public void usar(){
        if (ocupada){
            objeto_agarrado.accion();
        }
    }
    public bool agarrar(InteractuableComportamiento objeto){
        if(ocupada || objeto == null){
            return false;
        }

        objeto.colocar_en(gameObject.transform);
        objeto_agarrado = objeto;

        ocupada = true;

        return true;
    }

    public void soltar(){
        if(ocupada){
            objeto_agarrado.arrojar(fuerza_para_arrojar);
            objeto_agarrado = null;
            
            ocupada = false;
        }
    }
}
