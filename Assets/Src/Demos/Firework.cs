using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class Firework : MonoBehaviour
{
    [SerializeField]
    private GameObject particlePrefab;

    private Vector3 gravityAcceleration = new Vector3(0, -9.8f, 0);
    private List<GameObject> particles = new List<GameObject>();
    private List<GameObject> subParticles = new List<GameObject>();

    void FixedUpdate()
    {
        particles.ForEach(particle =>
        {
            if (!particle)
                return;

            particle.GetComponent<FireworkParticle>().ElapsedTime += Time.fixedDeltaTime;
            if (particle.GetComponent<FireworkParticle>().ElapsedTime >=
                particle.GetComponent<FireworkParticle>().TimeToLiveSec)
            {
                GenerateSubParticles(particle.transform.position);
                Destroy(particle);
            }

            Integrate(particle);
        });

        subParticles.ForEach(particle =>
        {
            if (!particle)
                return;

            particle.GetComponent<FireworkParticle>().ElapsedTime += Time.fixedDeltaTime;
            if (particle.GetComponent<FireworkParticle>().ElapsedTime >=
                particle.GetComponent<FireworkParticle>().TimeToLiveSec)
                Destroy(particle);

            Integrate(particle);
        });
    }

    private void Integrate(GameObject particle)
    {
        Vector3 velocity = particle.GetComponent<FireworkParticle>().Velocity;

        particle.transform.position += (velocity + (velocity + gravityAcceleration * Time.fixedDeltaTime)) *
            Time.fixedDeltaTime / 2;
        particle.GetComponent<FireworkParticle>().Velocity += gravityAcceleration * Time.fixedDeltaTime;
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.action.phase == InputActionPhase.Canceled)
        {
            GameObject particle = Instantiate(particlePrefab, Vector3.zero, Quaternion.identity);
            particle.GetComponent<FireworkParticle>().Velocity = GetRandomVelocity() * 10;
            particle.GetComponent<FireworkParticle>().TimeToLiveSec = 1.5f;

            particles.Add(particle);
        }
    }

    private Vector3 GetRandomVelocity()
    {
        float thetaRad = Random.Range(0, Mathf.Deg2Rad * 30);
        float phiRad = Random.Range(0, Mathf.PI * 2);

        float x = Mathf.Cos(phiRad) * Mathf.Sin(thetaRad);
        float y = Mathf.Cos(thetaRad);
        float z = Mathf.Sin(thetaRad) * Mathf.Sin(phiRad);

        // Debug.DrawLine(Vector3.zero, new Vector3(x, y, z), Color.magenta, 1000);

        return new Vector3(x, y, z);
    }

    private void GenerateSubParticles(Vector3 pos)
    {
        for (var i = 0; i < 5; i++)
        {
            GameObject particle = Instantiate(particlePrefab, pos, Quaternion.identity);
            particle.GetComponent<FireworkParticle>().Velocity = Random.onUnitSphere * 7;
            particle.GetComponent<FireworkParticle>().TimeToLiveSec = 1.5f;

            subParticles.Add(particle);
        }
    }
}