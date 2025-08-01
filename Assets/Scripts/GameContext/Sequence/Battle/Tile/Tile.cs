using UnityEngine;

namespace Gehenna
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        private TileConfig config;
        
        public void Initialize(TileConfig config)
        {
            this.config = config;
        }

        public void CleanUp()
        {
            this.config = null;
        }
    }
}