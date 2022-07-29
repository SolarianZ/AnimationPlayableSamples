using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace GBG.TimingTest
{
    public class LogTimingBehaviour : PlayableBehaviour
    {
        private Animator _animator;
        private AnimationClip _clip;
        private AnimationClipPlayable _animPlayable;


        public LogTimingBehaviour()
        {
            LogTiming.LogLocation(nameof(LogTimingBehaviour));
        }

        public void Initialize(Animator animator, AnimationClip clip)
        {
            LogTiming.LogLocation(nameof(LogTimingBehaviour));

            _animator = animator;
            _clip = clip;

        }

        //
        // 摘要:
        //     This function is called when the PlayableGraph that owns this PlayableBehaviour
        //     starts.
        //
        // 参数:
        //   playable:
        //     The Playable that owns the current PlayableBehaviour.
        public override void OnGraphStart(Playable playable)
        {
            LogTiming.LogLocation(nameof(LogTimingBehaviour));

            var graph = playable.GetGraph();

            LogTiming.LogLocation(nameof(LogTimingBehaviour), message: "Create AnimationClipPlayable");
            _animPlayable = AnimationClipPlayable.Create(graph, _clip);
            _animPlayable.Pause();

            LogTiming.LogLocation(nameof(LogTimingBehaviour), message: "Create AnimationPlayableOutput");
            var animOutput = AnimationPlayableOutput.Create(graph, nameof(LogTiming), _animator);

            LogTiming.LogLocation(nameof(LogTimingBehaviour), message: "SetSourcePlayable");
            animOutput.SetSourcePlayable(_animPlayable);
        }

        //
        // 摘要:
        //     This function is called when the PlayableGraph that owns this PlayableBehaviour
        //     stops.
        //
        // 参数:
        //   playable:
        //     The Playable that owns the current PlayableBehaviour.
        public override void OnGraphStop(Playable playable)
        {
            LogTiming.LogLocation(nameof(LogTimingBehaviour));
        }

        //
        // 摘要:
        //     This function is called when the Playable that owns the PlayableBehaviour is
        //     created.
        //
        // 参数:
        //   playable:
        //     The Playable that owns the current PlayableBehaviour.
        public override void OnPlayableCreate(Playable playable)
        {
            LogTiming.LogLocation(nameof(LogTimingBehaviour));
        }

        //
        // 摘要:
        //     This function is called when the Playable that owns the PlayableBehaviour is
        //     destroyed.
        //
        // 参数:
        //   playable:
        //     The Playable that owns the current PlayableBehaviour.
        public override void OnPlayableDestroy(Playable playable)
        {
            LogTiming.LogLocation(nameof(LogTimingBehaviour));
        }

        //
        // 摘要:
        //     This function is called when the Playable play state is changed to Playables.PlayState.Delayed.
        //
        // 参数:
        //   playable:
        //     The Playable that owns the current PlayableBehaviour.
        //
        //   info:
        //     A FrameData structure that contains information about the current frame context.
        public override void OnBehaviourDelay(Playable playable, FrameData info)
        {
            LogTiming.LogLocation(nameof(LogTimingBehaviour));
        }

        //
        // 摘要:
        //     This function is called when the Playable play state is changed to Playables.PlayState.Playing.
        //
        // 参数:
        //   playable:
        //     The Playable that owns the current PlayableBehaviour.
        //
        //   info:
        //     A FrameData structure that contains information about the current frame context.
        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            LogTiming.LogLocation(nameof(LogTimingBehaviour));

            LogTiming.LogLocation(nameof(LogTimingBehaviour), message: "Play AnimationClipPlayable");
            _animPlayable.Play();
        }

        //
        // 摘要:
        //     This method is invoked when one of the following situations occurs: <br><br>
        //     The effective play state during traversal is changed to Playables.PlayState.Paused.
        //     This state is indicated by FrameData.effectivePlayState.<br><br> The PlayableGraph
        //     is stopped while the playable play state is Playing. This state is indicated
        //     by PlayableGraph.IsPlaying returning true.
        //
        // 参数:
        //   playable:
        //     The Playable that owns the current PlayableBehaviour.
        //
        //   info:
        //     A FrameData structure that contains information about the current frame context.
        public override void OnBehaviourPause(Playable playable, FrameData info)
        {
            LogTiming.LogLocation(nameof(LogTimingBehaviour));

            LogTiming.LogLocation(nameof(LogTimingBehaviour), message: "Pause AnimationClipPlayable");
            _animPlayable.Pause();
        }

        //
        // 摘要:
        //     This function is called during the PrepareData phase of the PlayableGraph.
        //
        // 参数:
        //   playable:
        //     The Playable that owns the current PlayableBehaviour.
        //
        //   info:
        //     A FrameData structure that contains information about the current frame context.
        public override void PrepareData(Playable playable, FrameData info)
        {
            LogTiming.LogLocation(nameof(LogTimingBehaviour));
        }

        //
        // 摘要:
        //     This function is called during the PrepareFrame phase of the PlayableGraph.
        //
        // 参数:
        //   playable:
        //     The Playable that owns the current PlayableBehaviour.
        //
        //   info:
        //     A FrameData structure that contains information about the current frame context.
        public override void PrepareFrame(Playable playable, FrameData info)
        {
            LogTiming.LogLocation(nameof(LogTimingBehaviour));
        }

        //
        // 摘要:
        //     This function is called during the ProcessFrame phase of the PlayableGraph.
        //
        // 参数:
        //   playable:
        //     The Playable that owns the current PlayableBehaviour.
        //
        //   info:
        //     A FrameData structure that contains information about the current frame context.
        //
        //   playerData:
        //     The user data of the ScriptPlayableOutput that initiated the process pass.
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            LogTiming.LogLocation(nameof(LogTimingBehaviour));

            LogTiming.LogLocation(nameof(LogTiming), message: $"Position={_animator.transform.position:F5}");
            LogTiming.LogLocation(nameof(LogTiming), message: $"RootPosition={_animator.rootPosition:F5}");
            LogTiming.LogLocation(nameof(LogTiming), message: $"RootRotation={_animator.rootRotation}");
            LogTiming.LogLocation(nameof(LogTimingBehaviour), message: $"DeltaPosition={_animator.deltaPosition:F5}");
            LogTiming.LogLocation(nameof(LogTimingBehaviour), message: $"DeltaRotation={_animator.deltaRotation}");
        }

        public override object Clone()
        {
            LogTiming.LogLocation(nameof(LogTimingBehaviour));

            return base.Clone();
        }
    }
}