using UnityEngine;

namespace Gehenna
{
    public class BaseUIModel
    {
        public Vector2? AnchoredPosition;
        public Vector2? SizeDelta;

        public BaseUIModel(Vector2? anchoredPosition = null, Vector2? sizeDelta = null)
        {
            AnchoredPosition= anchoredPosition;
            SizeDelta= sizeDelta;
        }
    }
}