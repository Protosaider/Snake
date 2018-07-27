public interface IInputManager
{
	bool IsEnabled { get; set; }

	bool GetButton(int playerId, EInputAction action);
	bool GetButtonDown(int playerId, EInputAction action);
	bool GetButtonUp(int playerId, EInputAction action);
	float GetAxis(int playerId, EInputAction action);
	float GetAxisRaw(int playerId, EInputAction action);

    bool GetKey(string playerId, EInputAction action);
    bool GetKeyDown(string playerId, EInputAction action);
    bool GetKeyUp(string playerId, EInputAction action);
}
