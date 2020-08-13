using System;
using UnityEngine;

namespace AudioMultichannelTools {
	public static class Extensions {
		public static AudioClip CreateSpeakerSpecificClip(this AudioClip originalClip, int amountOfChannels,
			int targetChannel) {
			// Create a new clip with the target amount of channels.
			AudioClip clip = AudioClip.Create(originalClip.name, originalClip.samples, amountOfChannels,
				originalClip.frequency, false);
			// Init audio arrays.
			float[] audioData = new float[originalClip.samples * amountOfChannels];
			float[] originalAudioData = new float[originalClip.samples * originalClip.channels];
			if (!originalClip.GetData(originalAudioData, 0))
				return null;
			// Fill in the audio from the original clip into the target channel. Samples are interleaved by channel (L0, R0, L1, R1, etc).
			int originalClipIndex = 0;
			for (int i = targetChannel; i < audioData.Length; i += amountOfChannels) {
				audioData[i] = originalAudioData[originalClipIndex];
				originalClipIndex += originalClip.channels;
			}

			if (!clip.SetData(audioData, 0))
				return null;
			return clip;
		}

		public static AudioClip CreateSpeakerSpecificClip(this AudioClip originalClip, int amountOfChannels,
			float[] volumes) {
			if (volumes.Length != amountOfChannels)
				throw new ArgumentException(
					$"volumes count must be equal to amountOfChannels {amountOfChannels}, but in fact {volumes.Length}");
			// Create a new clip with the target amount of channels.
			AudioClip clip = AudioClip.Create(originalClip.name, originalClip.samples, amountOfChannels,
				originalClip.frequency, false);
			// Init audio arrays.
			float[] audioData = new float[originalClip.samples * amountOfChannels];
			float[] originalAudioData = new float[originalClip.samples * originalClip.channels];
			if (!originalClip.GetData(originalAudioData, 0))
				return null;
			// Fill in the audio from the original clip into the target channel. Samples are interleaved by channel (L0, R0, L1, R1, etc).
		
			for (int targetChannel = 0; targetChannel < amountOfChannels; targetChannel++) {
				int originalClipIndex = 0;
				for (int i = targetChannel; i < audioData.Length; i += amountOfChannels) {
					audioData[i] = originalAudioData[originalClipIndex]*volumes[targetChannel];
					originalClipIndex += originalClip.channels;
				}
			}


			if (!clip.SetData(audioData, 0))
				return null;
			return clip;
		}
	}
}