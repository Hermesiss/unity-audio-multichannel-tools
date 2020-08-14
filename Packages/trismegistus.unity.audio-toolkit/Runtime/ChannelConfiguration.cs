using System;
using UnityEngine;

namespace Trismegistus.AudioToolkit {
	[Serializable, CreateAssetMenu(menuName = "Trismegistus/Audio/ChannelConfiguration")]
	public class ChannelConfiguration : ScriptableObject {
		public ChannelGroup[] groups;
	}
}