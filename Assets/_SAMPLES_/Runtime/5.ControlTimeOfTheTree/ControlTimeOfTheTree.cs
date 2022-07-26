using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;


namespace GBG.AnimationPlayableSamples
{
    [RequireComponent(typeof(Animator))]
    public class ControlTimeOfTheTree : MonoBehaviour
    {
        public AnimationClip clip;

        public float time;

        private PlayableGraph _graph;

        private AnimationClipPlayable _clipPlayable;


        private void Start()
        {
            _graph = PlayableGraph.Create("PlayableDemo-ControlTimeOfTheTree");

            _clipPlayable = AnimationClipPlayable.Create(_graph, clip);

            var animator = GetComponent<Animator>();
            var output = AnimationPlayableOutput.Create(_graph, "AnimationOutput", animator);
            output.SetSourcePlayable(_clipPlayable);

            _graph.Play();

            // Pause the clip playable, then we control the time manually
            _clipPlayable.Pause();
        }

        private void Update()
        {
            _clipPlayable.SetTime(time);
        }

        private void OnDestroy()
        {
            _graph.Destroy();
        }
    }
}
