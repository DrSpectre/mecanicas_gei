using UnityEngine;

public class RecogibleComponente : MonoBehaviour, InteractuableComportamiento{
    public TipoInteraccion tipo { get; set; }
    public string nombre { get; set; }

    public string nombre_del_objeto; // aqui

    private ControladorGeneral controlador;

    public void arrojar(float fuerza){
        
    }

    public void colocar_en(Transform ubicacion){
        transform.position = new Vector3(0, -10, 0);

        controlador.actualizar(nombre, "1");
    }

    public void soltar(){
        
    }

    void Start(){
        tipo = TipoInteraccion.recogible;

        controlador = GameObject.FindAnyObjectByType<ControladorGeneral>();

        nombre = nombre_del_objeto; // Aui
    }
}
