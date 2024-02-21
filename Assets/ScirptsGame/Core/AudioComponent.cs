using UnityEngine;

public class AudioComponent
{
    private AudioSource _as;
    private RocketController _rocket;
    private GameAudioHolderSO _gameAudio;
    public AudioComponent(RocketController rocket, AudioSource audioSource, GameAudioHolderSO audioList)
    {
        _rocket = rocket;
        _as = audioSource;
        _gameAudio = audioList;
        _as.loop = false;
        _as.playOnAwake = false;

        _rocket.OnRocketGameComplete += SingleShoot;
    }

    ~AudioComponent()
    {
        _rocket.OnRocketGameComplete -= SingleShoot;
    }

    public void SetVolume(float value)
    {
        _as.volume = value;
    }

    public void RocketSoundHandle()
    {
        
        switch (_rocket._currentState)
        {
            case RocketCurrentState.Move:
                EngineThrootle(_gameAudio.EngineThrootleSFX);
                break;
            default:
                _as.Stop();
                break;
        }
    }
    
    public void EngineThrootle(AudioClip clip)
    {
        if (_as.isPlaying) return;
            _as.PlayOneShot(clip);
    }

    private void SingleShoot(RocketCurrentState state)
    {
        switch (state)
        {
            case RocketCurrentState.Win:
                _as.PlayOneShot(_gameAudio.WinSFX);
                break;
            case RocketCurrentState.Lose:
                _as.PlayOneShot(_gameAudio.ExploseSFX);
                break;
        }
    }
}
