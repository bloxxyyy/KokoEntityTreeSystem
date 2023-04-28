using KokoEntityTreeSystem.src.entity_data;

namespace KokoEntityTreeSystem.src.entity_tree_data;

/// <summary>
/// The EntityTreeManager class is designed to manage a collection of objects that
/// implement EntityTree, which are used to store and organize Entity objects.
/// This class is implemented to provide an efficient and centralized way of managing
/// the EntityTree objects, making it easier to
/// add, remove, and retrieve Entity objects from them.
/// </summary>
public class EntityTreeManager {
    private readonly Dictionary<int, EntityTree> _entityTrees = new();
    private readonly Action<string> _handleError;
    
    public EntityTreeManager(Action<string> errorHandleFunction) {
        _handleError = errorHandleFunction;
    }

    /// <summary>
    /// Checks if each specified EntityTree exists in the manager.
    /// If so, adds the specified Entity to each EntityTree and registers
    /// the Entity as an observer for each specified tree.
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="trees"></param>
    internal void AddEntityToTrees(Entity entity, params EntityTree[] trees) {
        foreach (var tree in trees) {
            if (!_entityTrees.ContainsKey(tree.Identifier)) {
                _handleError("Tree does not exist in manager!");
                break;
            }
            
            entity.RegisterObserver(tree);
            _entityTrees[tree.Identifier].AddEntity(entity);
        }
    }

    #region CRUD operations
    
    public void AddTree(EntityTree tree) => _entityTrees.Add(tree.Identifier, tree);
    public void RemoveTree(EntityTree tree) => _entityTrees.Remove(tree.Identifier);
    public EntityTree GetTree(int identifier) => _entityTrees[identifier];

    public T? GetTree<T>(int identifier) where T : EntityTree {
        if (_entityTrees.TryGetValue(identifier, out var tree) && tree is T tTree)
            return tTree;
        
        return null;
    }

    public EntityTree[] GetTrees() {
        var trees = new EntityTree[_entityTrees.Count];
        _entityTrees.Values.CopyTo(trees, 0);
        return trees.ToArray();
    }
    
    #endregion
}
