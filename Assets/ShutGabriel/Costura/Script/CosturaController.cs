using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CosturaController : MonoBehaviour
{
    public List<DragDrop> OBJDentro = new List<DragDrop>();
    public List<DragDrop> OBJFora = new List<DragDrop>();
    public List<DragDrop> objFisicos = new List<DragDrop>();
    [SerializeField] public GameObject botaoFinal;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Recortes()
    {
        for (int i = 0; i < objFisicos.Count; i++)
        {
            objFisicos[i].MudarImgff();
        }
    }
}
