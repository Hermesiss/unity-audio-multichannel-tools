using System.Linq;
using UnityEngine;

namespace Trismegistus.AudioMultichannelTools {
	public class SpeakerReference : MonoBehaviour{
		public ChannelConfiguration channelConfiguration;

		private Transform[] _speakers;

		public Transform[] Speakers {
			get => _speakers ?? (_speakers = new Transform[18]);
			set => _speakers = value;
		}

		public Vector3[] GetPositions() {
			return _speakers.Where(x => x != null).Select(x => x.position).ToArray();
		}
	}
}