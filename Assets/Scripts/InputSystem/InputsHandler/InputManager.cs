using UnityEngine;

public abstract class InputManager : MonoBehaviour, IInputManager {

    private static InputManager _instance;

    public static IInputManager instance
    {
        get { return _instance; }
    }

    public static void SetInstance(InputManager instance)
    {
        if (InputManager._instance == instance)
        {
            return;
        }

        if (InputManager._instance != null)
        {
            InputManager._instance.enabled = false;
        }

        InputManager._instance = instance;
    }

    private bool dontDestroyOnLoad = true;

    protected virtual void Awake()
    {
        if (dontDestroyOnLoad)
        {
            DontDestroyOnLoad(this.transform.root.gameObject);
        }
    }

    public virtual bool IsEnabled
    {
        get
        {
            return this.isActiveAndEnabled;
        }
        set
        {
            this.enabled = value;
        }
    }

    protected virtual void Start() { }
    protected virtual void Update() { }
    protected virtual void OnEnable() { }
    protected virtual void OnDisable() { }
    protected virtual void OnDestroy() { }

    public abstract bool GetButton(int playerId, EInputAction action);
    public abstract bool GetButtonDown(int playerId, EInputAction action);
    public abstract bool GetButtonUp(int playerId, EInputAction action);
    public abstract float GetAxis(int playerId, EInputAction action);
    public abstract float GetAxisRaw(int playerId, EInputAction action);

    public abstract bool GetKey(string playerId, EInputAction action);
    public abstract bool GetKeyDown(string playerId, EInputAction action);
    public abstract bool GetKeyUp(string playerId, EInputAction action);
}
