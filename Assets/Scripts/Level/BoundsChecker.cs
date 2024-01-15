using UnityEngine;

namespace ShootEmUp
{
    public sealed class BoundsChecker
    {
        private LevelBounds _levelBounds;
        public BoundsChecker(LevelBounds levelBounds)
        {
            _levelBounds = levelBounds;
        }

        public bool InBounds(Vector3 position)
        {
            var positionX = position.x;
            var positionY = position.y;
            return positionX > _levelBounds.LeftBorder.position.x
                   && positionX < _levelBounds.RightBorder.position.x
                   && positionY > _levelBounds.DownBorder.position.y
                   && positionY < _levelBounds.TopBorder.position.y;
        }
        public bool IsFreeByLeft(Vector2 position)
        {
            var positionX = position.x;
            return positionX > _levelBounds.LeftBorder.position.x;
        }
        public bool IsFreeByRight(Vector2 position)
        {
            var positionX = position.x;
            return positionX < _levelBounds.RightBorder.position.x;
        }
    }
}