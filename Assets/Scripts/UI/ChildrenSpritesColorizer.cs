using System.Linq;
using UnityEngine;

namespace PaperFold.UI
{
    public class ChildrenSpritesColorizer : MonoBehaviour
    {
        [SerializeField]
        private Sprite _spriteToColorize;
        [SerializeField]
        private bool _setRandomColor;
        [SerializeField]
        private Color _color;

        void Start()
        {
            var childSprites = GetComponentsInChildren<SpriteRenderer>().Where(renderer =>
                renderer.sprite == _spriteToColorize &&
                renderer.gameObject != gameObject
            );

            if (_setRandomColor)
            {
                GenerateRandomColor();
            }

            foreach (var spriteRenderer in childSprites)
            {
                spriteRenderer.color = _color;
            }

            Destroy(this);
        }

        private void GenerateRandomColor()
        {
            var minHue = 0.5f;
            var maxHue = 0.75f;
            _color = Random.ColorHSV(minHue, maxHue, minHue, maxHue, 0.3f, 1f);
        }
    }
}