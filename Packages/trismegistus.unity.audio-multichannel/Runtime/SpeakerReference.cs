using System.Linq;
using UnityEngine;

namespace Trismegistus.AudioMultichannelTools {
	public class SpeakerReference : MonoBehaviour{
		public ChannelConfiguration channelConfiguration;

		[SerializeField]
		private Transform[] speakers;

		public Transform[] Speakers {
			get => speakers ?? (speakers = new Transform[18]);
			set => speakers = value;
		}

		public Vector3[] GetPositions() {
			return speakers.Where(x => x != null).Select(x => x.position).ToArray();
		}
	}
}