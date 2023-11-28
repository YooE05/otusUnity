using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


namespace ShootEmUp
{
    public class EnemiesMoveController : MonoBehaviour
    {
        public Action<GameObject> OnEnemyReachPosition;


        private Dictionary<MoveComponent, FlyProgress> enemies = new();

        private void FixedUpdate()
        {
            TranslateEnemies();
        }


        public void TrackEnemy(MoveComponent newEnemy, Vector2 endPoint)
        {
            this.enemies.TryAdd(newEnemy, new FlyProgress());
            SetDestination(newEnemy, endPoint);
        }


        public void SetDestination(MoveComponent enemyMove, Vector2 endPoint)
        {
            this.enemies[enemyMove].destination = endPoint;
            this.enemies[enemyMove].isReached = false;
        }

        public void TranslateEnemies()
        {
            foreach (var enemy in enemies)
            {
                if (!enemy.Value.isReached)
                {
                    var vector = enemy.Value.destination - (Vector2)enemy.Key.transform.position;
                    if (vector.magnitude <= 0.25f)
                    {
                        enemy.Value.isReached = true;
                        OnEnemyReachPosition?.Invoke(enemy.Key.gameObject);
                        return;
                    }
                    else
                    {
                        var direction = vector.normalized * Time.fixedDeltaTime;
                        enemy.Key.MoveByRigidbody(direction);
                    }

                }

            }
        }


        private class FlyProgress
        {
            public FlyProgress()
            { isReached = true; }


            public Vector2 destination;
            public bool isReached;
        }
    }

}
