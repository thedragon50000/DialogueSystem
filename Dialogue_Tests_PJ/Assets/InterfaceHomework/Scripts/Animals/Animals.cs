using System;
using System.Collections.Generic;
using System.Linq;
using InterfaceHomework.Scripts.Animals.Interfaces;
using UnityEngine;

namespace InterfaceHomework.Scripts.Animals
{
    public class Frog : BaseAnimal, ISwim, IJump
    {
        public Frog(E_Animals e) : base(e)
        {
        }
    }

    public class Goose : BaseAnimal, ISwim, IFly
    {
        public Goose(E_Animals e) : base(e)
        {
        }
    }

    public class Kangaroo : BaseAnimal, IJump
    {
        public Kangaroo(E_Animals e) : base(e)
        {
        }

        public override void Start()
        {
            base.Start();
            Action[] temp = Actions.Concat(new[] {(Action) BabySit}).ToArray();
            Actions = temp;
        }

        void BabySit()
        {
            Debug.Log(_eAnimals + " Babysit.");
        }
    }

    public class Owl : BaseAnimal, IFly
    {
        public Owl(E_Animals e) : base(e)
        {
        }
    }

    public class Shark : BaseAnimal, ISwim
    {
        public Shark(E_Animals e) : base(e)
        {
        }
    }
}