using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CustomUI
{

    public class Sprite
    {
        public Vector2Int textureSize;
        public Mesh mesh;
        public Vector3[] verticies;
        public Vector2[] uv;
        public int[] indicies;

        public Sprite()
        {
            mesh = new Mesh();
            textureSize = Vector2Int.zero;


        }
         ~Sprite()
        {
            mesh = null;
            verticies = null;
            uv = null;
            indicies = null;

        }
        public void CreateSprite(int width, int height)
        {

            textureSize = new Vector2Int(width, height);
            var halfTexSize = new Vector2Int(textureSize.x / 2, textureSize.y / 2);

            verticies = new[]
            {
                new Vector3(-halfTexSize.x,-halfTexSize.y),
                new Vector3(halfTexSize.x,-halfTexSize.y),
                new Vector3(-halfTexSize.x,halfTexSize.y),
                new Vector3(halfTexSize.x,halfTexSize.y)
            };

            uv = new[]
            {
                new Vector2(0,0),
                new Vector2(1,0),
                new Vector2(0,1),
                new Vector2(1,1),
            };

            indicies = new[] { 0, 1, 2, 2, 1, 3 };

            mesh.vertices = verticies;
            mesh.uv = uv;
            mesh.triangles = indicies;
        }
    }
}
