using PaperFold.UI;
using System;
using UnityEngine;

namespace PaperFold.Core
{
    public class TapArea : MonoBehaviour
    {
        [SerializeField]
        private FoldAnimator _paperFold;

        public event Action<FoldAnimator> OnPaperFoldTapped;

        private void OnMouseDown()
        {
            OnPaperFoldTapped?.Invoke(_paperFold);
        }
    }
}