﻿using KokoEntityTreeSystem.src.entity_data;

namespace KokoEntityTreeSystem.src.entity_tree_data;

public abstract class EntityTree : IEntityObserver {
    public int Identifier { get; private set; }
    private Dictionary<int, Entity> _entities = new();

    protected EntityTree(int identifier) => Identifier = identifier;

    #region CRUD operations
    public void AddEntity(Entity entity) => _entities.Add(entity.Identifier, entity);
    public Entity GetEntity(int identifier) => _entities[identifier];
    public void OnEntityDestroyed(Entity entity) => RemoveEntity(entity);
    public void RemoveEntity(Entity entity) => _entities.Remove(entity.Identifier);

    public Entity[] GetEntities() {
        var entities = new Entity[_entities.Count];
        _entities.Values.CopyTo(entities, 0);
        return entities.ToArray();
    }

    #endregion
}