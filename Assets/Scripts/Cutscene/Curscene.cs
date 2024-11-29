using UnityEngine;
using UnityEngine.Playables;

public class Curscene : MonoBehaviour
{
    public PlayableDirector cutscene;
    public BoolValue storedValue;
    public bool active;

    void Start()
    {
        cutscene = GetComponent<PlayableDirector>();
        active = storedValue.RuntimeValue;

        if(active)
        {
            playAnimation();
        }else
        {
            stopAnimation();
        }
    }

        public void playAnimation()
    {
        cutscene.enabled = true;
        storedValue.RuntimeValue = false;
        active = false;

    }

    public void stopAnimation()
    {
        cutscene.enabled = false;
    }


}
