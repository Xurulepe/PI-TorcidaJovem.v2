using System;
using UnityEngine;

namespace MiniGame.TecInformatica
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        private int incorrectItemCount = 0;

        public event Action OnPCChecked;
        public event Action OnGameFinished;

        private void Awake()
        {
            Instance = this;
        }

        public void CheckPC()
        {
            incorrectItemCount = 0;

            OnPCChecked?.Invoke();

            CheckForWin();
        }

        private void CheckForWin()
        {
            if (incorrectItemCount == 0)
            {
                OnGameFinished?.Invoke();
            }
        }

        public void IncreaseIncorrectItemCount()
        {
            incorrectItemCount++;
        }
    }
}
