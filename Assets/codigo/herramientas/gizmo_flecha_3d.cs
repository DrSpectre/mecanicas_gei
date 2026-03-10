using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ContadorObjetosMecanica))]
public class GizmoFlecha3D: Editor{
    //public int tamaño = 1;
    void OnSceneGUI() {
        ContadorObjetosMecanica objeto = target as ContadorObjetosMecanica;


        Handles.color = Handles.xAxisColor;
        
        // Handles.DrawLine(objeto.transform.position, objeto.transform.position + (Vector3.forward * 1));
        // Handles.PositionHandle(objeto.transform.position, objeto.transform.rotation);
    }
}
