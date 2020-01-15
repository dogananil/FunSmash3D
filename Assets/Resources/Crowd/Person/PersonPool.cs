using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonPool : MonoBehaviour
{
    [SerializeField] private Person person;
    [NonSerialized] public static Stack<Person> pool=new Stack<Person>();
    [SerializeField] private int size;

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
