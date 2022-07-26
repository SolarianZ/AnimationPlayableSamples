using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace GBG.AnimationPlayableSamples
{
    [RequireComponent(typeof(Animator))]
    public class PlaySingleClip : MonoBehaviour
    {
        public AnimationClip clip;

        private PlayableGraph _graph;

        private AnimationClipPlayable _clipPlayable;


        private void Start()
        {
            // 1. Create a graph
            // control the lifecycle of playables and their outputs
            _graph = PlayableGraph.Create("PlayableDemo-PlaySingleClip");
            //_graph.SetTimeUpdateMode(DirectorUpdateMode.Manual);

            // 2. Create a playable
            // as the source of the output
            _clipPlayable = AnimationClipPlayable.Create(_graph, clip);

            // 3. Create a output
            // as the output of the graph
            var animator = GetComponent<Animator>();
            var animOutput = AnimationPlayableOutput.Create(_graph, "AnimationOutput", animator);
            animOutput.SetSourcePlayable(_clipPlayable);

            // 4. Play the graph
            _graph.Play();

            // A simplify of above codes
            //AnimationPlayableUtilities.PlayClip(GetComponent<Animator>(), clip, out _graph);
        }

        private void OnDestroy()
        {
            //_clipPlayable.Destroy();

            // Destroy all playables and outputs that were create by this graph
            _graph.Destroy();
        }


        public bool IsAnimationPlaying()
        {
            return _clipPlayable.GetPlayState() == PlayState.Playing;
        }

        public double GetPlaybackSpeed()
        {
            if (!_clipPlayable.IsValid())
            {
                return 1;
            }

            return _clipPlayable.GetSpeed();
        }

        public void SetPlaybackSpeed(double speed)
        {
            _clipPlayable.SetSpeed(speed);
        }

        public void PauseAnimation()
        {
            _clipPlayable.Pause();
        }

        public void ResumeAnimation()
        {
            _clipPlayable.Play();
        }
    }
}
