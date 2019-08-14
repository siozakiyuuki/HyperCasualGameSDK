using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
public class GameManagerSample : MonoBehaviour
{
    public string StageName = "SampleLevel";
    private ReactiveProperty<StageState> GameState { get; set; } = new ReactiveProperty<StageState>();
    private GameUIManagerSample UIManager;
    private int StageLevel = 1;
    private void Awake()
    {
        UIManager = GetComponent<GameUIManagerSample>();
        SetStateEvent();
        SetUIEvent();
    }
    private void Start()
    {
        LoadStage(1);
    }
    private void LoadNext()
    {
        StageLevel++;
        if(StageLevel >= SceneManager.sceneCountInBuildSettings)
        {
            StageLevel = 1;
        }
        LoadStage(StageLevel);
    }
    private void LoadStage(int level)
    {
        try
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1));
        }
        catch (Exception) {
        }
        SceneManager.LoadSceneAsync(StageName + level, LoadSceneMode.Additive).completed += _ => {
            StageManagerAbstract.NowStageState(state => {
                GameState.Value = state;
            });
            OnLoadStage(StageManagerSample.GetSingleton());
        };
    }
    private void SetStateEvent()
    {
        GameState.Subscribe(state => {
            switch (state)
            {
                case StageState.Play:
                    OnPlay();
                    break;
                case StageState.Clear:
                    OnClear();
                    break;
            }
        });
    }
    private void SetUIEvent()
    {
        UIManager.NextButtonClickObservable().Subscribe(_ => LoadNext());
    }
    private void OnLoadStage(StageManagerSample stage)
    {
        stage
            .OnTouchObservable()
            .Subscribe(OnTap);
    }
    private void OnPlay()
    {
        UIManager.SetStageLevel(StageLevel);
        UIManager.SetTapCount(0);
        UIManager.SetTutorialTextActive(false);
        UIManager.SetClearTextActive(false);
        UIManager.SetNextButtonActive(false);
    }
    private void OnClear()
    {
        UIManager.SetClearTextActive(true);
        UIManager.SetNextButtonActive(true);
    }
    private void OnTap(int count)
    {
        UIManager.SetTapCount(count);
    }
}
