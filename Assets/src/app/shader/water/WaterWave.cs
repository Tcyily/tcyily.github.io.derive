using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaterWave : MonoBehaviour
{
    Mesh mesh;
    public float height;
    //尖锐
    [Range(0, 1f)]
    public float sharp = 0.5f;
    [Range(0.5f, 10f)]
    public float speed = 2f;//初相
    [Range(1, 50)]
    public int waveT;//周期
    public Vector2 WaveDir = Vector2.left;

    private Vector3[] baseVertices;
    private void OnEnable()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        baseVertices = mesh.vertices;
    }

    void Update()
    {
        Vector3[] vertices = this.mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertice = this.baseVertices[i];
            float A = this.height;
            float w = (float)(2 * Math.PI / waveT);
            float Qi = sharp / w * A;
            float cosNum = Mathf.Cos(Time.time * speed + w * Vector2.Dot(WaveDir, new Vector2(vertices[i].x, vertices[i].z)));
            float sinNum = Mathf.Sin(Time.time * speed + w * Vector2.Dot(WaveDir, new Vector2(vertices[i].x, vertices[i].z)));

            vertice.x += Qi * A * WaveDir.x * cosNum;
            vertice.z += Qi * A * WaveDir.y * cosNum;
            vertice.y = sinNum * A;

            vertices[i] = vertice;
        }
        this.mesh.vertices = vertices;
        this.mesh.RecalculateNormals();//重新计算法线
    }

}