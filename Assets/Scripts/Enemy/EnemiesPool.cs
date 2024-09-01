using System.Collections.Generic;
using System.Linq;

namespace Enemy
{
    public class EnemiesPool
    {
        public List<EnemyController> Pool => _enemies;
        private List<EnemyController> _enemies;

        public void Init(IEnumerable<EnemyController> enemyControllers)
        {
            _enemies = enemyControllers.ToList();
        }

        public bool TryGetAvailableEnemy(out EnemyController enemyController)
        {
            enemyController = _enemies.FirstOrDefault(component => !component.gameObject.activeSelf);
            if (enemyController == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public int ActiveEnemies()
        {
            return _enemies.Count(controller => controller.gameObject.activeSelf);
        }
    }
}