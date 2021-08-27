using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PaperFold.UI
{
    public class ChildrenSpriteReplacer : MonoBehaviour
    {
        [SerializeField]
        private Sprite _spriteToReplace;
        [SerializeField, Header("Insert multiple items if need to set randomly.")]
        private List<Sprite> _spriteToSet;

        void Start()
        {
            var childSprites = GetComponentsInChildren<SpriteRenderer>().Where(renderer =>
                renderer.sprite == _spriteToReplace &&
                renderer.gameObject != gameObject
            );

            var sprite = _spriteToSet[Random.Range(0, _spriteToSet.Count)];

            foreach (var spriteRenderer in childSprites)
            {
                spriteRenderer.sprite = sprite;
            }

            Destroy(this);
        }
    }
}