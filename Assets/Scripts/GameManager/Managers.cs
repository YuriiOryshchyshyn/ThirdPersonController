using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMnager))]
[RequireComponent(typeof(InventoryManager))]
public class Managers : MonoBehaviour
{
    public static PlayerMnager Player {get; private set;}
    public static InventoryManager Inventory {get; private set;}

    private List<IGameManager> _startSequance;

    private void Awake()
    {
        Player = GetComponent<PlayerMnager>();
        Inventory = GetComponent<InventoryManager>();

        _startSequance = new List<IGameManager>();

        _startSequance.Add(Player);
        _startSequance.Add(Inventory);

        StartCoroutine(StartupManagers());
    }

    private IEnumerator StartupManagers()
    {
        foreach (IGameManager manager in _startSequance)
        {
            manager.Startup();
        }

        yield return null;

        int numModules = _startSequance.Count;
        int numReady = 0;

        while (numReady < numModules)
        {
            int lastReady = numReady;
            numReady = 0;

            foreach (IGameManager manager in _startSequance)
            {
                if (manager.status == ManagerStatus.Started)
                {
                    numReady++;
                }
            }

            if (numReady > lastReady)
            {
                print($"{numReady} / {numModules}");
                yield return null;
            }

            print("All Managers Started");
        }
    }
}
