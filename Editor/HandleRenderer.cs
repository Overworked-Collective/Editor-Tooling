using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Tooling.Editor
{
    public static class HandleRenderer
    {
        static int _nearestHandle = -1;
        static Vector2 _previousMousePosition;

        static Vector3[] _handleDirections = new Vector3[4] { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };
        static Color _defaultColor = new Color(1, 1, 1, 0.75f);
        static Color _fadedColor = new Color(1, 1, 1, 0.35f);
        static Color _hoverColor = new Color(1, 1, 1, 1);
        static Color _activeColor = Color.yellow;

        public static float DrawCircularHandle(int[] handleIds, Transform center, float radius, bool fillCircle)
        {
            return DrawCircularHandle(handleIds, center.position, center.up, radius, fillCircle);
        }

        public static float DrawCircularHandle(int[] handleIds, Vector3 position, Vector3 normal, float radius, bool fillCircle)
        {
            Vector3[] handlePositions = new Vector3[4]
            {
                position + _handleDirections[0] * radius,
                position + _handleDirections[1] * radius,
                position + _handleDirections[2] * radius,
                position + _handleDirections[3] * radius
            };

            if (Event.current.type == EventType.Repaint)
            {
                if (fillCircle)
                {
                    Handles.color = new Color(1, 1, 1, 0.15f);
                    Handles.DrawSolidDisc(position, Vector3.up, radius);
                } 
                SetHandleColor(handleIds);
                Handles.DrawWireArc(position, Vector3.up, Vector3.forward, 360, radius);
                CreateHandleCaps(handleIds, handlePositions, Quaternion.identity, EventType.Repaint);

                Handles.color = _defaultColor;
                for (int i = 0; i < 4; i++)
                {
                    Handles.DrawLine(handlePositions[i], position);
                }
            }

            if (Event.current.type == EventType.Layout)
            {
                for (int i = 0; i < handleIds.Length; i++)
                {
                    CreateHandleCap(handleIds[i], handlePositions[i], Quaternion.identity, EventType.Layout);
                }
            }

            if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
            {
                _nearestHandle = HandleUtility.nearestControl;
                _previousMousePosition = Event.current.mousePosition;
            }

            if (Event.current.type == EventType.MouseUp && Event.current.button == 0)
            {
                _nearestHandle = -1;
                _previousMousePosition = Vector2.zero;
            }

            if (Event.current.type == EventType.MouseDrag && Event.current.button == 0)
            {
                if (handleIds.Contains(_nearestHandle))
                {
                    int index = FindIndex(handleIds, _nearestHandle);
                    float move = HandleUtility.CalcLineTranslation(_previousMousePosition, Event.current.mousePosition, position, _handleDirections[index]) * 0.5f;   

                    radius += move;
                    _previousMousePosition = Event.current.mousePosition;
                }
                HandleUtility.Repaint();
            }
            return radius;
        }

        private static int FindIndex(int[] values, int value)
        {
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] == value)
                {
                    return i;
                }
            }
            return -1;
        }

        private static void SetHandleColor(int[] handleIds)
        {
            Color color = _defaultColor;
            if (handleIds.Contains(HandleUtility.nearestControl)) { color = _hoverColor; }
            if (handleIds.Contains(_nearestHandle)) { color = _activeColor; }
            if (!handleIds.Contains(_nearestHandle) && _nearestHandle != -1) { color = _fadedColor; }
            Handles.color = color;
        }

        static void CreateHandleCap(int id, Vector3 position, Quaternion rotation, EventType eventType, float size = 0.075f)
        {
            Handles.DotHandleCap(id, position, rotation, size, eventType);
        }

        static void CreateHandleCaps(int[] ids, Vector3[] positions, Quaternion rotations, EventType eventType, float size = 0.075f)
        {
            for (int i = 0; i < ids.Length; i++)
            {
                CreateHandleCap(ids[i], positions[i], rotations, eventType, size);
            }
        }

        public static int[] GetHandleIDs(string uniqueIdentifier, int numberOfHandles)
        {
            int[] handles = new int[numberOfHandles];
            for (int i = 0; i < numberOfHandles; i++)
            {
                handles[i] = GUIUtility.GetControlID(new GUIContent(uniqueIdentifier), FocusType.Passive);
            }
            return handles;
        }
    }
}
