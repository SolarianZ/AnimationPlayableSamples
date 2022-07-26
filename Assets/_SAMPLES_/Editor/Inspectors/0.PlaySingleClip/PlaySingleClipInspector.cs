using GBG.AnimationPlayableSamples;
using UnityEditor;
using UnityEngine;

namespace GBG.Editor.AnimationPlayableSamples
{
    [CustomEditor(typeof(PlaySingleClip))]
    internal class PlaySingleClipInspector : UnityEditor.Editor
    {
        private float _playbackSpeed = 1.0f;

        private PlaySingleClip _target => (PlaySingleClip)target;


        private void OnEnable()
        {
            if (!Application.isPlaying)
            {
                return;
            }

            _playbackSpeed = (float)_target.GetPlaybackSpeed();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (!Application.isPlaying)
            {
                return;
            }

            // Playback speed slider
            EditorGUILayout.Space();
            EditorGUI.BeginChangeCheck();
            _playbackSpeed = EditorGUILayout.Slider(_playbackSpeed, -2, 2);
            if (EditorGUI.EndChangeCheck())
            {
                _target.SetPlaybackSpeed(_playbackSpeed);
            }

            // Pause / Resume button
            EditorGUILayout.Space();
            var isGraphPlaying = _target.IsAnimationPlaying();
            if (isGraphPlaying)
            {
                if (GUILayout.Button("Pause"))
                {
                    _target.PauseAnimation();
                }
            }
            else
            {
                if (GUILayout.Button("Resume"))
                {
                    _target.ResumeAnimation();
                }
            }
        }
    }
}
