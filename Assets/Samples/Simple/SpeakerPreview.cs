using System;
using System.Collections;
using Trismegistus.AudioToolkit;
using UnityEngine;
using Tools = Trismegistus.AudioToolkit.Tools;


public class SpeakerPreview : MonoBehaviour {
	[SerializeField] private AudioGenerator audioGenerator = null;
	[SerializeField] private SpeakerReference speakerReference = null;

	private bool _inProgress;
	private float[] _volumes;
	private Vector3[] _speakers;

	private void Awake() { }

	private void OnGUI() {
		if (GUI.Button(new Rect(12, 12, 100, 30), _inProgress ? "Stop" : "Play")) {
			if (_inProgress) {
				audioGenerator.Stop();
				_inProgress = false;
				
			}
			else {
				_inProgress = true;
				_speakers = speakerReference.GetPositions();
				_volumes = Tools.CalculateVolumes(_speakers, audioGenerator.AudioSource);
				audioGenerator.UpdateSound(_volumes);
				audioGenerator.Play();
				WaitForSoundEnd();
			}
		}
	}

	private void WaitForSoundEnd() {
		StartCoroutine(i());

		IEnumerator i() {
			while (true) {
				yield return null;
				if (audioGenerator.AudioSource.isPlaying) continue;
				
				_inProgress = false;
				yield break;
			}
		}
	}

	private void OnDrawGizmos() {
		if (_volumes == null || _speakers == null) return;
		if (_volumes.Length != _speakers.Length) return;
		
		var c = Gizmos.color;
		for (var i = 0; i < _speakers.Length; i++) {
			Gizmos.color = Color.Lerp(Color.red, Color.green, _volumes[i]);
			Gizmos.DrawSphere(_speakers[i], 0.5f);
		}

		Gizmos.color = c;
	}
}