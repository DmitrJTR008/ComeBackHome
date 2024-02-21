using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RocketVfxController
{
    private ParticleComponentSO VfxGame;
    private RocketController _rocket;
    private ParticleSystem _throotleVfx;

    public RocketVfxController(RocketController rocket,Transform EngineThrootle, ParticleComponentSO GameParticle)
    {
        VfxGame = GameParticle;
        _rocket = rocket;
        _throotleVfx = EngineThrootle.GetChild(0).GetComponent<ParticleSystem>();
        _rocket.OnRocketGameComplete += SingleParticle;
    }

    ~RocketVfxController()
    {
        _rocket.OnRocketGameComplete -= SingleParticle;
    }
    
    public void ThrootleParticle()
    {
        switch (_rocket._currentState)
        {
            case RocketCurrentState.Move:
                if (!_throotleVfx.isPlaying)
                {
                    _throotleVfx.loop= true;
                    _throotleVfx.Play();
                }
                break;
            default:
                if (_throotleVfx.isPlaying)
                {
                    _throotleVfx.loop = false;
                    _throotleVfx.Stop();
                }
                break;
        }
    }

    public void SingleParticle(RocketCurrentState state)
    {
        switch (state)
        {
            case RocketCurrentState.Lose:
                VfxGame.InstanceParticleSingle(VfxGame.LoseParticle, _rocket.transform);
                break;
            case RocketCurrentState.Win:
                VfxGame.InstanceParticleSingle(VfxGame.WinParticle, _rocket.transform);
                break;
        }
    }
    
}
