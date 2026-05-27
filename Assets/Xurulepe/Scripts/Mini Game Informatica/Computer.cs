using System.Collections.Generic;
using UnityEngine;

namespace MiniGame.TecInformatica
{
    public class Computer
    {
        private List<ItemType> equipedComponentList = new List<ItemType>();
        private string requiredComponentText;

        public string RequiredComponentText => requiredComponentText;


        public void AddEquipedComponent(ItemType component)
        {
            if (!equipedComponentList.Contains(component))
            {
                equipedComponentList.Add(component);
            }
        }

        public void RemoveEquipedComponent(ItemType component)
        {
            if (equipedComponentList.Contains(component))
            {
                equipedComponentList.Remove(component);
            }
        }

        public List<ItemType> GetEquipedComponents()
        {
            return equipedComponentList;
        }

        public void SetRequiredComponentText()
        {
            if (!equipedComponentList.Contains(ItemType.Motherboard))
            {
                requiredComponentText = "O computador precisa de uma placa-mãe.";
            }
            else if (!equipedComponentList.Contains(ItemType.PSU))
            {
                requiredComponentText = "O computador precisa de energia.";
            }
            else if (!equipedComponentList.Contains(ItemType.CPU))
            {
                requiredComponentText = "O computador precisa de uma unidade de processamento para funcionar.";
            }
            else if (!equipedComponentList.Contains(ItemType.RAM))
            {
                requiredComponentText = "O computador precisa de memórias RAM.";
            }
            else if (!equipedComponentList.Contains(ItemType.GPU))
            {
                requiredComponentText = "O computador não possui video integrado.";
            }
            else if (!equipedComponentList.Contains(ItemType.Cooler))
            {
                requiredComponentText = "O computador precisa de um resfriamento na sua unidade de processamento.";
            }
            else
            {
                requiredComponentText = "Nenhum problema encontrado.";
            }
        }
    }
}
