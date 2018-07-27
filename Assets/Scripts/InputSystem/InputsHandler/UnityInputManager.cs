using System.Collections.Generic;
using UnityEngine;

public sealed class UnityInputManager : InputManager
{
    [SerializeField]
    private string _playerAxisPrefix = ""; //may or may not use this thing
    [SerializeField]
    private int _maxNumberOfPlayers = 1;    //default = 1

    #region Enable\Disable Actions
    public Dictionary<EInputAction, bool>[] buttonsEnabledStatus;

    private void InitializeAllActionsStatus()
    {
        buttonsEnabledStatus = new Dictionary<EInputAction, bool>[_maxNumberOfPlayers];

        for (int i = 0; i < _maxNumberOfPlayers; i++)
        {
            Dictionary<EInputAction, bool> playerActions = new Dictionary<EInputAction, bool>();
            buttonsEnabledStatus[i] = playerActions;

            foreach (var item in System.Enum.GetValues(typeof(EInputAction)))
            {
                if ((EInputAction.None == (EInputAction)item)) //is it right?
                {
                    continue;
                }
                playerActions.Add((EInputAction)item, true);
            }
        }
    }

    public void ChangeActionsStatus(int playerId, EInputAction action, bool isEnabled)
    {
        buttonsEnabledStatus[playerId][action] = isEnabled;
    }

    public bool IsActionEnabled(int playerId, EInputAction action)
    {
        return buttonsEnabledStatus[playerId][action];
    }
    #endregion

    #region Unity Axis Mappings
    [Header("Unity Axis Mappings")]
    [SerializeField]
    private string _horizontalAxis = "Horizontal";
    [SerializeField]
    private string _verticalAxis = "Vertical";
    [SerializeField]
    private string _mouseXAxis = "Mouse X";
    [SerializeField]
    private string _mouseYAxis = "Mouse Y";
    [SerializeField]
    private string _fire1Axis = "Fire1";
    [SerializeField]
    private string _fire2Axis = "Fire2";
    [SerializeField]
    private string _fire3Axis = "Fire3";
    [SerializeField]
    private string _jumpAxis = "Jump";

    private Dictionary<int, string>[] _actions;
    #endregion

    #region Unity Keys Mappings

    [Header("Unity Keys Mappings")]
    [SerializeField]
    private KeyCode _horizontalPositiveKey = KeyCode.D;
    [SerializeField]
    private KeyCode _horizontalNegativeKey = KeyCode.A;
    [SerializeField]
    private KeyCode _verticalPositiveKey = KeyCode.W;
    [SerializeField]
    private KeyCode _verticalNegativeKey = KeyCode.S;
    [SerializeField]
    private KeyCode _mouseXKey = KeyCode.Mouse0;
    [SerializeField]
    private KeyCode _mouseYKey = KeyCode.Mouse1;
    [SerializeField]
    private KeyCode _fire1Key = KeyCode.Mouse0;
    [SerializeField]
    private KeyCode _fire2Key = KeyCode.Mouse0;
    [SerializeField]
    private KeyCode _fire3Key = KeyCode.Mouse0;
    [SerializeField]
    private KeyCode _jumpKey = KeyCode.Space;

    private Dictionary<string, Dictionary<int, KeyCode>> _actionsKeys;
        //! If we not using prefix
        //private Dictionary<int, KeyCode>[] _actionsKeys;

    #endregion

    #region Initialization
    protected override void Awake()
    {
        base.Awake();

        if (InputManager.instance != null)
        {
            IsEnabled = false;
            return;
        }

        SetInstance(this);

        InitializeActionsKeys();
        InitializeActionsButtons();
        InitializeAllActionsStatus();
    }

