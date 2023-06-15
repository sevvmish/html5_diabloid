using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    //SFX=============================
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip swing1hWeaponEasy;
    [SerializeField] private AudioClip swing1hWeaponMedium;
    [SerializeField] private AudioClip swing1hWeaponHuge;

    [SerializeField] private AudioClip meleeImpactMedium1;
    [SerializeField] private AudioClip meleeImpactMedium2;
    [SerializeField] private AudioClip meleeImpactMedium3;
    [SerializeField] private AudioClip meleeImpactMediumMoreMetal1;
    [SerializeField] private AudioClip meleeImpactMediumMoreMetal2;
    [SerializeField] private AudioClip meleeImpactMediumMoreBlunt;

    private AudioClip clip;

    //VFX============================

    public void PlayRandomMeleeImpactMedium()
    {
        int[] rnd = new int[] { 3, 4, 5 };
        PlaySound((SoundsType)rnd[UnityEngine.Random.Range(0, 3)]);
    }

    public void PlaySound(SoundsType soundsType)
    {       
        switch(soundsType)
        {
            case SoundsType.swing1H_easy:
                clip = swing1hWeaponEasy;
                break;
            case SoundsType.swing1H_medium:
                clip = swing1hWeaponMedium;
                break;
            case SoundsType.swing1H_huge:
                clip = swing1hWeaponHuge;
                break;
            
            case SoundsType.meleeImpactMedium1:
                clip = meleeImpactMedium1;
                break;
            case SoundsType.meleeImpactMedium2:
                clip = meleeImpactMedium2;
                break;
            case SoundsType.meleeImpactMedium3:
                clip = meleeImpactMedium3;
                break;
            case SoundsType.meleeImpactMediumMoreMetal1:
                clip = meleeImpactMediumMoreMetal1;
                break;
            case SoundsType.meleeImpactMediumMoreMetal2:
                clip = meleeImpactMediumMoreMetal2;
                break;
            case SoundsType.meleeImpactMediumMoreBlunt:
                clip = meleeImpactMediumMoreBlunt;
                break;
        }

        if (audioSource.isPlaying) audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }


}

public enum SoundsType
{
    swing1H_easy,
    swing1H_medium,
    swing1H_huge,
    meleeImpactMedium1,
    meleeImpactMedium2,
    meleeImpactMedium3,
    meleeImpactMediumMoreMetal1,
    meleeImpactMediumMoreMetal2,
    meleeImpactMediumMoreBlunt
}
