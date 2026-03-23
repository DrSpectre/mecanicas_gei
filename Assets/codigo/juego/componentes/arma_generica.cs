using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;

public abstract class ArmaComponente: MonoBehaviour{
    public int daño = 100;
    protected List<GameObject> a_quienes_dañar;
    void Start(){
        a_quienes_dañar = new List<GameObject>();
    }

    // Update is called once per frame
    abstract public void dañar();
    
}
