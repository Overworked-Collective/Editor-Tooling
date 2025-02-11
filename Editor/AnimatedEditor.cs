using UnityEngine;
using UnityEditor;
using Tooling.Extensions;

namespace Tooling.Editor
{
    public static class AnimatedEditor
    {
        public static bool DrawAnimatedToggle(Rect rect, bool value, Material material, string materialProperty, float animationSpeed = 0.5f)
        {
            //if mouse presses on the rect => toggle value
            if (Event.current.type == EventType.MouseDown && Event.current.button == 0 && rect.Contains(Event.current.mousePosition))
            {
                value = !value;
            }
            DrawAnimatedQuad(rect, value, material, materialProperty, animationSpeed);
            return value;
        }

        public static void DrawAnimatedQuad(Rect rect, bool value, Material material, string materialProperty, float animationSpeed = 0.5f)
        {
            float animatedValue = material.GetFloat(materialProperty);
            material.SetFloat(materialProperty, Mathf.Lerp(animatedValue, value.ToInt(), animationSpeed * (Time.deltaTime < 1f ? Time.deltaTime : 0)));

            GLLib.DrawQuad(rect, material, new Rect(0, 0, rect.width, rect.height));

            if (!Mathf.Approximately(material.GetFloat(materialProperty), value.ToInt()))
            {
                HandleUtility.Repaint();
            }
        }
    }
}
