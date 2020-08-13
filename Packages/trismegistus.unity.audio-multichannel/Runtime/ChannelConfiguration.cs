using System;
using UnityEngine;

namespace AudioMultichannelTools {
	[Serializable, CreateAssetMenu(menuName = "Trismegistus/Audio/ChannelConfiguration")]
	public class ChannelConfiguration : ScriptableObject {
		public ChannelGroup[] groups;
	}
}