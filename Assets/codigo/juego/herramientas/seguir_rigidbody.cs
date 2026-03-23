using UnityEngine;

public enum EstadosSeguirRigidbody { 
    siguiendo,
    desactivado
}

public class SeguirRigidbody: MonoBehaviour{
    private Rigidbody rigid_body_hijo;

    public EstadosSeguirRigidbody estado = EstadosSeguirRigidbody.siguiendo;
    void Start(){
        rigid_body_hijo = GetComponentInChildren<Rigidbody>();
    }

    void FixedUpdate() {
        switch (estado) {
            case EstadosSeguirRigidbody.siguiendo:
                gameObject.transform.position = rigid_body_hijo.transform.position;
                rigid_body_hijo.transform.position = transform.position;

                transform.rotation = rigid_body_hijo.transform.rotation;
                rigid_body_hijo.transform.rotation = transform.rotation;
                break;

            case EstadosSeguirRigidbody.desactivado:
                rigid_body_hijo.angularVelocity = Vector3.zero;
                rigid_body_hijo.linearVelocity = Vector3.zero;
                break;
        }
    }

    public void desactviar(){
        rigid_body_hijo.useGravity = false;
        rigid_body_hijo.isKinematic = true;
        rigid_body_hijo.detectCollisions = false;

        rigid_body_hijo.position = transform.position;
        
        estado = EstadosSeguirRigidbody.desactivado;
    }

    public void activar() { 
        rigid_body_hijo.useGravity = true;
        rigid_body_hijo.isKinematic = false;
        rigid_body_hijo.detectCollisions = true;

        estado = EstadosSeguirRigidbody.siguiendo;
    }

    public void agregar_fuerza(float fuerza) { 
         rigid_body_hijo.AddForce(transform.parent.forward * fuerza);
    }
}
