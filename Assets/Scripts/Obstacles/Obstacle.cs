using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script is the base class for implemented obstacles.
/// Derived classes should take care of spawning any object needed for the obstacles.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public abstract class Obstacle : MonoBehaviour
{
	public AudioClip impactedSound;
    public virtual void Setup() {}

    [Header("Damage")]
    public int DistanceDivider;
    public int DamageValue;
    public TextMeshProUGUI DamageValueText;
    
    public abstract IEnumerator Spawn(TrackSegment segment, float t, float worldDistance);

	public virtual void Impacted()
	{
		Animation anim = GetComponentInChildren<Animation>();
		AudioSource audioSource = GetComponent<AudioSource>();

		if (anim != null)
		{
			anim.Play();
		}

		if (audioSource != null && impactedSound != null)
		{
			audioSource.Stop();
			audioSource.loop = false;
			audioSource.clip = impactedSound;
			audioSource.Play();
		}
	}
	
}
