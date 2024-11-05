using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RoomMovement : MonoBehaviour
{

    public Vector2 cameraChangeMinimum;
    public Vector2 cameraChangeMaximum;
    public Vector3 playerChange;
    private CameraMovement cam;
    public bool needText;
    public string placeName;
    public GameObject text;
    public Text placeText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = GameObject.Find("Camera").GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")){
            cam.minPotition = cameraChangeMinimum;
            cam.maxPotition = cameraChangeMaximum;
            other.transform.position += playerChange;
            if(needText){
                StartCoroutine(placeNameCount());
            }
        }
    }

    private IEnumerator placeNameCount(){
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(4f);
        text.SetActive(false);
    }
}
