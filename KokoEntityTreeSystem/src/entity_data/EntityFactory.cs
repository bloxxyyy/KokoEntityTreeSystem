using KokoEntityTreeSystem.src.entity_tree_data;

namespace KokoEntityTreeSystem.src.entity_data;

public class EntityFactory {
    private EntityTreeManager _EntityTreeManager;

    public EntityFactory(EntityTreeManager entityTreeManager) {
        _EntityTreeManager = entityTreeManager;
    }

    public T CreateEntity<T>(int identifier, params EntityTree[] trees) where T : Entity {
        if (trees.Length < 1) throw new InvalidOperationException("must add to atleast 1 tree!");
        var entity = (T)Activator.CreateInstance(typeof(T), identifier);
        _EntityTreeManager.AddEntityToTrees(entity, trees);
        return entity;
    }

    public void DestroyEntity(Entity entity) {
        entity.NotifyObservers();
    }
}
