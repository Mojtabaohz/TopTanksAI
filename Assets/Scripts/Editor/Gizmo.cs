using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Gizmo : MonoBehaviour
{
    [DrawGizmo(GizmoType.NonSelected)]
    static void DrawSelectedViewRange(GameObject obj,GizmoType gizmoType){
        
        if(obj.GetComponent<TanksAttr>()){
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(obj.transform.position,obj.GetComponent<TanksAttr>().viewRange);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(obj.transform.position,obj.GetComponent<TanksAttr>().fireRange);
        }
        

    }

    
    
}
