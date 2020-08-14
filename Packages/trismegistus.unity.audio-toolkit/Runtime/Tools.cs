using System;
using System.Collections.Generic;
using Trismegistus.MathfExtensions;
using UnityEngine;

namespace Trismegistus.AudioToolkit {
	public static class Tools {
		public static float[] CalculateVolumes(IReadOnlyList<Vector3> channels, AudioSource source) {
			var result = new float[channels.Count];
			var currentPos = source.transform.position;

			for (var i = 0; i < channels.Count; i++) {
				var distance = Vector3.Distance(currentPos, channels[i]);
				result[i] = EvaluateRolloff(distance, source);
			}

			return result;
		}

		public static float EvaluateRolloff(float distance, AudioSource source) {
			switch (source.rolloffMode) {
				case AudioRolloffMode.Logarithmic:
					return (source.minDistance / distance).Clamp01();
				case AudioRolloffMode.Linear:
					return (1 - (distance - source.minDistance) /
						(source.maxDistance - source.minDistance)).Clamp01();
				case AudioRolloffMode.Custom:
					var animationCurve = source.GetCustomCurve(AudioSourceCurveType.CustomRolloff);
					return animationCurve.Evaluate(distance / source.maxDistance);
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}