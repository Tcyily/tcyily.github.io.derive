using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[ExecuteInEditMode]
public class MetaBallEntity : MonoBehaviour
{
    public struct MetaBallData
    {
        public const int size = 24;
        public Vector3 position;
        public float radius;
        public float softness;
        public float notUse1;
    }

    public MetaBallRenderer m_renderer = null;
    public float m_radius = 0.125f;
    public float m_softness = 1.0f;
    MetaBallData data;
    // Update is called once per frame
    void Update()
    {
        if(m_renderer != null)
        {
            data.radius = m_radius;
            data.softness = m_softness;
            data.position = transform.position;
            m_renderer.addEntity(data);
        }
        
    }

    private void OnDrawGizmos()
    {
        if (!enabled) return;
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, m_radius);
    }
}
