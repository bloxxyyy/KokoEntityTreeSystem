using KokoEntityTreeSystem.src.entity_tree_data;

namespace KokoEntityTreeSystem.src.entity_data;

/// <summary>
/// The base class for all entities.
/// Uses observer pattern to notify the entity tree when it is destroyed.
/// Since when an entity is destroyed it should not be alive in any tree anymore.
/// </summary>
public abstract class Entity {
    public int Identifier { get; }
    private List<IEntityObserver> _observers = new List<IEntityObserver>();

    protected Entity(int identifier) {
        Identifier = identifier;
    }

    public void RegisterObserver(IEntityObserver observer) =>_observers.Add(observer);
    public void NotifyObservers() => _observers.ForEach(o => o.OnEntityDestroyed(this));
}
