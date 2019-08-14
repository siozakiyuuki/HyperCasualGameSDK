using UnityEngine;
public class UnityEditorTouchInput : MonoBehaviour, ITouchInput
{
    public bool OnTouch()
    {
        return Input.GetMouseButtonDown(0);
    }
}
