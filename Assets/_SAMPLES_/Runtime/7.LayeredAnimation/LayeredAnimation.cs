using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace GBG.AnimationPlayableSamples
{
    [RequireComponent(typeof(Animator))]
    public class LayeredAnimation : MonoBehaviour
    {
        public AnimationClip clip0;

        public AvatarMask avatarMask0;

        [Range(0, 1)]
        public float layer0Weight = 1;

        public AnimationClip clip1;

        public AvatarMask avatarMask1;

        [Range(0, 1)]
        public float layer1Weight = 1;

        private PlayableGraph _graph;

        private AnimationLayerMixerPlayable _layerMixer;


        private void Start()
        {
            _graph = PlayableGraph.Create("PlayableDemo-LayeredAnimation");

            var clipPlayable0 = AnimationClipPlayable.Create(_graph, clip0);
            var clipPlayable1 = AnimationClipPlayable.Create(_graph, clip1);
            _layerMixer = AnimationLayerMixerPlayable.Create(_graph, 2);
            _layerMixer.SetLayerMaskFromAvatarMask(0, avatarMask0);
            _layerMixer.SetLayerMaskFromAvatarMask(1, avatarMask1);

            // InputPort <=> Layer
            _graph.Connect(clipPlayable0, 0, _layerMixer, 0);
            _graph.Connect(clipPlayable1, 0, _layerMixer, 1);

            var animator = GetComponent<Animator>();
            var output = AnimationPlayableOutput.Create(_graph, "AnimationOutput", animator);
            output.SetSourcePlayable(_layerMixer);

            _graph.Play();
        }

        private void Update()
        {
            _layerMixer.SetInputWeight(0, layer0Weight);
            _layerMixer.SetInputWeight(1, layer1Weight);
        }

        private void OnDestroy()
        {
            _graph.Destroy();
        }
    }
}
