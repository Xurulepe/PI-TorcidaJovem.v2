using System.Collections.Generic;
using UnityEngine;

public class AreaReset : MonoBehaviour
{
    private Dictionary<GameObject, Vector3> posicoesIniciais = new Dictionary<GameObject, Vector3>();
    private List<GameObject> objetosDentro = new List<GameObject>();
    private void Start()
    {
        GameObject[] objetos = GameObject.FindGameObjectsWithTag("ObjetoArea");
        foreach (GameObject obj in objetos)
        {
            posicoesIniciais[obj] = obj.transform.position;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ObjetoArea"))
        {
            if(!objetosDentro.Contains(other.gameObject))
            {
                objetosDentro.Add(other.gameObject);

            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ObjetoArea"))
        {
            Debug.Log("Objeto saiu da area");
            ResetarObjetos();
        }
    }
    void ResetarObjetos()
    {
        foreach (GameObject obj in objetosDentro)
        {
            obj.transform.position = posicoesIniciais[obj];
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
            } 
        }
    }
}
