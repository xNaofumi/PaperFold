using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace PaperFold.Core
{
    public class PaperSpawner : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> _prefabs;
        [SerializeField]
        private int _delayBetweenSpawns;

        private GameObject _currentPaper;

        void Start()
        {
            Spawn();
        }

        private async void PaperFoldedEventHandler(GameObject obj)
        {
            await SpawnAfterDelayAsync(_delayBetweenSpawns);
        }

        public void Spawn()
        {
            var index = UnityEngine.Random.Range(0, _prefabs.Count);
            var paperPrefab = _prefabs[index];

            _currentPaper = Instantiate(paperPrefab);
            _currentPaper.GetComponent<PaperDispatcher>().OnPaperFoldedCorrectly += PaperFoldedEventHandler;
        }

        public async Task SpawnAfterDelayAsync(int seconds)
        {
            await Task.Delay(seconds * 1000);

            _currentPaper.GetComponent<PaperDispatcher>().OnPaperFoldedCorrectly -= PaperFoldedEventHandler;
            Destroy(_currentPaper);
            Spawn();
        }
    }
}