using PaperFold.UI;
using System;
using UnityEngine;

namespace PaperFold.Core
{
    [Serializable]
    public struct FoldOrder
    {
        [SerializeField]
        private FoldAnimator[] _foldAnimators;

        public FoldAnimator[] FoldAnimators => _foldAnimators;
    }
}