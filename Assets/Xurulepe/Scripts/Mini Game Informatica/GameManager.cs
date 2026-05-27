using System;
using UnityEngine;

namespace MiniGame.TecInformatica
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance {  get; private set; }

        private int incorrectItemCount = 0;
        private Computer computerStatus;

        public int IncorrectItemCount => incorrectItemCount;
        public Computer ComputerStatus => computerStatus;

        public event Action OnCheckPC;
        public event Action OnAfterPCChecked;
        public event Action OnGameFinished;

        private void Awake()
        {
            Instance = this;

            computerStatus = new Computer();
        }

        public void CheckPC()
        {
            incorrectItemCount = 0;

            OnCheckPC?.Invoke();

            computerStatus.SetRequiredComponentText();

            // debug
            //foreach (ItemType component in computerStatus.GetEquipedComponents())
            //{
            //    Debug.Log("Has " + component);
            //}

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

        public void AddComputerComponent(ItemType component)
        {
            computerStatus.AddEquipedComponent(component);
        }

        public void RemoveComputerComponent(ItemType component)
        {
            computerStatus.RemoveEquipedComponent(component);
        }
    }
}
