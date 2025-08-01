using UnityEngine;

namespace Gehenna
{
    public class CommonUIModel : BaseUIModel
    {
        public string Text { get; set; }
        
        public string Image { get; set; }
        
        public string ButtonImage { get; set; }
        
        public string ButtonText { get; set; }
        
        public Vector2? TextPosition { get; set; }
        public Vector2? TextSize { get; set; }

        public Vector2? ImagePosition { get; set; }
        public Vector2? ImageSize { get; set; }

        public Vector2? ButtonPosition { get; set; }
        public Vector2? ButtonSize { get; set; }
    }
}