    //! etc. Example is uncompleted
    private void InitializeActionsKeys()
    {
        _actionsKeys = new Dictionary<string, Dictionary<int, KeyCode>>(_maxNumberOfPlayers);

        for (int i = 0; i < _maxNumberOfPlayers; i++)
        {
            string prefix = !string.IsNullOrEmpty(_playerAxisPrefix) ? _playerAxisPrefix : string.Empty;
            prefix += i;

            Dictionary<int, KeyCode> playerActions = new Dictionary<int, KeyCode>();
            _actionsKeys[prefix] = playerActions;

            playerActions.Add((int)EInputAction.Horizontal, _horizontalPositiveKey);
            playerActions.Add((int)EInputAction.Vertical, _horizontalNegativeKey);
            playerActions.Add((int)EInputAction.Fire1, _verticalPositiveKey);
            playerActions.Add((int)EInputAction.Fire2, _verticalNegativeKey);
        }
    }

    private void InitializeActionsButtons()
    {
        _actions = new Dictionary<int, string>[_maxNumberOfPlayers];

        for (int i = 0; i < _maxNumberOfPlayers; i++)
        {
            Dictionary<int, string> playerActions = new Dictionary<int, string>();
            _actions[i] = playerActions;

            //if has smth - prefix = playerPrefix + playerNumber; like: hellPlayer1
            string prefix = !string.IsNullOrEmpty(_playerAxisPrefix) ? _playerAxisPrefix + i : string.Empty;

            AddActionButton(EInputAction.Horizontal, prefix + _horizontalAxis, playerActions);
            AddActionButton(EInputAction.Vertical, prefix + _verticalAxis, playerActions);
            AddActionButton(EInputAction.MouseX, prefix + _mouseXAxis, playerActions);
            AddActionButton(EInputAction.MouseY, prefix + _mouseYAxis, playerActions);
            AddActionButton(EInputAction.Fire1, prefix + _fire1Axis, playerActions);
            AddActionButton(EInputAction.Fire2, prefix + _fire2Axis, playerActions);
            AddActionButton(EInputAction.Fire3, prefix + _fire3Axis, playerActions);
            AddActionButton(EInputAction.Jump, prefix + _jumpAxis, playerActions);
        }
    }

    private static void AddActionButton(EInputAction action, string actionName, Dictionary<int, string> actions)
    {
        if (string.IsNullOrEmpty(actionName))
        {
            return;
        }
        actions.Add((int)action, actionName);
    }

    #endregion

    #region Calling the action (Unity Axis Mapping)
    //!calling the action Unity Input System

    public override bool GetButton(int playerId, EInputAction action)
    {
        //playerId - number of dictionary in array. Action - number of string in dictionary. 1) Prefix null. 2) Prefix != null
        bool value = Input.GetButton(_actions[playerId][(int)action]); //example of return:  1) Jump         2) hellPlayer1Jump
        return value;
    }

    // tells us if the user pressed the key down during the last Update() function
    public override bool GetButtonDown(int playerId, EInputAction action)
    {
        bool value = Input.GetButtonDown(_actions[playerId][(int)action]); //calling the action Unity Input System
        return value;
    }

    public override bool GetButtonUp(int playerId, EInputAction action)
    {
        bool value = Input.GetButtonUp(_actions[playerId][(int)action]); //calling the action Unity Input System
        return value;
    }

    public override float GetAxis(int playerId, EInputAction action)
    {
        float value = Input.GetAxis(_actions[playerId][(int)action]); //calling the action Unity Input System
        return value;
    }

    //input is not smoothed, keyboard input will always be either -1, 0 or 1.
    public override float GetAxisRaw(int playerId, EInputAction action)
    {
        float value = Input.GetAxisRaw(_actions[playerId][(int)action]); //calling the action Unity Input System
        return value;
    }

    #endregion

    #region Unity Key Mapping

    public override bool GetKey(string playerId, EInputAction action)
    {
        return Input.GetKey(_actionsKeys[playerId][(int)action]);
    }

    public override bool GetKeyDown(string playerId, EInputAction action)
    {
        return Input.GetKeyDown(_actionsKeys[playerId][(int)action]);
    }

    public override bool GetKeyUp(string playerId, EInputAction action)
    {
        return Input.GetKeyUp(_actionsKeys[playerId][(int)action]);
    }

    #endregion

}
