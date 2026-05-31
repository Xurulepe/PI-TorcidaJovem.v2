using System;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGame.TecInformatica
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance {  get; private set; }

        [Header("Computer Settings")]
        [SerializeField] private List<DraggableItem> computerCompenentList = new List<DraggableItem>();

        private Computer computerStatus;
        private bool hasGameFinished;

        public bool HasGameFinished => hasGameFinished;
        public Computer ComputerStatus => computerStatus;

        public event Action OnPCChecked;
        public event Action OnGameFinished;

        private void Awake()
        {
            Instance = this;

            computerStatus = new Computer();
        }

        public void CheckPC()
        {
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
            int equipedComponentCount = computerStatus.GetEquipedComponentListCount();
            if (equipedComponentCount == computerCompenentList.Count)
            {
                hasGameFinished = true;

                OnGameFinished?.Invoke();
            }

            OnPCChecked?.Invoke();
        }

        public void AddComputerComponent(DraggableItem component)
        {
            computerCompenentList.Add(component);
        }

        public void EquipComputerComponent(ItemType component)
        {
            computerStatus.AddEquipedComponent(component);
        }

        public void RemoveComputerComponent(ItemType component)
        {
            computerStatus.RemoveEquipedComponent(component);
        }
    }
}
