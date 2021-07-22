using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;



public class PostProcesingScript : MonoBehaviour
{

    public  Volume volume;
    //  public Vignette vignette;
    //    public Bloom bloom;
    Bloom bloom;

    void Start()
    {
       
        if (volume.profile.TryGet(out bloom))
        {
            bloom.intensity.value = 10;
        }
     

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            bloom.intensity.value = 100;
        }
        if (Input.GetMouseButtonUp(0))
        {
            bloom.intensity.value = 1;
            // vignette.smoothness.value = 0f;
        }
    }
}
