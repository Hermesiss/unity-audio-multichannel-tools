using System;
using UnityEngine;

namespace Trismegistus.AudioMultichannelTools {
	[Serializable, CreateAssetMenu(menuName = "Trismegistus/Audio/ChannelConfiguration")]
	public class ChannelConfiguration : ScriptableObject {
		public ChannelGroup[] groups;
	}
}