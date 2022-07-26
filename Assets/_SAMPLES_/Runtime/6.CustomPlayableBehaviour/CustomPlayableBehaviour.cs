using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace GBG.AnimationPlayableSamples
{
    public class CustomPlayableBehaviour : PlayableBehaviour
    {
        private int _currentClipIndex = -1;

        private float _timeToNextClip;

        private Playable _mixer;


        public void Initialize(AnimationClip[] clipsToPlay, Playable owner, PlayableGraph graph)
        {
            Debug.Log("#      CustomPlayableBehaviour::Initialize 0");

            owner.SetInputCount(1);

            _mixer = AnimationMixerPlayable.Create(graph, clipsToPlay.Length);

            // mixer -> owner
            graph.Connect(_mixer, 0, owner, 0);

            owner.SetInputWeight(0, 1);

            for (int i = 0; i < _mixer.GetInputCount(); i++)
            {
                var clipPlayable = AnimationClipPlayable.Create(graph, clipsToPlay[i]);
                graph.Connect(clipPlayable, 0, _mixer, i);
                _mixer.SetInputWeight(i, 1.0f);
            }

            Debug.Log("#      CustomPlayableBehaviour::Initialize 1");
        }

        public override void OnGraphStart(Playable playable)
        {
            Debug.Log("#      CustomPlayableBehaviour::OnGraphStart");
        }

        public override void OnGraphStop(Playable playable)
        {
            Debug.Log("#      CustomPlayableBehaviour::OnGraphStop");
        }

        public override void OnPlayableCreate(Playable playable)
        {
            Debug.Log("#      CustomPlayableBehaviour::OnPlayableCreate");
        }

        public override void OnPlayableDestroy(Playable playable)
        {
            Debug.Log("#      CustomPlayableBehaviour::OnPlayableDestroy");
        }

        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            Debug.Log("#      CustomPlayableBehaviour::OnBehaviourPlay");
        }

        public override void OnBehaviourPause(Playable playable, FrameData info)
        {
            Debug.Log("#      CustomPlayableBehaviour::OnBehaviourPause");
        }

        public override void PrepareData(Playable playable, FrameData info)
        {
            Debug.Log("#      CustomPlayableBehaviour::PrepareData");
        }

        public override void PrepareFrame(Playable playable, FrameData info)
        {
            Debug.Log("#      CustomPlayableBehaviour::PrepareFrame 0");

            base.PrepareFrame(playable, info);

            if (_mixer.GetInputCount() == 0)
            {
                return;
            }

            // Advance to next clip if necessary
            _timeToNextClip -= (float)info.deltaTime;
            if (_timeToNextClip <= 0f)
            {
                _currentClipIndex++;
                if (_currentClipIndex >= _mixer.GetInputCount())
                {
                    _currentClipIndex = 0;
                }

                var currentClip = (AnimationClipPlayable)_mixer.GetInput(_currentClipIndex);
                // Play from start
                currentClip.SetTime(0);

                //_timeToNextClip = currentClip.GetDuration();
                _timeToNextClip = currentClip.GetAnimationClip().length;
            }

            // Adjust the weight of the inputs
            for (int i = 0; i < _mixer.GetInputCount(); i++)
            {
                if (i == _currentClipIndex)
                {
                    _mixer.SetInputWeight(i, 1.0f);
                }
                else
                {
                    _mixer.SetInputWeight(i, 0.0f);
                }
            }

            Debug.Log("#      CustomPlayableBehaviour::PrepareFrame 1");
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            Debug.Log("#      CustomPlayableBehaviour::ProcessFrame");
        }

    }
}
