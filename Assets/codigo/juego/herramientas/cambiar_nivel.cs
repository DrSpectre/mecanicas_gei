using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarNivel : MonoBehaviour{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        cambiar_nivel();
    }

    void cambiar_nivel(){
        SceneManager.LoadScene("escena_2");
    }

    void reiniciar_nivel(){
        
    }
        
}
