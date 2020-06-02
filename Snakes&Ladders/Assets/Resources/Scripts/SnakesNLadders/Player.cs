using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AGAC.SnakesLadders.Players
{
    [DisallowMultipleComponent]
    public class Player : MonoBehaviour
    {
        public int CurrentTile = 0;

        [SerializeField]
        private Renderer renderer;
        public Color Color { get; private set; }

        public void MoveTo(Transform point, float speed = 1) 
        {
            Vector3 direction = (point.position - transform.position).normalized;
            transform.position += direction * speed;
        }

        public void ChangeColor(Color color) 
        {
            SetRenderer();
            if (renderer == null) return;
            Color = color;
            renderer.materials[0].SetColor("_Color", color);
        }

        private void SetRenderer()
        {
            if (renderer != null) return;
            renderer = GetComponent<Renderer>();
        }
    }
}