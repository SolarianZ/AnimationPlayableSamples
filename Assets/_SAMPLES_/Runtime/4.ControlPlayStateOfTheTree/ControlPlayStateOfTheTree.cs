using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;

namespace GBG.AnimationPlayableSamples
{
    [RequireComponent(typeof(Animator))]
    public class ControlPlayStateOfTheTree : MonoBehaviour
    {
        public AnimationClip clip0;

        public AnimationClip clip1;

        private PlayableGraph _graph;

        private AnimationMixerPlayable _mixer;


        private void Start()
        {
            _graph = PlayableGraph.Create("PlayableDemo-ControllPlayStateOfTheTree");

            var animPlayable0 = AnimationClipPlayable.Create(_graph, clip0);
            var animPlayable1 = AnimationClipPlayable.Create(_graph, clip1);
            _mixer = AnimationMixerPlayable.Create(_graph, 2);
            _graph.Connect(animPlayable0, 0, _mixer, 0);
            _graph.Connect(animPlayable1, 0, _mixer, 1);
            _mixer.SetInputWeight(0, 1.0f);
            _mixer.SetInputWeight(1, 1.0f);

            // internal time will stop advancing and keep outputs the same value
            animPlayable0.Pause(); // SetPlayState(PlayState.Paused) is obsoleted

            var animator = GetComponent<Animator>();
            var output = AnimationPlayableOutput.Create(_graph, "AnimationOutput", animator);
            output.SetSourcePlayable(_mixer);

            _graph.Play();
        }

        private void OnDestroy()
        {
            _graph.Destroy();
        }
    }
}
