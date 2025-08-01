using System;

namespace Gehenna
{
    public class IntroUIModel : BaseUIModel
    {
        public float FadeInDuration;
        public float HoldDuration;
        public float FadeOutDuration;
        public Action OnComplete;
    }
}