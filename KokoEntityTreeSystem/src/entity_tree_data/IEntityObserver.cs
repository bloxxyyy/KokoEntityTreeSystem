using KokoEntityTreeSystem.src.entity_data;

namespace KokoEntityTreeSystem.src.entity_tree_data;

public interface IEntityObserver {
    void OnEntityDestroyed(Entity entity);
}