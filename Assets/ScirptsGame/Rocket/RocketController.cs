using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public enum RocketCurrentState
{
    Win,
    Move,
    Stay,
    Drop,
    Lose
};

[RequireComponent(typeof(AudioSource))]
public class RocketController : MonoBehaviour
{
    public GameObject BODY;
    private RocketStatsData     _rocketStats;
    private RocketMoveComponent _rocketController;
    private RocketVfxController _rocketVfxController;
    public  AudioComponent _rocketAudioComponent;
    public RocketCurrentState _currentState;
    public Action<RocketCurrentState> OnRocketGameComplete;
    
    #region COMPONENTS
    private Rigidbody _rb;
    [SerializeField] private Transform RocketThrootleEngine;
    [SerializeField] private ParticleComponentSO Particles;
    #endregion

    #region HANDLER
    [SerializeField]private Joystick moveJoy, rotateJoy;
    private bool canMove = true;
    #endregion

    private void Awake()
    {
        _rb                   = GetComponent<Rigidbody>();
        _rocketStats          = new RocketStatsData(1500, 35);
        _rocketController     = new RocketMoveComponent(this,moveJoy,rotateJoy,_rb);
        _rocketVfxController  = new RocketVfxController(this,RocketThrootleEngine , Particles);
        _rocketAudioComponent = new AudioComponent(this, GetComponent<AudioSource>(),Resources.Load<GameAudioHolderSO>("AudioHolderSO"));

    }

    public void SetSkit(Material skin)
    {
        BODY.GetComponent<MeshRenderer>().material = skin;
    }

    void FixedUpdate()
    {
        if (_currentState == RocketCurrentState.Lose || _currentState == RocketCurrentState.Win) return;
        _rocketController.MoveRocket(_rocketStats.Force,_rocketStats.MaxVelocity);
        _rocketController.RotateRocket();
        _rocketVfxController.ThrootleParticle();
        _rocketAudioComponent.RocketSoundHandle();
        
    }
    

    void OnTriggerEnter(Collider other)
    {
        string tag = other.tag;
        
        if ((tag.Equals("obstacle") || tag.Equals(("Finish")))  && _currentState != RocketCurrentState.Lose)
        {
            canMove = false;

            switch (tag)
            {
                case "obstacle":
                    DestroyRocket();
                    break;
                
                case "Finish":
                    canMove = false;
                    _currentState = RocketCurrentState.Win;
                    break;
                
            }
            if(Vector3.Angle(Vector3.up, transform.up) > 40f)
                DestroyRocket();
            OnRocketGameComplete?.Invoke(_currentState);
        }
    }

    void DestroyRocket()
    {
        _rb.isKinematic = true;
        for(int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            Transform child = transform.GetChild(0).GetChild(i);
            child.AddComponent<Rigidbody>();
        }
        _currentState = RocketCurrentState.Lose;
    }

}
