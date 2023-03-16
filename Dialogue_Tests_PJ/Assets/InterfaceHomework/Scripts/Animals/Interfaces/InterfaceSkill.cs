using UnityEngine;

namespace InterfaceHomework.Scripts.Animals.Interfaces
{
    public interface IFly
    {
        void Fly(E_Animals animal)
        {
            Debug.Log(animal + " Fly.");
        }
    }

    public interface ISwim
    {
        public void Swim(E_Animals animal)
        {
            Debug.Log(animal + " Swim.");
        }
    }

    public interface IJump
    {
        public void Jump(E_Animals animal)
        {
            Debug.Log(animal + " Jump.");
        }
    }
}