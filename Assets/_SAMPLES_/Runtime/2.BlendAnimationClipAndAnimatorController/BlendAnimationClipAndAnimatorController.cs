using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace GBG.AnimationPlayableSamples
{
    [RequireComponent(typeof(Animator))]
    public class BlendAnimationClipAndAnimatorController : MonoBehaviour
    {
        public AnimationClip clip;

        public RuntimeAnimatorController controller;

        [Range(0, 1)]
        public float weight;

        PlayableGraph _graph;

        AnimationMixerPlayable _mixer;


        private void Start()
        {
            _graph = PlayableGraph.Create("PlayableDemo-BlendAnimationClipAndAnimatorController");

            var clipPlayable = AnimationClipPlayable.Create(_graph, clip);
            var controllerPlayable = AnimatorControllerPlayable.Create(_graph, controller);
            _mixer = AnimationMixerPlayable.Create(_graph, 2);
            _graph.Connect(clipPlayable, 0, _mixer, 0);
            _graph.Connect(controllerPlayable, 0, _mixer, 1);

            var animator = GetComponent<Animator>();
            var output = AnimationPlayableOutput.Create(_graph, "AnimationOutput", animator);
            output.SetSourcePlayable(_mixer);

            _graph.Play();
        }

        private void Update()
        {
            weight = Mathf.Clamp01(weight);
            _mixer.SetInputWeight(0, 1 - weight);
            _mixer.SetInputWeight(1, weight);
        }

        private void OnDestroy()
        {
            _graph.Destroy();
        }
    }
}
