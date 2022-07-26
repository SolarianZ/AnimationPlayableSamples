using System;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace GBG.AnimationPlayableSamples
{
    [RequireComponent(typeof(Animator))]
    public class BlendTwoClips : MonoBehaviour
    {
        public AnimationClip clip0;

        public AnimationClip clip1;

        [Range(0, 1)]
        public float weight;

        private PlayableGraph _graph;

        private AnimationMixerPlayable _mixer;


        private void Start()
        {
            // 1. Create a graph
            // control the lifecycle of playables and their outputs
            _graph = PlayableGraph.Create("PlayableDemo-BlendTwoClips");

            // 2. Create two playables
            // as the input of the mixer
            var clipPlayable0 = AnimationClipPlayable.Create(_graph, clip0);
            var clipPlayable1 = AnimationClipPlayable.Create(_graph, clip1);

            // 3. Create a mixer, and connect inputs to the mixer
            // as the source of the output
            _mixer = AnimationMixerPlayable.Create(_graph, 2);
            _graph.Connect(clipPlayable0, 0, _mixer, 0);
            _graph.Connect(clipPlayable1, 0, _mixer, 1);

            // 4. Create a output
            // as the output of the graph
            var animator = GetComponent<Animator>();
            var playableOutput = AnimationPlayableOutput.Create(_graph, "AnimationOutput", animator);
            playableOutput.SetSourcePlayable(_mixer);

            // 4. Play the graph
            _graph.Play();
        }

        private void Update()
        {
            // Adjust the blend weight of each playable
            weight = Mathf.Clamp01(weight);
            _mixer.SetInputWeight(0, 1 - weight);
            _mixer.SetInputWeight(1, weight);
        }

        private void OnDestroy()
        {
            // Destroy all playables and outputs that were create by this graph
            _graph.Destroy();
        }
    }
}
