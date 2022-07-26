using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Audio;
using UnityEngine.Playables;

namespace GBG.AnimationPlayableSamples
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(AudioSource))]
    public class GraphWithMultiOutputs : MonoBehaviour
    {
        public AnimationClip animationClip;

        public AudioClip audioClip;

        private PlayableGraph _graph;


        private void Start()
        {
            _graph = PlayableGraph.Create("PlayableDemo-GraphWithMultiOutputs");

            var animPlayable = AnimationClipPlayable.Create(_graph, animationClip);
            var animator = GetComponent<Animator>();
            var animOutput = AnimationPlayableOutput.Create(_graph, "AnimationOutput", animator);
            animOutput.SetSourcePlayable(animPlayable);

            var audioPlayable = AudioClipPlayable.Create(_graph, audioClip, true);
            var audioSource = GetComponent<AudioSource>();
            var audioOutput = AudioPlayableOutput.Create(_graph, "AudioOutput", audioSource);
            audioOutput.SetSourcePlayable(audioPlayable);

            _graph.Play();
        }

        private void OnDestroy()
        {
            _graph.Destroy();
        }
    }
}
