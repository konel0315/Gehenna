using System;
using Sirenix.OdinInspector;
using UnityEditor.Rendering;
using UnityEngine;

namespace Gehenna
{
    [Serializable]
    public class CameraSetting
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public ProjectionType ProjectionType;
        
        [ShowIf(nameof(IsOrthographic))]
        public float Size;

        [ShowIf(nameof(IsPerspective))]
        public float FieldOfView;

        private bool IsOrthographic => ProjectionType == ProjectionType.Orthographic;
        private bool IsPerspective => ProjectionType == ProjectionType.Perspective;
        
        public void ApplyTo(Camera camera)
        {
            camera.transform.SetLocalPositionAndRotation(Position, Rotation);

            switch (ProjectionType)
            {
                case ProjectionType.Orthographic:
                    camera.orthographic = true;
                    camera.orthographicSize = Size;
                    break;
                case ProjectionType.Perspective:
                    camera.orthographic = false;
                    camera.fieldOfView = FieldOfView;
                    break;
            }
        }
    }
}