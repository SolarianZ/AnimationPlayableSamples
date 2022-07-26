using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace GBG.AnimationPlayableSamples
{
    [RequireComponent(typeof(Animator))]
    public class CustomPlayableBehaviourComponent : MonoBehaviour
    {
        public AnimationClip[] clipsToPlay;

        private PlayableGraph _graph;

        private long _frame = 1;

        private string _frameStr = null;


        private string GetFrameStr()
        {
            if (string.IsNullOrEmpty(_frameStr))
            {
                _frameStr = _frame.ToString("D4");
            }

            return _frameStr;
        }

        private void Awake()
        {
            Application.targetFrameRate = 60;
        }

        private void Start()
        {
            Debug.Log($"#{GetFrameStr()} CustomPlayableBehaviourComponent::Start 0", this);

            _graph = PlayableGraph.Create("PlayableDemo-CustomPlayableBehaviour");

            var playable = ScriptPlayable<CustomPlayableBehaviour>.Create(_graph);
            var behaviour = playable.GetBehaviour();
            behaviour.Initialize(clipsToPlay, playable, _graph);

            var animator = GetComponent<Animator>();
            var output = AnimationPlayableOutput.Create(_graph, "AnimationOutput", animator);
            output.SetSourcePlayable(playable, 0);

            _graph.Play();

            Debug.Log($"#{GetFrameStr()} CustomPlayableBehaviourComponent::Start 1", this);
        }

        private void FixedUpdate()
        {
            Debug.Log($"#{GetFrameStr()} CustomPlayableBehaviourComponent::FixedUpdate", this);
        }

        private void Update()
        {
            Debug.Log($"#{GetFrameStr()} CustomPlayableBehaviourComponent::Update", this);
        }


        // Animation events
        [SuppressMessage("CodeQuality", "IDE0051:删除未使用的私有成员", Justification = "<挂起>")]
        private void OnFootstep()
        {
            Debug.Log($"#{GetFrameStr()} CustomPlayableBehaviourComponent::OnFootstep(animation events)", this);
        }

        private void OnAnimatorMove()
        {
            Debug.Log($"#{GetFrameStr()} CustomPlayableBehaviourComponent::OnAnimatorMove", this);
        }

        private void OnAnimatorIK(int layerIndex)
        {
            Debug.Log($"#{GetFrameStr()} CustomPlayableBehaviourComponent::OnAnimatorIK", this);
        }

        private void LateUpdate()
        {
            Debug.Log($"#{GetFrameStr()} CustomPlayableBehaviourComponent::LateUpdate", this);

            _frame++;
            _frameStr = null;
        }

        private void OnDestroy()
        {
            Debug.Log($"#{GetFrameStr()} CustomPlayableBehaviourComponent::OnDestroy 0", this);

            _graph.Destroy();

            Debug.Log($"#{GetFrameStr()} CustomPlayableBehaviourComponent::OnDestroy 1", this);
        }
    }
}
