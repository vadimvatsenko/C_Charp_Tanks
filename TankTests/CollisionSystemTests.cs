using System.Diagnostics;
using C_Charp_Tanks.Blocks;
using C_Charp_Tanks.Engine;
using C_Charp_Tanks.Engine.Ray;
using C_Charp_Tanks.Fabrics;
using C_Charp_Tanks.Fabrics.BlocksFactory;
using C_Charp_Tanks.Fabrics.BulletsFactory;
using C_Charp_Tanks.Systems;
using C_Charp_Tanks.Venicals;
using C_Charp_Tanks.Venicals.Enemy;
using Xunit;

namespace C_Charp_Tanks.Tests
{
    public class CollisionSystemTests
    {
        private static CollisionSystem _collisionSystem = new CollisionSystem();
        private static ConsoleInput _consoleInput = new ConsoleInput();
        private static UnitFabric _unitFabric = new UnitFabric(_consoleInput, _collisionSystem);
        private static BlocksFabric _blockFabric = new BlocksFabric();
        private static BulletsFabric _bulletsFabric = new BulletsFabric();
        private static FabricController _fabricController = new FabricController(_unitFabric, _blockFabric, _bulletsFabric);

        [Fact]
        public void CheckUnitsCollider()
        {
            Player player = new Player(Vector2.Zero, _fabricController, _consoleInput, _collisionSystem);
            Enemy enemy = new Enemy(Vector2.One, _fabricController, _collisionSystem);
            
            bool isColliding = player.Collider.IsColliding(enemy.Collider);
            
            Assert.True(isColliding); // Позиция заблокирована
        }
        
        [Fact]
        public void CheckEnemyShootRayCast()
        {
            Player player = new Player(Vector2.Zero, _fabricController, _consoleInput, _collisionSystem);
            Enemy enemy = new Enemy(Vector2.Down * 10, _fabricController, _collisionSystem);
            
            RayCast rayCast = new RayCast(enemy.Position, Vector2.Up, 10);
            
            bool isPlayerDetection = rayCast.CheckDetection(player.Collider);
            
            Assert.True(isPlayerDetection); 
        }

        [Fact]
        public void CheckBulletsCollider()
        {
            Player player = new Player(Vector2.Zero, _fabricController, _consoleInput, _collisionSystem);
            Bullet bullet = new Bullet(Vector2.One, player.CurrentDirection);
            
            bool isPlayerDetection = bullet.Collider.IsColliding(player.Collider);
            
            Assert.True(isPlayerDetection);
        }
        
    }
}