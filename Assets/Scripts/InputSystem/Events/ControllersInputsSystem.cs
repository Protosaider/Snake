using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Handler-class for all inputs (GetButtonDown, GetAxis, etc.). 
/// Uses custom InputManager (MonoBehaviour).
/// </summary>
public class ControllersInputsSystem : MonoBehaviour {

    private IInputManager input;

	// Use this for initialization
	private void Start ()
    {
        input = InputManager.instance;
    }

    #region Axis Raw
    //! --- Axis Raw ---
    #region Left Joystick
    //public float LeftHorizontal(int playerID)
    //{
    //    float r = 0.0f;
    //    r += input.GetAxis(playerID, EInputAction.LeftHorizontalJoystick);
    //    r += input.GetAxis(playerID, EInputAction.LeftHorizontalKeyboard);
    //    return Mathf.Clamp(r, -1.0f, 1.0f);
    //}
    //public float LeftVertical(int playerID)
    //{
    //    float r = 0.0f;
    //    r += input.GetAxis(playerID, EInputAction.LeftVerticalJoystick);
    //    r += input.GetAxis(playerID, EInputAction.LeftVerticalKeyboard);
    //    return Mathf.Clamp(r, -1.0f, 1.0f);
    //}
    //public Vector3 LeftJoystick(int playerID)
    //{
    //    return new Vector3(LeftHorizontal(), 0, LeftVertical());
    //}
    #endregion

    #region Right Joystick
    //public float RightHorizontal(int playerID)
    //{
    //    float r = 0.0f;
    //    r += input.GetAxis(playerID, EInputAction.RightHorizontalJoystick);
    //    r += input.GetAxis(playerID, EInputAction.RightHorizontalKeyboard);
    //    return Mathf.Clamp(r, -1.0f, 1.0f);
    //}
    //public float RightVertical(int playerID)
    //{
    //    float r = 0.0f;
    //    r += input.GetAxis(playerID, EInputAction.RightVerticalJoystick);
    //    r += input.GetAxis(playerID, EInputAction.RightVerticalKeyboard);
    //    return Mathf.Clamp(r, -1.0f, 1.0f);
    //}
    //public Vector3 RightJoystick(int playerID)
    //{
    //    return new Vector3(RightHorizontal(), 0, RightVertical());
    //}
    #endregion

    #region Simple Axis
    public float Horizontal(int playerId)
    {
        return input.GetAxisRaw(playerId, EInputAction.Horizontal);
    }

    public float Vertical(int playerId)
    {
        return input.GetAxisRaw(playerId, EInputAction.Vertical);
    }

    public float HorizontalInverse(int playerId)
    {
        return - input.GetAxisRaw(playerId, EInputAction.Horizontal);
    }

    public float VerticalInverse(int playerId)
    {
        return - input.GetAxisRaw(playerId, EInputAction.Vertical);
    }

    #endregion
    #endregion

    #region Button Status
    //! -- Buttons Status --
    public bool IsHeld(int playerId, EInputAction buttonName)
    {
        return input.GetButton(playerId, buttonName);
    }

    public bool IsDown(int playerId, EInputAction buttonName)
    {
        return input.GetButtonDown(playerId, buttonName);
    }

    public bool IsUp(int playerId, EInputAction buttonName)
    {
        return input.GetButtonUp(playerId, buttonName);
    }
    #endregion

}
