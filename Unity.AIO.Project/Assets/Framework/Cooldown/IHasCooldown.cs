/// <summary>
/// Interface for classes that have cooldown abilities
/// </summary>
public interface IHasCooldown
{
    string Id { get; }
    float CooldownDuration { get; }
}