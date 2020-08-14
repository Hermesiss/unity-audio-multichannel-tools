using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace Trismegistus.AudioMultichannelTools.Editor {
	public class ChannelEditor : VisualElement {
		public ChannelEditor(Channel channel, Transform transform) {
			var visualTree = Resources.Load<VisualTreeAsset>("SpeakerReferenceEditor_Channel");
			visualTree.CloneTree(this);

			var s = Resources.Load<StyleSheet>("SpeakerReferenceEditor_Channel_Style");
			styleSheets.Add(s);

			this.Query<Label>("index").First().text = channel.index.ToString();

			var caption =
				$"{(channel.hexFlag == 0 ? "" : $"0x{channel.hexFlag:00000000}")}    {$"{channel.id}      ".Substring(0, 3)}   {channel.identifier}";

			var foldout = this.Query<Foldout>("foldout").First();
			foldout.text = caption;
			foldout.value = !string.IsNullOrWhiteSpace(channel.id);

			var speakerTransform = this.Query<ObjectField>("transform").First();
			speakerTransform.objectType = typeof(Transform);
			speakerTransform.value = transform;
			speakerTransform.RegisterValueChangedCallback(evt => { OnValueChanged?.Invoke((Transform) evt.newValue); });
		}

		public event Action<Transform> OnValueChanged;
	}

	[CustomEditor(typeof(SpeakerReference))]
	public class SpeakerReferenceEditor : UnityEditor.Editor {
		private SpeakerReference _speakerRef;
		private VisualElement _rootElement;

		List<ChannelGroup> _speakers;
		VisualElement _speakerRoot;

		private void OnEnable() {
			_speakerRef = (SpeakerReference) target;
			_rootElement = new VisualElement();
			var visualTree =
				Resources.Load<VisualTreeAsset>(
					$"{nameof(SpeakerReferenceEditor)}_Main");
			visualTree.CloneTree(_rootElement);

			var style = Resources.Load<StyleSheet>(
				$"{nameof(SpeakerReferenceEditor)}_Style");
			_rootElement.styleSheets.Add(style);
		}

		public override VisualElement CreateInspectorGUI() {
			var configField = _rootElement.Query<ObjectField>("ConfigSelectorField").First();
			configField.objectType = typeof(ChannelConfiguration);
			configField.value = _speakerRef.channelConfiguration;
			configField.RegisterValueChangedCallback(evt => {
				_speakerRef.channelConfiguration = (ChannelConfiguration) evt.newValue;
				UpdateSpeakers();
				EditorUtility.SetDirty(_speakerRef);
			});

			_speakerRoot = _rootElement.Query<VisualElement>("SpeakerRoot").First();

			UpdateSpeakers();

			return _rootElement;
		}

		private void UpdateSpeakers() {
			_speakerRoot.Clear();
			if (_speakerRef.channelConfiguration == null) return;
			_speakers = _speakerRef.channelConfiguration.groups.ToList();

			var channels = _speakers.SelectMany(channelGroup => channelGroup.channels).ToList();

			for (var i = 0; i < 18; i++) {
				var channel = channels.FirstOrDefault(x => x.index == i);

				if (channel != null) {
					channels.Remove(channel);
				}
				else {
					channel = new Channel() {index = i};
				}

				var channelEditor = new ChannelEditor(channel, _speakerRef.Speakers[i]);
				var index = i;
				channelEditor.OnValueChanged += transform => {
					_speakerRef.Speakers[index] = transform;
					EditorUtility.SetDirty(_speakerRef);
				};
				_speakerRoot.Add(channelEditor);
			}
		}
	}
}