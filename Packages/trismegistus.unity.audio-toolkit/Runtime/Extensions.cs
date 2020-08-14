using System;
using System.Linq;
using UnityEngine;

namespace Trismegistus.AudioToolkit {
	public static class Extensions {
		public static AudioClip CreateSpeakerSpecificClip(this AudioClip originalClip, int amountOfChannels,
			int targetChannel) {
			var volumes = Enumerable.Repeat<float>(-1, amountOfChannels).ToArray();
			volumes[targetChannel] = 1;
			return CreateSpeakerSpecificClip(originalClip, amountOfChannels, volumes);
		}

		public static AudioClip CreateSpeakerSpecificClip(this AudioClip originalClip, int amountOfChannels,
			float[] volumes) {
			if (volumes.Length != amountOfChannels)
				throw new ArgumentException(
					$"volumes count must be equal to amountOfChannels {amountOfChannels}, but in fact {volumes.Length}");

			if (originalClip == null) throw new NullReferenceException("Passed AudioCLip is null");

			var clip = AudioClip.Create(originalClip.name, originalClip.samples, amountOfChannels,
				originalClip.frequency, false);

			var audioData = new float[originalClip.samples * amountOfChannels];
			var originalAudioData = new float[originalClip.samples * originalClip.channels];
			if (!originalClip.GetData(originalAudioData, 0))
				return null;

			for (var targetChannel = 0; targetChannel < amountOfChannels; targetChannel++) {
				if (volumes[targetChannel] < 0) continue;
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