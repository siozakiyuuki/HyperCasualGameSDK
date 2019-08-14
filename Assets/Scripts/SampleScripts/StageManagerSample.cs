using System;
using UniRx;
public class StageManagerSample : StageManagerAbstract
{
    private static StageManagerSample _singleton;
    public static StageManagerSample GetSingleton() {
        return _singleton;
    }
    private readonly ReactiveProperty<int> TapCount = new ReactiveProperty<int>(0);
    public IObservable<int> OnTouchObservable() => TapCount;
    private ITouchInput TouchInput;
    public int StageNumber;
    protected override void Awake()
    {
        base.Awake();
        _singleton = this;
        TouchInput = GetComponent<ITouchInput>();
    }
    private void Start()
    {
        Observable
             .EveryUpdate()
             .Where(_ => StageStates.Value == StageState.Play)
             .Where(_ => TouchInput.OnTouch())
             .Subscribe(_ => OnTap());
    }
    private void OnTap()
    {
        TapCount.Value++;
        if(TapCount.Value == StageNumber)
        {
            StageStates.Value = StageState.Clear;
        }
    }
}
public interface ITouchInput
{
    bool OnTouch();
}
