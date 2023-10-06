using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleGravity : MonoBehaviour
{
    private ParticleSystem particleSystem;
    private ParticleSystem.Particle[] particles;

    public float gravityMultiplier = 1.0f; // Adjust this to control the strength of gravity along the Z-axis.

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];
    }

    void Update()
    {
        Invoke("fallOnZaxis",1f);
    }

    private void fallOnZaxis()
    {
        int numParticlesAlive = particleSystem.GetParticles(particles);

        for (int i = 0; i < numParticlesAlive; i++)
        {
            // Apply gravity along the Z-axis (forward direction).
            particles[i].velocity = new Vector3(particles[i].velocity.z * gravityMultiplier * Time.deltaTime, 0,0);
            particles[i].velocity += Vector3.forward * Physics.gravity.z;
        }

        particleSystem.SetParticles(particles, numParticlesAlive);
    }
}
