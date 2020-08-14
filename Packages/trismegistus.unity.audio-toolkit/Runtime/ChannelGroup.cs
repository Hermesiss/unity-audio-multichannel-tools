using System;
using UnityEngine;

namespace Trismegistus.AudioToolkit {
    
    [Serializable, CreateAssetMenu(menuName = "Trismegistus/Audio/ChannelGroup")]
    public class ChannelGroup : ScriptableObject {
        public Channel[] channels;
    }
}
