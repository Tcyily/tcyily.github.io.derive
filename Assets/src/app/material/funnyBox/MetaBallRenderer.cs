using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaBallRenderer : MonoBehaviour
{
    private MetaBallEntity.MetaBallData[] m_entities;
    private int m_entities_cnt = 0;
    private const int m_entitis_limit = 32;

    private Material m_material;
    private ComputeBuffer m_buffer;

    public void Start()
    {
        m_entities = new MetaBallEntity.MetaBallData[m_entitis_limit];
        m_material = transform.GetComponent<Renderer>().sharedMaterial;
        m_buffer = new ComputeBuffer(m_entitis_limit, MetaBallEntity.MetaBallData.size);
    }

    public void addEntity(MetaBallEntity.MetaBallData data)
    {
        if (m_entities_cnt >= m_entities_cnt) return;
        m_entities[m_entities_cnt] = data;
        m_entities_cnt++;
    }
    // Update is called once per frame
    private void LateUpdate()
    {
        m_buffer.SetData(m_entities);
        m_material.SetBuffer("_EntityData", m_buffer);
        m_material.SetInt("_NumLimit", m_entitis_limit);
        m_entities_cnt = 0;
    }
    private void OnDestroy()
    {
        m_buffer.Release();
        m_buffer = null;
    }
}
