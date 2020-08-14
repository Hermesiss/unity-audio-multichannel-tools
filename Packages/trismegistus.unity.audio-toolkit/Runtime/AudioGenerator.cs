using System;
using UnityEngine;

namespace Trismegistus.AudioToolkit {
	[RequireComponent(typeof(AudioSource))]
	public class AudioGenerator : MonoBehaviour {
		[SerializeField] private AudioSource audioSource;
		[SerializeField] private AudioClip originalSound;

		public AudioSource AudioSource => audioSource;

		private void OnEnable() {
			if (!originalSound) {
				originalSound = audioSource.clip;
			}
		}


		private void Reset() {
			audioSource = GetComponent<AudioSource>();
		}

		public void UpdateSound(Vector3[] channels) =>
			audioSource.clip =
				originalSound.CreateSpeakerSpecificClip(channels.Length,
					Tools.CalculateVolumes(channels, audioSource));

		public void UpdateSound(float[] volumes) =>
			audioSource.clip = originalSound.CreateSpeakerSpecificClip(volumes.Length, volumes);

		public void Play() => audioSource.Play();

		public void Stop() => audioSource.Stop();
	}
}