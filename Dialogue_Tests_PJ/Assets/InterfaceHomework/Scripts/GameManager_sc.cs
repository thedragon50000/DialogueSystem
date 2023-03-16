using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using InterfaceHomework.Scripts.Animals;
using InterfaceHomework.Scripts.Animals.Interfaces;
using UniRx;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GameManager_sc : MonoBehaviour
{
    public E_Animals eAnimals;
    private BaseAnimal _animal;

    // Start is called before the first frame update
    void Start()
    {
        InitAnimal();
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    switch (keyCode)
                    {
                        case KeyCode.Q:
                            print("Press Q.");
                            _animal.Actions[0].Invoke();
                            break;
                        case KeyCode.W:
                            print("Press W.");
                            _animal.Actions[1].Invoke();
                            break;
                        case KeyCode.E:
                            print("Press E.");
                            _animal.Actions[2].Invoke();
                            break;
                        case KeyCode.R:
                            print("Press R.");
                            _animal.Actions[3].Invoke();
                            break;
                        case KeyCode.D:
                            print("Press D.");
                            if (_animal.Actions.Length < 5)
                            {
                                print(eAnimals + " Idle.");
                            }
                            else
                            {
                                _animal.Actions[4].Invoke();
                            }

                            break;
                    }
                }
            }
        }
    }

    private void InitAnimal()
    {
        Action[] temp;
        ISwim swim;
        IJump jump;
        IFly fly;
        Action[] actions;
        switch (eAnimals)
        {
            case E_Animals.frog:
                _animal = new Frog(eAnimals);
                _animal.Start();
                swim = (ISwim) _animal;
                jump = (IJump) _animal;

                temp = new[] {(Action) (() => swim.Swim(eAnimals)), () => jump.Jump(eAnimals)};
                actions = _animal.Actions.Concat(temp).ToArray();
                _animal.Actions = actions;
                InitKey(actions);

                break;
            case E_Animals.goose:
                _animal = new Goose(eAnimals);
                _animal.Start();
                swim = (ISwim) _animal;
                fly = (IFly) _animal;

                temp = new[] {(Action) (() => swim.Swim(eAnimals)), () => fly.Fly(eAnimals)};
                actions = _animal.Actions.Concat(temp).ToArray();
                _animal.Actions = actions;
                InitKey(actions);

                break;
            case E_Animals.kangaroo:
                _animal = new Kangaroo(eAnimals);
                _animal.Start();
                jump = (IJump) _animal;
                temp = new[] {(Action) (() => jump.Jump(eAnimals))};
                actions = _animal.Actions.Concat(temp).ToArray();
                _animal.Actions = actions;
                InitKey(actions);

                break;
            case E_Animals.owl:
                _animal = new Owl(eAnimals);
                _animal.Start();
                fly = (IFly) _animal;
                temp = new[] {(Action) (() => fly.Fly(eAnimals))};
                actions = _animal.Actions.Concat(temp).ToArray();
                _animal.Actions = actions;
                InitKey(actions);

                break;
            case E_Animals.shark:
                _animal = new Shark(eAnimals);
                _animal.Start();
                swim = (ISwim) _animal;
                temp = new[] {(Action) (() => swim.Swim(eAnimals))};
                actions = _animal.Actions.Concat(temp).ToArray();
                _animal.Actions = actions;
                InitKey(actions);
                break;
            default:
                break;
        }

        _animal.StrName = eAnimals.ToString();
    }

    private void InitKey(Action[] actions)
    {
        int i = 0;
        foreach (Action action in actions)
        {
        }
    }
}

public enum E_Animals
{
    frog,
    shark,
    kangaroo,
    owl,
    goose,
    MAX
}