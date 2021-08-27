using UnityEngine;

namespace PaperFold.Core
{
    public class MaskRootMovement : MonoBehaviour
    {
        [SerializeField]
        private Transform _foldTransform;
        [SerializeField]
        private Vector2 _rootDestinationForOffsetCalculation;
        [SerializeField]
        private Vector2 _foldDestinationForOffsetCalculation;
        [SerializeField]
        private Vector2 _destination;

        public Vector2 InitialPosition => _initialPosition;
        public Vector2 Destination => _destination;

        private Vector2 _initialPosition;
        private Vector2 _foldInitialPosition;

        private void Start()
        {
            _initialPosition = transform.position;
            _foldInitialPosition = _foldTransform.localPosition;
        }

        private void Update()
        {
            if (!transform.hasChanged) return;

            UpdateFoldPosition();

            transform.hasChanged = false;
        }

        private void UpdateFoldPosition()
        {
            var overallDistance = Vector2.Distance(_initialPosition, _rootDestinationForOffsetCalculation);
            var localDistance = Vector2.Distance(transform.position, _rootDestinationForOffsetCalculation);
            var distanceNormalized = 1f - (localDistance / overallDistance);

            var newPosition = Vector2.Lerp(_foldInitialPosition, _foldDestinationForOffsetCalculation, distanceNormalized);
            _foldTransform.localPosition = newPosition;
        }
    }
}