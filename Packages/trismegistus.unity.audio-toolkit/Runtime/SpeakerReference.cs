using System.Linq;
using UnityEngine;

namespace Trismegistus.AudioToolkit {
	
	public class SpeakerReference : MonoBehaviour {
		public ChannelConfiguration channelConfiguration;

		[SerializeField] private Transform[] speakers;

		public Transform[] Speakers {
			get => speakers ?? (speakers = new Transform[18]);
			set => speakers = value;
		}

		public Vector3[] GetPositions() {
			return GetActiveSpeakers().Select(x => x.position)
				.ToArray();
		}

		public Transform[] GetActiveSpeakers() {
			var channels = channelConfiguration.groups.SelectMany(x => x.channels).Select(x => x.index);
			return speakers.Where((x, index) => channels.Contains(index)).ToArray();
		}
	}
}