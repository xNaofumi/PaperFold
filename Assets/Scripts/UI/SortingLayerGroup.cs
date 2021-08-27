using System.Collections.Generic;
using UnityEngine;

namespace PaperFold.UI
{
    public class SortingLayerGroup : MonoBehaviour
    {
        private const int MAX_GROUP_ORDER_RANGE = 10;

        private IEnumerable<Renderer> _renderers;
        private IEnumerable<SpriteMask> _masks;

        private void Awake()
        {
            _renderers = GetComponentsInChildren<Renderer>();
            _masks = GetComponentsInChildren<SpriteMask>();
        }

        private void AddSortingOrder(int add)
        {
            foreach (var renderer in _renderers)
            {
                renderer.sortingOrder += add;
            }

            foreach (var mask in _masks)
            {
                // Order of setting the properties does matter.
                if (add > 0)
                {
                    mask.frontSortingOrder += add;
                    mask.backSortingOrder += add;
                }
                else
                {
                    mask.backSortingOrder += add;
                    mask.frontSortingOrder += add;
                }
            }
        }

        public void IncreaseSortingOrder(int multiplier)
        {
            AddSortingOrder(MAX_GROUP_ORDER_RANGE * multiplier);
        }

        public void DecreaseSortingOrder(int multiplier)
        {
            AddSortingOrder(-MAX_GROUP_ORDER_RANGE * multiplier);
        }
    }
}