using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class PersonPool : MonoBehaviour
{
    [SerializeField] private Person person;
    [NonSerialized] public Stack<Person> pool=new Stack<Person>();
    [SerializeField] private int size;
    public static PersonPool INSTANCE;

    private void Awake()
    {
        INSTANCE = this;
    }
    

    private void CombineMeshes()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);

            i++;
        }
        transform.GetComponent<MeshFilter>().mesh = new Mesh();
        transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
        transform.gameObject.SetActive(true);
    }
    public void InitializePersonPool()
    {
        for (int i = 0; i < size; i++)
        {
            Person person = Instantiate(this.person, this.transform);
           
            person.gameObject.SetActive(false);
            pool.Push(person);
        }
    }
   /* public void TakePerson(Person newPerson)
    {

        pool.Push(newPerson);
    }

    public Person GivePerson()
    {
        return pool.Pop();
    }*/

    public bool ListIsEmpty()
    {
        return pool.Count == 0;
    }
}


