using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor (typeof(BaseEnemy),true)]
public class BaseEnemyEditor : Editor {

    private void OnSceneGUI()
    {
        BaseEnemy baseEnemy = (BaseEnemy)target;
        Handles.color = Color.red;
        Handles.DrawWireArc(baseEnemy.transform.position, Vector3.forward, Vector3.up, 360, baseEnemy.agroRadius);
    }
}
