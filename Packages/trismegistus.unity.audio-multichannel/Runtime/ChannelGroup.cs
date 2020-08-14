using System;
using UnityEngine;

namespace Trismegistus.AudioMultichannelTools {
    
    [Serializable, CreateAssetMenu(menuName = "Trismegistus/Audio/ChannelGroup")]
    public class ChannelGroup : ScriptableObject {
        public Channel[] channels;
    }
}
