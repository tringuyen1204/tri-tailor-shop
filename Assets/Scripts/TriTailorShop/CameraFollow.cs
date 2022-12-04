using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    protected GameObject target;

    [SerializeField] 
    protected float threshold;
    
    private Vector3 m_oldPos;
    
    // Update is called once per frame
    void Update()
    {
        var pos = target.transform.position;
        pos.z = -10;

        var m = (pos - m_oldPos).magnitude;
        
        if (m > threshold)
        {
            transform.position = pos;
            m_oldPos = pos;
        }
    }
}
