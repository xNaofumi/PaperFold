using PaperFold.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PaperFold.Core
{
    public class PaperDispatcher : MonoBehaviour
    {
        [SerializeField]
        private FoldOrder[] _foldAnimatorsOrder;
        [SerializeField]
        private List<TapArea> _tapAreas;

        public event Action<GameObject> OnPaperFoldedCorrectly;

        private Stack<FoldAnimator> _foldStack;

        private void Start()
        {
            _foldStack = new Stack<FoldAnimator>();
        }

        private void OnEnable()
        {
            foreach (var tapArea in _tapAreas)
            {
                tapArea.OnPaperFoldTapped += FoldEventHandler;
            }
        }

        private void OnDisable()
        {
            foreach (var tapArea in _tapAreas)
            {
                tapArea.OnPaperFoldTapped -= FoldEventHandler;
            }
        }

        private void FoldEventHandler(FoldAnimator foldAnimator)
        {
            if (_foldStack.Count == 0)
            {
                Fold(foldAnimator);
                return;
            }

            var isAlreadyInQueue = _foldStack.Contains(foldAnimator);
            var isOnTopOfQueue = _foldStack.Peek() == foldAnimator;

            if (isOnTopOfQueue)
            {
                Unfold(foldAnimator);
                return;
            }

            if (isAlreadyInQueue)
            {
                UnfoldAll();
            }
            else
            {
                Fold(foldAnimator);
                CheckIfFoldedCorrectly();
            }
        }

        private void CheckIfFoldedCorrectly()
        {
            var foldsCount = _foldStack.Count;
            var foldsNeeded = _foldAnimatorsOrder[0].FoldAnimators.Length;
            if (foldsCount < foldsNeeded) return;

            // Note that the array is inverted since it's a stack, so
            // we need to iterate through it from the end.
            var foldArray = _foldStack.ToArray();

            foreach (var foldOrder in _foldAnimatorsOrder)
            {
                var isCorrectOrder = true;

                var length = foldArray.Length;
                for (int i = 0; i < length; i++)
                {
                    if (foldArray[i] != foldOrder.FoldAnimators[length - i - 1])
                    {
                        isCorrectOrder = false;
                    }
                }

                if (isCorrectOrder)
                {
                    OnPaperFoldedCorrectly?.Invoke(gameObject);
                    return;
                }
            }
        }

        private void Fold(FoldAnimator foldAnimator)
        {
            foldAnimator.Fold();
            foldAnimator.SortingGroup.IncreaseSortingOrder(_foldStack.Count + 1);
            _foldStack.Push(foldAnimator);
        }

        private void Unfold(FoldAnimator foldAnimator)
        {
            foldAnimator.Unfold();
            foldAnimator.SortingGroup.DecreaseSortingOrder(_foldStack.Count);
            _foldStack.Pop();
        }

        private void UnfoldAll()
        {
            while (_foldStack.Count > 0)
            {
                var paper = _foldStack.Peek();
                Unfold(paper);
            }
        }
    }
}