/// <summary>
/// Inteface for pooled objects. Suggest to implement two methods: Reuse(), OnReuse().
/// </summary>
public interface IPoolObject {

    /// <summary>
    /// Use this method if you need to reactivate object.
    /// </summary>
    void OnReuse(); //already public, must have new implementation

    /// <summary>
    /// This method is suggested to be done after Reuse() method invoked. 
    /// Additional method that can be combined with OnEnable() MonoBehaviour function.
    /// </summary>
    void Reuse(); //already public, must have new implementation

    /// <summary>
    /// This method is suggested to be done after Reuse() method invoked. 
    /// Additional method that can be combined with OnEnable() MonoBehaviour function.
    /// </summary>
    void SetParent(UnityEngine.Transform parent);
}
