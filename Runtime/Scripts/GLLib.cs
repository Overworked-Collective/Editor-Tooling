using UnityEngine;

namespace Tooling.Editor
{
    public static class GLLib
    {
        public static void DrawQuad(Rect rect, Material mat, Rect windowRect)
        {
            if (Event.current.type == EventType.Repaint)
            {
                GUI.BeginClip(windowRect);

                GL.MultMatrix(Matrix4x4.TRS(new Vector3(rect.x, rect.y < 0 ? 0 : rect.y, 0), Quaternion.identity, Vector3.one));
                GL.PushMatrix();

                //GL.Clear(true, false, Color.black);

                mat.SetPass(0);

                GL.Begin(GL.QUADS);

                GL.TexCoord(new Vector3(0, (rect.y < 0 ? -1*rect.y : 0)/rect.height, 0));
                GL.Vertex3(0, 0, 0);

                GL.TexCoord(new Vector3(1, (rect.y < 0 ? -1 * rect.y : 0)/ rect.height, 0));
                GL.Vertex3(rect.width, 0, 0);

                GL.TexCoord(new Vector3(1, 1, 0));
                GL.Vertex3(rect.width, rect.height + (rect.y < 0 ? rect.y : 0), 0);

                GL.TexCoord(new Vector3(0, 1, 0));
                GL.Vertex3(0, rect.height + (rect.y < 0 ? rect.y : 0), 0);

                GL.End();
                GL.PopMatrix();
                GUI.EndClip();
            }

            //DrawQuad(rect, mat, Vector2.one, Vector2.zero);
        }

        /// <summary>
        /// Render a quad with a material applied using the low level GL graphics class
        /// </summary>
        /// <param name="rect">Position and Scale of the quad</param>
        /// <param name="mat">Material applied to the quad</param>
        /// <param name="tiling">The tiling of the uv</param>
        /// <param name="offset">The offset of the uv</param>
        public static void DrawQuad(Rect rect, Material mat, Vector2 tiling, Vector2 offset)
        {
            if (Event.current.type == EventType.Repaint)
            {
                GUI.BeginClip(new Rect(rect.x, rect.y + 10, rect.width, rect.height));

                GL.PushMatrix();

                GL.Clear(true, false, Color.black);

                mat.SetPass(0);

                GL.Begin(GL.QUADS);

                Vector3 tiling3 = (Vector3)tiling;
                Vector3 offset3 = (Vector3)offset;

                GL.TexCoord(Vector3.Scale(new Vector3(0, 0, 0), tiling3) + offset3);
                GL.Vertex3(0, 0, 0);

                GL.TexCoord(Vector3.Scale(new Vector3(1, 0, 0), tiling3) + offset3);
                GL.Vertex3(rect.width, 0, 0);

                GL.TexCoord(Vector3.Scale(new Vector3(1, 1, 0), tiling3) + offset3);
                GL.Vertex3(rect.width, rect.height, 0);

                GL.TexCoord(Vector3.Scale(new Vector3(0, 1, 0), tiling3) + offset3);
                GL.Vertex3(0, rect.height, 0);

                GL.End();
                GUI.EndClip();

                GL.PopMatrix();
            }
        }
    }
}
