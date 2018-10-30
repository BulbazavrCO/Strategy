using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour {

    public GameObject m_fogOfWarPlane;
    public Transform m_player;
    public LayerMask m_fogLayer;
    public float m_radius = 5f;
    private float m_radiusSqr { get { return m_radius * m_radius; } }

    private Mesh m_mesh;
    private Vector3[] m_vertices;
    public Color[] m_colors;
    public bool yes;

    // Use this for initialization
    void Start()
    {
        
        if (yes)
        {
            Initialize();
            MinusFog();
            yes = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (yes)
        {
            Initialize();
            MinusFog();
            yes = false;
        }
    }

    void MinusFog()
    {
        Ray r = new Ray(transform.position, new Vector3(transform.position.x, 0, transform.position.z) - transform.position);
        RaycastHit hit;
        if (Physics.Raycast(r, out hit, 1000, m_fogLayer, QueryTriggerInteraction.Collide))
        {

            for (int i = 0; i < m_vertices.Length; i++)
            {
                Vector3 v = m_fogOfWarPlane.transform.TransformPoint(m_vertices[i]);

                float dist = Vector3.SqrMagnitude(v - hit.point);
                if (dist < m_radiusSqr)
                {
                    float alpha = Mathf.Min(m_colors[i].a, dist / m_radiusSqr);
                    if (m_colors[i].a > alpha)
                    {
                        m_colors[i].a = alpha;
                    }
                }
            }
            UpdateColor();
        }
    }

    void Initialize()
    {
        m_mesh = m_fogOfWarPlane.GetComponent<MeshFilter>().mesh;
        m_vertices = m_mesh.vertices;
        m_colors = m_mesh.colors;
        if (m_colors.Length < 1)
        {
            m_colors = new Color[m_vertices.Length];
            for (int i = 0; i < m_colors.Length; i++)
            {
                m_colors[i] = Color.black;
            }
        }
      // UpdateColor();
    }

    void UpdateColor()
    {
        m_mesh.colors = m_colors;
    }
}
