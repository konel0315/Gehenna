using UnityEngine;

namespace Gehenna
{
    public interface IInputHandler
    {
        void Move(Vector2 direction);
        void Submit();
        void Cancel();
    }
}
