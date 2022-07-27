# Playable动画与传统Animator动画对比


## AnimatorController的缺点

- 网状结构难以维护
- 无法控制动画资产的加载时机
- 无法运行时增删动画，只能使用OverrideController替换动画
    - 【高开销】每次替换，Unity会将所有State数据合并到AnimationSet中
- 只能在AnimatorController级别控制动画播放过程

-----

## Playable动画的优点

- 方便插入自定义数据
- 可以定制动画资产的加载策略
- 提供Clip级别的播放控制
    - 播放进度
    - 播放速度
    - 权重
    - Mask
- 允许运行时在任意时间进行任意Playable混合
    - Clip与Clip
    - Clip与Mixer
    - Clip与AnimatorController

-----

## Playable动画的缺点

- 需要重新构建一套系统，来实现AnimatorController的所有功能
- 需要重新开发配套工具

-----

**参考资料：**

> https://docs.unity3d.com/2021.3/Documentation/Manual/Playables-Examples.html 
> 
> https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Playables.PlayableGraph.html 
> 
> https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Playables.PlayableExtensions.html 
> 
> https://docs.unity3d.com/2021.3/Documentation/ScriptReference/Animator.html 
> 
> https://github.com/Unity-Technologies/SimpleAnimation 
> 
> https://github.com/Unity-Technologies/animation-jobs-samples 
> 
> https://assetstore.unity.com/packages/tools/animation/animancer-pro-116514 
