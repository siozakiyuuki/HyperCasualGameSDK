using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx;
public class GameUIManagerSample : MonoBehaviour
{
    public Text LevelText;
    public Text TutorialText;
    public Text ClearText;
    public Button NextButton;
    public Text TapCountText;
    public Image BackGround;

    public void SetLevelTextActive(bool isActive)
    {
        SetUIBehaviorActive(LevelText, isActive);
    }
    public void SetStageLevel(int level)
    {
        LevelText.text = "Level" + level.ToString();
    }
    public void SetClearTextActive(bool isActive)
    {
        SetUIBehaviorActive(ClearText, isActive);
    }
    public void SetNextButtonActive(bool isActive)
    {
        SetUIBehaviorActive(NextButton, isActive);
    }
    public void SetTapCountTextActive(bool isActive)
    {
        SetUIBehaviorActive(TapCountText, isActive);
    }
    public void SetTapCount(int count)
    {
        TapCountText.text = "Count : " + count.ToString();
    }
    public void SetBackGroundColor(Color color)
    {
        BackGround.color = color;
    }
    public void SetTutorialTextActive(bool isActive)
    {
        SetUIBehaviorActive(TutorialText, isActive);
    }
    public IObservable<Unit> NextButtonClickObservable()
    {
        return NextButton.OnClickAsObservable();
    }

    private static void SetUIBehaviorActive(UIBehaviour behavior, bool isActive) => behavior.gameObject.SetActive(isActive);
}
