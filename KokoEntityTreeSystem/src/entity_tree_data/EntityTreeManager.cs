using KokoEntityTreeSystem.src.entity_data;

namespace KokoEntityTreeSystem.src.entity_tree_data;

public class EntityTreeManager {
    private Dictionary<int, EntityTree> _entityTrees = new();
    private Func<string, object> _handleError;
    
    public EntityTreeManager(Func<string, object> errorHandleFunction) {
        _handleError = errorHandleFunction;
    }

    internal void AddEntityToTrees(Entity entity, params EntityTree[] trees) {
        foreach (var tree in trees) {
            if (!_entityTrees.ContainsKey(tree.Identifier))
                _handleError("Tree does not exist!");
            
            entity.RegisterObserver(tree);
            _entityTrees[tree.Identifier].AddEntity(entity);
        }
    }

    #region CRUD operations
    
    public void AddTree(EntityTree tree) => _entityTrees.Add(tree.Identifier, tree);
    public void RemoveTree(EntityTree tree) => _entityTrees.Remove(tree.Identifier);
    public EntityTree GetTree(int identifier) => _entityTrees[identifier];

    public EntityTree[] GetTrees() {
        var trees = new EntityTree[_entityTrees.Count];
        _entityTrees.Values.CopyTo(trees, 0);
        return trees.ToArray();
    }

    #endregion
}
