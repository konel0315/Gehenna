using UnityEngine;

namespace Gehenna
{
    public class Grid : MonoBehaviour
    {
        private GridModel model;
        
        public void Initialize(GridModel model)
        {
            this.model = model;
        }

        public void CleanUp()
        {
            model = null;
        }

        public Vector3 GridToWorld()
        {
            return default;
        }

        public Vector3 WorldToGrid()
        {
            return default;
        }
    }
}