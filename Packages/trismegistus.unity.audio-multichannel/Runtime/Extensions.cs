using System;
using UnityEngine;

namespace Trismegistus.AudioMultichannelTools {
	public static class Extensions {
		public static AudioClip CreateSpeakerSpecificClip(this AudioClip originalClip, int amountOfChannels,
			int targetChannel) {
			// Create a new clip with the target amount of channels.
			var clip = AudioClip.Create(originalClip.name, originalClip.samples, amountOfChannels,
				originalClip.frequency, false);
			// Init audio arrays.
			var audioData = new float[originalClip.samples * amountOfChannels];
			var originalAudioData = new float[originalClip.samples * originalClip.channels];
			if (!originalClip.GetData(originalAudioData, 0))
				return null;
			// Fill in the audio from the original clip into the target channel. Samples are interleaved by channel (L0, R0, L1, R1, etc).
			var originalClipIndex = 0;
			for (var i = targetChannel; i < audioData.Length; i += amountOfChannels) {
				audioData[i] = originalAudioData[originalClipIndex];
				originalClipIndex += originalClip.channels;
			}

			return !clip.SetData(audioData, 0) ? null : clip;
		}

		public static AudioClip CreateSpeakerSpecificClip(this AudioClip originalClip, int amountOfChannels,
			float[] volumes) {
			if (volumes.Length != amountOfChannels)
				throw new ArgumentException(
					$"volumes count must be equal to amountOfChannels {amountOfChannels}, but in fact {volumes.Length}");
			// Create a new clip with the target amount of channels.
			var clip = AudioClip.Create(originalClip.name, originalClip.samples, amountOfChannels,
				originalClip.frequency, false);
			// Init audio arrays.
			var audioData = new float[originalClip.samples * amountOfChannels];
			var originalAudioData = new float[originalClip.samples * originalClip.channels];
			if (!originalClip.GetData(originalAudioData, 0))
				return null;
			// Fill in the audio from the original clip into the target channel. Samples are interleaved by channel (L0, R0, L1, R1, etc).

			for (var targetChannel = 0; targetChannel < amountOfChannels; targetChannel++) {
				var originalClipIndex = 0;
				for (var i = targetChannel; i < audioData.Length; i += amountOfChannels) {
					audioData[i] = originalAudioData[originalClipIndex] * volumes[targetChannel];
					originalClipIndex += originalClip.channels;
				}
			}


			return !clip.SetData(audioData, 0) ? null : clip;
		}
	}
}