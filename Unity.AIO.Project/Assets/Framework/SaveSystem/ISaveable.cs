/// <summary>
/// Used to implement in classes that needs to have data saved.
/// </summary>
public interface ISaveable
{
    /// <summary>
    ///
    /// </summary>
    /// <returns>Returns an object because this system is used for multiple scenarios</returns>
    object SaveState();

    /// <summary>
    ///
    /// </summary>
    /// <param name="state"></param>
    void LoadState(object state);
}