using System;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    private List<ForceGenerator> forces = new List<ForceGenerator>();

    private Vector3 velocity;
    private float mass;
    private Vector3 netForce;

    private Mesh mesh;
    private int frameCount;

    void Awake()
    {
        // Time.timeScale = 0.1f;
        Time.fixedDeltaTime = 0.02f;

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    protected virtual void FixedUpdate()
    {
        frameCount++;
        
        Grapher.Log(transform.position, "pos");
        Grapher.Log(velocity, "vel");

        forces.ForEach(force => force.GetImpact(this));
        // Debug.Log($"net force: {netForce.ToString("F5")}");

        Vector3 accel = netForce / mass;
        // Debug.Log($"net accel: {netAccel.ToString("F5")}");

        // Change in position calculated through average velocity
        /*Vector3 instantVel = velocity + acceleration * Time.fixedDeltaTime;
        Vector3 avgVel = (velocity + instantVel) / 2;
        position += avgVel * Time.fixedDeltaTime;*/

        // Change in position calculated through trapezoid square (is mathematically equivalent the previous approach)
        Vector3 oldPos = transform.position; // TODO just for debug
        transform.position += (velocity + (velocity + accel * Time.fixedDeltaTime)) * Time.fixedDeltaTime / 2;
        
        // Change in position calculated geometrically (see my notes)
        /*Vector3 capSquare = acceleration * Time.fixedDeltaTime * Time.fixedDeltaTime / 2;
        Vector3 restSquare = velocity * Time.fixedDeltaTime;
        position += capSquare + restSquare;*/

        // Debug.Log($"prev vel: {velocity.ToString("F5")}");

        Vector3 oldVel = velocity; // TODO just for debug
        velocity += accel * Time.fixedDeltaTime * 0.5f;
        // Debug.Log($"new vel: {velocity.ToString("F5")}");
        
        Debug.Log($"elapsed_time: {frameCount * Time.fixedDeltaTime}\n" +
                  $"old_vel: {oldVel.ToString("F5")}\n" +
                  $"old_pos: {oldPos.ToString("F5")}\n" +
                  $"net_force: {netForce.ToString("F5")}\n" +
                  $"accel: {accel.ToString("F5")}\n" +
                  $"new_vel: {velocity.ToString("F5")}\n" +
                  $"new_pos: {transform.position.ToString("F5")}");

        netForce = Vector3.zero;
    }

    void Update()
    {
        if (transform.position.y < -1000)
            Destroy(gameObject);

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

    public void ApplyForce(Vector3 force)
    {
        netForce += force;
    }

    public void AddForceGenerator(ForceGenerator forceGenerator)
    {
        forces.Add(forceGenerator);
    }

    public Vector3 Velocity
    {
        get => velocity;
        set => velocity = value;
    }

    public float Mass
    {
        get => mass;
        set => mass = value;
    }
}