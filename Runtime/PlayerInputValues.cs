using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayerInputData", menuName = "ScriptableObjects/PlayerInputValues", order = 1)]
public class PlayerInputValues : ScriptableObject
{
    /// <summary>
    /// The vector from the origin of the movement joystick to the current touch position in pixels
    /// </summary>
    public Vector2 RawMovementDir;
    /// <summary>
    /// The vector from the origin of the look joystick to the current touch position in pixels
    /// </summary>
    public Vector2 RawLookDir;
    /// <summary>
    /// The normalized direction from the origin of the movement joystick to the current touch position
    /// </summary>
    public Vector2 MovementDir { get { return RawMovementDir.normalized; } }
    /// <summary>
    /// The normalized direction from the origin of the look joystick to the current touch position
    /// </summary>
    public Vector2 LookDir { get { return RawLookDir.normalized; } }
    /// <summary>
    /// Position where the movement joystick should be drawn in screen space normalized
    /// </summary>
    public Vector2 MovementJoyStickOrigin;
    /// <summary>
    /// Position where the look joystick should be drawn in screen space normalized
    /// </summary>
    public Vector2 LookJoyStickOrigin;

    public UnityEvent<Vector2> OnLeftTouchDown;
    public UnityEvent OnLeftTouchUp;
    public UnityEvent<Vector2> OnRightTouchDown;
    public UnityEvent OnRightTouchUp;

    private void Awake()
    {
        RawMovementDir = RawLookDir = MovementJoyStickOrigin = LookJoyStickOrigin = Vector2.zero;
        OnLeftTouchDown = new UnityEvent<Vector2>();
        OnLeftTouchUp = new UnityEvent();
        OnRightTouchDown = new UnityEvent<Vector2>();
        OnRightTouchUp = new UnityEvent();
    }
}
