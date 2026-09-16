using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CosturaController : MonoBehaviour
{
    public List<DragDrop> OBJDentro = new List<DragDrop>();
    public List<DragDrop> OBJFora = new List<DragDrop>();
    public List<DragDrop> objFisicos = new List<DragDrop>();
    public List<DragDrop> ObjInventario = new List<DragDrop>();
    [SerializeField] public GameObject botaoFinal;
   
    public void Recortes()
    {
        for (int i = 0; i < objFisicos.Count; i++)
        {
            objFisicos[i].MudarImgff();
        }
    }


    public void sairMenu()
    {
        SceneManager.LoadScene("Cenas_select");
    }
}
