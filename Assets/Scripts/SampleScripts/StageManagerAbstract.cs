using System;
using UnityEngine;
using UniRx;

public abstract class StageManagerAbstract : MonoBehaviour
{
    private static StageManagerAbstract _nowStageManager;
    private static IDisposable _disposable;
    public static void NowStageState(Action<StageState> action)
    {
        _disposable?.Dispose();
        _disposable = _nowStageManager.StageStates.Subscribe(action);
    }
    protected ReactiveProperty<StageState> StageStates { get; set; } = new ReactiveProperty<StageState>(StageState.Play);
    protected virtual void Awake()
    {
        _nowStageManager = this;
    }
}

public enum StageState
{
    Play, Clear
}