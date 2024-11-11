using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class Signal : ScriptableObject
{
    public List<SignalListener> Listeners = new List<SignalListener>();

    public void Raise(){
        Debug.Log("Signal Raised");
        for(int i =  Listeners.Count -1 ; i>= 0 ;i--){
            Listeners[i].OnSignalRaised();
        }
    }

    public void RegisterListener(SignalListener listener){
        Listeners.Add(listener);
    }

    public void DeRegisterListener(SignalListener listener){
        Listeners.Remove(listener);
    }
}
