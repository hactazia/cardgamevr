using System;
using System.Collections.Generic;
using CardGameVR.Players;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CardGameVR
{
    public class GameManager : MonoBehaviour
    {
        private const int LoadingSceneIndex = 0;
        private const int MainSceneIndex = 1;

        private static List<string> _oList = new();

#if UNITY_EDITOR
        private void OnGUI()
        {
            if (_oList.Count == 0) return;
            GUILayout.BeginArea(new Rect(10, 10, 200, 200));
            GUILayout.Label("Operations:");
            foreach (var operation in _oList)
                GUILayout.Label(operation);
            GUILayout.EndArea();
        }
#endif


        public static void AddOperation(string operation)
        {
            Debug.Log($"GameManager.AddOperation: {operation}");
            _oList.Add(operation);
        }

        public static void RemoveOperation(string operation)
        {
            Debug.Log($"GameManager.RemoveOperation: {operation}");
            _oList.Remove(operation);
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        public static void OnAfterAssembliesLoaded()
        {
            Debug.Log("GameManager.OnAfterAssembliesLoaded");
            _oList.Clear();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void OnAfterSceneLoad()
        {
            Debug.Log("GameManager.OnAfterSceneLoad");
            SceneManager.LoadScene(LoadingSceneIndex);
            var go = new GameObject($"[{nameof(GameManager)}]");
            go.AddComponent<GameManager>();
            DontDestroyOnLoad(go);
        }

        private void Start()
            => StartAsync().Forget();

        private async UniTask StartAsync()
        {
            Debug.Log("GameManager.StartAsync");
            await UniTask.WaitUntil(() => _oList.Count == 0);
            await SceneManager.LoadSceneAsync(MainSceneIndex);
            PlayerManager.player.Recenter();
        }
    }
}