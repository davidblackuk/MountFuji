namespace MountFuji.Services;

/// <summary>
/// We can't wire up the Application class for injection as it's created after the DI
/// container is built.
///
/// This interface allows us to wrap the static Application.Current with an injectable
/// resolver that uses the static method, BUT, can have the implementation swapped out
/// with another in a unit test
/// </summary>
public interface IApplicationResolver
{
    /// <summary>
    /// Gets the application.
    /// </summary>
    Application Application { get; }
}