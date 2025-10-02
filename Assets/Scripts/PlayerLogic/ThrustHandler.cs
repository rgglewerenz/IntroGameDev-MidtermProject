using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrustHandler : MonoBehaviour
{

    public ParticleSystem thrustParticles;

    private void Start()
    {
    }


    public void Emit() {
        if(!thrustParticles.isStopped) return;
        thrustParticles.Play();
    }


    public void StopEmitting()
    {
        thrustParticles.Stop();
    }
}
