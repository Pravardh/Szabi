using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    [SerializeField]
    private AudioSource playerAudioSource;

    [SerializeField]
    private AudioClip playerHurtSound;


    public void PlaySound()
    {

    
        playerAudioSource.PlayOneShot(playerHurtSound);
    
    }



}
