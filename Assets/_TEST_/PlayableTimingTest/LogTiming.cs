using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace GBG.TimingTest
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Animator))]
    public class LogTiming : MonoBehaviour
    {
        public static int Frame { get; private set; }

        private static string _frameStr;

        private static LogTiming _instance;


        public static string GetFrameString()
        {
            if (string.IsNullOrEmpty(_frameStr))
            {
                _frameStr = Frame.ToString("D6");
            }

            return _frameStr;
        }

        public static void LogLocation(string typeName, string message = null,
            [CallerMemberName] string methodName = null, Object context = null)
        {
            string content;
            if (string.IsNullOrEmpty(message))
            {
                content = $"{GetFrameString()}  {typeName}\t. {methodName}";
            }
            else
            {
                content = $"{GetFrameString()}  {typeName}\t. {methodName}\t: {message}";
            }
            Debug.Log(content, context);
        }


        public bool applyRootMotion = true;

        public AnimationClip clip;

        private Animator _animator;

        private PlayableGraph _graph;


        private void Awake()
        {
            if (!_instance)
            {
                _instance = this;
            }
            else if (_instance != this)
            {
                Destroy(this);
                Debug.LogError($"Destroy instance: {name}.", gameObject);
                return;
            }

            Frame = 0;
            _frameStr = null;

            _animator = GetComponent<Animator>();

            LogLocation(nameof(LogTiming), context: gameObject);

            DontDestroyOnLoad(this);
        }

        private void OnEnable()
        {
            LogLocation(nameof(LogTiming), context: gameObject);

            LogLocation(nameof(LogTiming), message: "Create Graph", context: gameObject);
            _graph = PlayableGraph.Create(nameof(LogTiming));

            LogLocation(nameof(LogTiming), message: "Create LogTimingBehaviour Playable", context: gameObject);
            var timingPlayable = ScriptPlayable<LogTimingBehaviour>.Create(_graph);
            LogLocation(nameof(LogTiming), message: "Get LogTimingBehaviour", context: gameObject);
            var timingBehaviour = timingPlayable.GetBehaviour();
            LogLocation(nameof(LogTiming), message: "Initialize LogTimingBehaviour", context: gameObject);
            timingBehaviour.Initialize(_animator, clip);

            LogLocation(nameof(LogTiming), message: "Create ScriptPlayableOutput", context: gameObject);
            var timingOutput = ScriptPlayableOutput.Create(_graph, nameof(LogTiming));
            LogLocation(nameof(LogTiming), message: "Set ScriptPlayableOutput Source Playable", context: gameObject);
            timingOutput.SetSourcePlayable(timingPlayable);

            //LogLocation(nameof(LogTiming), message: "Create AnimationClipPlayable", context: gameObject);
            //var animPlayable = AnimationClipPlayable.Create(_graph, clip);

            //LogLocation(nameof(LogTiming), message: "Create AnimationPlayableOutput", context: gameObject);
            //var animOutput = AnimationPlayableOutput.Create(_graph, nameof(LogTiming), _animator);

            //LogLocation(nameof(LogTiming), message: "SetSourcePlayable", context: gameObject);
            //animOutput.SetSourcePlayable(animPlayable);

            LogLocation(nameof(LogTiming), message: "Play Graph", context: gameObject);
            _graph.Play();
        }

        private void Start()
        {
            LogLocation(nameof(LogTiming), context: gameObject);
        }

        private void OnDisable()
        {
            LogLocation(nameof(LogTiming), context: gameObject);

            LogLocation(nameof(LogTiming), message: "Destroy Graph", context: gameObject);
            _graph.Destroy();
        }

        private void OnDestroy()
        {
            LogLocation(nameof(LogTiming), context: gameObject);
        }

        private void FixedUpdate()
        {
            LogLocation(nameof(LogTiming), context: gameObject);

            LogLocation(nameof(LogTiming), message: $"Position={_animator.transform.position:F5}", context: gameObject);
            LogLocation(nameof(LogTiming), message: $"RootPosition={_animator.rootPosition:F5}", context: gameObject);
            LogLocation(nameof(LogTiming), message: $"RootRotation={_animator.rootRotation}", context: gameObject);
            LogLocation(nameof(LogTiming), message: $"DeltaPosition={_animator.deltaPosition:F5}", context: gameObject);
            LogLocation(nameof(LogTiming), message: $"DeltaRotation={_animator.deltaRotation}", context: gameObject);
        }

        private void Update()
        {
            LogLocation(nameof(LogTiming), context: gameObject);

            LogLocation(nameof(LogTiming), message: $"Position={_animator.transform.position:F5}", context: gameObject);
            LogLocation(nameof(LogTiming), message: $"RootPosition={_animator.rootPosition:F5}", context: gameObject);
            LogLocation(nameof(LogTiming), message: $"RootRotation={_animator.rootRotation}", context: gameObject);
            LogLocation(nameof(LogTiming), message: $"DeltaPosition={_animator.deltaPosition:F5}", context: gameObject);
            LogLocation(nameof(LogTiming), message: $"DeltaRotation={_animator.deltaRotation}", context: gameObject);
        }

        private void LateUpdate()
        {
            LogLocation(nameof(LogTiming), context: gameObject);
            
            LogLocation(nameof(LogTiming), message: $"Position={_animator.transform.position:F5}", context: gameObject);
            LogLocation(nameof(LogTiming), message: $"RootPosition={_animator.rootPosition:F5}", context: gameObject);
            LogLocation(nameof(LogTiming), message: $"RootRotation={_animator.rootRotation}", context: gameObject);
            LogLocation(nameof(LogTiming), message: $"DeltaPosition={_animator.deltaPosition:F5}", context: gameObject);
            LogLocation(nameof(LogTiming), message: $"DeltaRotation={_animator.deltaRotation}", context: gameObject);

            Frame++;
            _frameStr = null;
        }

        private void OnAnimatorIK(int layerIndex)
        {
            LogLocation(nameof(LogTiming), context: gameObject);
        }

        private void OnAnimatorMove()
        {
            LogLocation(nameof(LogTiming), context: gameObject);

            LogLocation(nameof(LogTiming), message: $"Position={_animator.transform.position:F5}", context: gameObject);
            LogLocation(nameof(LogTiming), message: $"RootPosition={_animator.rootPosition:F5}", context: gameObject);
            LogLocation(nameof(LogTiming), message: $"RootRotation={_animator.rootRotation}", context: gameObject);
            LogLocation(nameof(LogTiming), message: $"DeltaPosition={_animator.deltaPosition:F5}", context: gameObject);
            LogLocation(nameof(LogTiming), message: $"DeltaRotation={_animator.deltaRotation}", context: gameObject);

            if (applyRootMotion)
            {
                LogLocation(nameof(LogTiming), message: "ApplyBuiltinRootMotion", context: gameObject);

                _animator.ApplyBuiltinRootMotion();
            }
        }

        private void SomeAnimationEvent(AnimationEvent e)
        {
            LogLocation(nameof(LogTiming), context: gameObject);
        }
    }
}
