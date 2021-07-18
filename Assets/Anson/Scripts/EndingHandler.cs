using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EndingOption
{
    public string endingName;
    public int counter;
    public int capacity;
}
public class EndingHandler : MonoBehaviour
{
    [SerializeField] List<EndingOption> endingOptions;

    public void IncrementEnding(int index)
    {
        if (index >= endingOptions.Count)
        {
            Debug.LogError("Ending error");
            return;
        }
        EndingOption e = endingOptions[index];
        e.counter++;
        if (e.counter == e.capacity)
        {
            FindObjectOfType<EndingPlayer>().PlayEnding(e.endingName);
            Debug.Log($"Play ending {e}");
            PlayerHandler.handler.InputLock(true);
        }
        
    }

}
