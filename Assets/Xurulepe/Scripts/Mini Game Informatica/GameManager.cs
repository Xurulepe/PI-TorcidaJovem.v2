using System;
using UnityEngine;

namespace MiniGame.TecInformatica
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        private int incorrectItemCount = 0;

        public int IncorrectItemCount => incorrectItemCount;

        public event Action OnCheckPC;
        public event Action OnAfterPCChecked;
        public event Action OnGameFinished;

        private void Awake()
        {
            Instance = this;
        }

        public void CheckPC()
        {
            incorrectItemCount = 0;

            OnCheckPC?.Invoke();

            CheckForWin();
        }

        private void CheckForWin()
        {
            if (incorrectItemCount == 0)
            {
                OnGameFinished?.Invoke();
            }

            OnAfterPCChecked?.Invoke();
        }

        public void IncreaseIncorrectItemCount()
        {
            incorrectItemCount++;
        }
    }
}
