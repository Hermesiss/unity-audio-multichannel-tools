using System;
using System.Collections;
using System.Collections.Generic;
using Trismegistus.AudioMultichannelTools;
using Trismegistus.MathfExtensions;
using UnityEngine;

namespace Trismegistus.AudioMultichannelTools {
	[RequireComponent(typeof(AudioSource))]
	public class AudioGenerator : MonoBehaviour {
		[SerializeField] private AudioSource audioSource;

		private void Reset() {
			audioSource = GetComponent<AudioSource>();
		}

		public void UpdateSound(Vector3[] channels) {
			var clip = audioSource.clip;
			var newClip = clip.CreateSpeakerSpecificClip(4, CalculateVolumes(channels));
			audioSource.clip = newClip;
		}

		private float[] CalculateVolumes(Vector3[] channels) {
			var rolloffMode = audioSource.rolloffMode;
			var result = new float[channels.Length];
			var currentPos = transform.position;
			for (int i = 0; i < channels.Length; i++) {
				var distance = Vector3.Distance(currentPos, channels[i]);
				float volume;
				switch (rolloffMode) {
					case AudioRolloffMode.Logarithmic:
						volume = (audioSource.minDistance / distance).Clamp01();
						break;
					case AudioRolloffMode.Linear:
						volume = (1 - (distance - audioSource.minDistance) /
							(audioSource.maxDistance - audioSource.minDistance)).Clamp01();

						break;
					case AudioRolloffMode.Custom:
						var animationCurve = audioSource.GetCustomCurve(AudioSourceCurveType.CustomRolloff);
						volume = animationCurve.Evaluate(distance / audioSource.maxDistance);
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}

				result[i] = volume;
			}

			return result;
		}

		public void Play() => audioSource.Play();

		public void Stop() => audioSource.Stop();
	}
}