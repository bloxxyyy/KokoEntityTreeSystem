namespace KokoEntityTreeSystem.tests;

using Xunit;
using KokoEntityTreeSystem.src.entity_data;
using KokoEntityTreeSystem.src.entity_tree_data;

public class EntityFactoryTests {

    private class MockEntityTree : EntityTree {
        public MockEntityTree(int identifier) : base(identifier) { }
    }

    private class MockEntity : Entity {
        public MockEntity(int identifier) : base(identifier) { }
    }

    [Fact]
    public void CreateEntity_AddToSingleTree_EntityAddedToCorrectTree() {
        // Arrange
        var entityTreeManager = new EntityTreeManager();
        var entityFactory = new EntityFactory(entityTreeManager);
        var tree = new MockEntityTree(1);
        var tree2 = new MockEntityTree(2);

        entityTreeManager.AddTree(tree);
        entityTreeManager.AddTree(tree2);

        // Act
        var entity = entityFactory.CreateEntity<MockEntity>(1, tree);

        // Assert
        Assert.Single(tree.GetEntities());
        Assert.Equal(entity, tree.GetEntity(1));

        Assert.Empty(tree2.GetEntities());
    }

    [Fact]
    public void CreateEntity_AddToMultipleTrees_EntityAddedToAllCorrectTrees() {
        // Arrange
        var entityTreeManager = new EntityTreeManager();
        var entityFactory = new EntityFactory(entityTreeManager);
        var tree1 = new MockEntityTree(1);
        var tree2 = new MockEntityTree(2);
        var tree3 = new MockEntityTree(3);

        entityTreeManager.AddTree(tree1);
        entityTreeManager.AddTree(tree2);
        entityTreeManager.AddTree(tree3);

        // Act
        var entity = entityFactory.CreateEntity<MockEntity>(1, tree1, tree2);

        // Assert
        Assert.Single(tree1.GetEntities());
        Assert.Equal(entity, tree1.GetEntity(1));

        Assert.Single(tree2.GetEntities());
        Assert.Equal(entity, tree2.GetEntity(1));

        Assert.Empty(tree3.GetEntities());
    }

    [Fact]
    public void DestroyEntity_EntityRemovedFromAllTrees() {
        // Arrange
        var entityTreeManager = new EntityTreeManager();
        var entityFactory = new EntityFactory(entityTreeManager);
        var tree1 = new MockEntityTree(1);
        var tree2 = new MockEntityTree(2);
        entityTreeManager.AddTree(tree1);
        entityTreeManager.AddTree(tree2);
        var entity = entityFactory.CreateEntity<MockEntity>(1, tree1, tree2);

        // Act
        entityFactory.DestroyEntity(entity);

        // Assert
        Assert.Empty(tree1.GetEntities());
        Assert.Empty(tree2.GetEntities());
    }

    [Fact]
    public void CreateEntity_InvalidTree_ThrowsException() {
        // Arrange
        var entityTreeManager = new EntityTreeManager();
        var entityFactory = new EntityFactory(entityTreeManager);

        // Act and assert
        var ex = Assert.Throws<InvalidOperationException>(() => {
            var entity = entityFactory.CreateEntity<MockEntity>(1, new MockEntityTree(1), new MockEntityTree(2));
        });

        Assert.Equal("Tree does not exist!", ex.Message);
    }

    [Fact]
    public void CreateEntity_NoTreesPassed_ThrowsInvalidOperationException() {
        // Arrange
        var entityTreeManager = new EntityTreeManager();
        var entityFactory = new EntityFactory(entityTreeManager);

        // Act and assert
        var ex = Assert.Throws<InvalidOperationException>(
            () => entityFactory.CreateEntity<MockEntity>(1)
        );

        Assert.Equal("must add to atleast 1 tree!", ex.Message);
    }
}