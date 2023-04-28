using KokoEntityTreeSystem.src.entity_tree_data;

namespace KokoEntityTreeSystem.src.entity_data;

/// <summary>
/// the EntityFactory class provides a convenient way of creating and destroying Entity objects
/// while ensuring that they are properly registered with the appropriate EntityTree objects.
/// </summary>
public class EntityFactory {
    private readonly EntityTreeManager _EntityTreeManager;
    private readonly Action<string> _handleError;

    public EntityFactory(EntityTreeManager entityTreeManager, Action<string> errorHandleFunction) {
        _EntityTreeManager = entityTreeManager;
        _handleError = errorHandleFunction;
    }


    /// <summary>
    /// This code provides a method for creating new Entity objects of a specific type
    /// with a specified set of constructor arguments, and adds them to one or more EntityTree objects.
    /// Entities should always contain an identifier.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="trees"></param>
    /// <param name="entityParams"></param>
    /// <returns></returns>
    public T CreateEntity<T>(EntityTree[] trees, params object[] entityParams) where T : Entity {
        if (trees.Length < 1) _handleError("Entity made but not added to any tree!");

        var constructor = typeof(T).GetConstructor(entityParams.Select(arg => arg.GetType()).ToArray());
        if (constructor == null) {
            _handleError("No constructor found that matches the provided arguments!");
            return null;
        }
        
        var entity = (T)constructor.Invoke(entityParams);
        
        _EntityTreeManager.AddEntityToTrees(entity, trees);
        return entity;
    }

    public void DestroyEntity(Entity entity) {
        entity.NotifyDestruction();
    }
}
