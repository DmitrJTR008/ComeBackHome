using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameParticleComponent", menuName = "SO/GameVFXSO")]
public class ParticleComponentSO : ScriptableObject
{
    public ParticleSystem SalutParticle;
    public ParticleSystem FireExploseParticle;
    public ParticleSystem WinParticle;
    public ParticleSystem LoseParticle;
    public ParticleSystem RocketThrootleParticle;
    public void InstanceParticleSingle(ParticleSystem type, Transform point)
    {
        Instantiate(type, point.position,Quaternion.identity);
    }
}
