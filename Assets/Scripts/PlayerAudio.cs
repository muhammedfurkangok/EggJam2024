using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    PlayerController2D pc;
    //PlayerHealth ph;

    [SerializeField] private AudioSource footstepAudio;
    [SerializeField] private AudioSource deathAudio;
    [SerializeField] private AudioSource impactAudio;

    [SerializeField] private AudioClip[] footstepClips;
    [SerializeField] private AudioClip[] deathClips;
    [SerializeField] private AudioClip[] impactClips;

    private void Start()
    {
        pc = GetComponent<PlayerController2D>();
        //ph = GetComponent<PlayerHealth>();
        //ph.OnPlayerDeath += PlayDeathAudio;
        //ph.OnPlayerHit += PlayImpactAudio;
        pc.GroundedChanged += PlayFootstepAudio;
        pc.Jumped += PlayFootstepAudio;
    }

    void PlayDeathAudio()
    {
        deathAudio.clip = deathClips[UnityEngine.Random.Range(0, deathClips.Length)];
        deathAudio.Play();
    }
    void PlayFootstepAudio(bool isAirJump)
    {
        if (isAirJump) return;
        footstepAudio.clip = footstepClips[UnityEngine.Random.Range(0, footstepClips.Length)];
        footstepAudio.Play();
    }
    void PlayFootstepAudio(bool grounded, float impact)
    {
        if (impact < -1) return; // impact is positive always
        footstepAudio.clip = footstepClips[UnityEngine.Random.Range(0, footstepClips.Length)];
        footstepAudio.Play();
    }
    void PlayImpactAudio()
    {
        impactAudio.clip = impactClips[UnityEngine.Random.Range(0, impactClips.Length)];
        impactAudio.Play();
    }

    private void Awake()
    {
        
    }

}