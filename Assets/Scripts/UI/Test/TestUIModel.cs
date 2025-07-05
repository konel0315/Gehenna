using UnityEngine;

namespace Gehenna
{
    public class TestUIModel : BaseUIModel
    {
        public string Speaker;
        public string Text;
        public string Portrait;
        public TestUIModel( string speaker, string text, string portrait ,Vector2? size = null, Vector2? position = null)
        {
            Speaker = speaker;
            Text = text;
            Portrait = portrait;
            
            SizeDelta = size;
            AnchoredPosition = position;
        }
    }
}