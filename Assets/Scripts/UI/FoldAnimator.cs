using PaperFold.Core;
using System.Collections;
using UnityEngine;

namespace PaperFold.UI
{
    [RequireComponent(typeof(SortingLayerGroup))]
    [RequireComponent(typeof(MaskRootMovement))]
    public class FoldAnimator : MonoBehaviour
    {
        public SortingLayerGroup SortingGroup { get; private set; }

        private Transform _maskRoot;
        private MaskRootMovement _maskMovement;

        private void Awake()
        {
            _maskRoot = transform;
            _maskMovement = GetComponent<MaskRootMovement>();
            SortingGroup = GetComponent<SortingLayerGroup>();
        }

        private void StartMoving(Vector2 destination)
        {
            var coroutine = Move(_maskRoot, destination);
            StartCoroutine(coroutine);
        }

        private IEnumerator Move(Transform transform, Vector2 destination)
        {
            var initialDuration = 0.5f;
            var duration = initialDuration;
            var startingPosition = transform.position;

            while (duration > 0f)
            {
                transform.position = Vector2.Lerp(startingPosition, destination, 1f - duration / initialDuration);

                duration -= Time.deltaTime;
                yield return null;
            }

            transform.position = destination;
        }

        public void Fold()
        {
            StartMoving(_maskMovement.Destination);
        }

        public void Unfold()
        {
            StartMoving(_maskMovement.InitialPosition);
        }
    }
}