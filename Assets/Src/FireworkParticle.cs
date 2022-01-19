using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class FireworkParticle : MonoBehaviour
{
    private Vector3 velocity;
    private float mass = 1;
    private Mesh mesh;

    private float timeToLiveSec;
    private float elapsedTime;

    void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    void Update()
    {
        mesh.Clear();

        Vector3[] vertices = new Vector3[4];
        vertices[0] = new Vector3(-0.1f, -0.1f);
        vertices[1] = new Vector3(-0.1f, 0.1f);
        vertices[2] = new Vector3(0.1f, 0.1f);
        vertices[3] = new Vector3(0.1f, -0.1f);

        mesh.vertices = vertices;
        mesh.triangles = new[]
        {
            0, 1, 3,
            1, 2, 3
        };
        mesh.normals = new[]
        {
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward
        };
    }

    public Vector3 Velocity
    {
        get => velocity;
        set => velocity = value;
    }

    public float TimeToLiveSec
    {
        get => timeToLiveSec;
        set => timeToLiveSec = value;
    }

    public float ElapsedTime
    {
        get => elapsedTime;
        set => elapsedTime = value;
    }
}