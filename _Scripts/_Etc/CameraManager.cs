using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraManager : MonoBehaviour
{

    private bool filed = false;
    private Vector3 startPos;
    private float dfHead = 0.0f;
    private bool dfStart = false;
    private float dfcurrent = 0.0f;
    float filedValue = 60.0f;
    float filedValueHead = 60.0f;

    public bool shake = false;
    public bool aWhileShake = false;
    public float aWhileShakeCurrentTime = 0.0f;
    public float aWhileShakeTime = 0.0f;

    public float zoomSpeed;
    public float shakef;
    public Volume post;
    DepthOfField df;


    Coroutine ShakeMove;
    void Start()
    {
        startPos = this.transform.localPosition;
        post.profile.TryGet<DepthOfField>(out df);
  
    }

    // Update is called once per frame
    void Update()
    {
        if (shake)
        {
            Vector3 temp = startPos;
            temp.x += Random.Range(-shakef, shakef);
            temp.y += Random.Range(-shakef, shakef);
            temp.z += Random.Range(-shakef, shakef);
            this.transform.localPosition = temp;
        }

        if (filed)
        {
            if (filedValue >= filedValueHead)
            {
                filedValue -= zoomSpeed * Time.deltaTime;
                this.gameObject.GetComponent<Camera>().fieldOfView = filedValue;
            }
            else
                this.gameObject.GetComponent<Camera>().fieldOfView = filedValueHead;
        }
        if (dfStart)
        {
            if (dfHead < df.aperture.value)
            { 
                dfcurrent -= 15.0f * Time.deltaTime;
                df.aperture.value = dfcurrent;
            }
        }

        else if (aWhileShake)
        {
            aWhileShakeCurrentTime += 1.0f * Time.deltaTime;
            Vector3 temp = startPos;
            temp.x += Random.Range(-shakef, shakef);
            temp.y += Random.Range(-shakef, shakef);
            temp.z += Random.Range(-shakef, shakef);
            this.transform.localPosition = temp;
            if (aWhileShakeCurrentTime >= aWhileShakeTime)
            {
                this.transform.localPosition = startPos;
                aWhileShake = false;
            }
        }
        if(!shake && !filed && !dfStart && !aWhileShake)
            this.transform.localPosition = startPos;
    }

    public void StartShakeOnly(float f)
    {
        shake = true;
        shakef = f;
    }
    public void EndShakeOnly()
    {
        shake = false;
        this.transform.localPosition = startPos;
    }

    public void StartDf(float f)
    {
        dfHead = f;
        dfStart = true;
        dfcurrent = df.aperture.value;
    }

    public void EndDf()
    {
        dfStart = false;
        StartCoroutine(DfSet());
    }

    public void ZoomIn(float f)
    {
        filed = true;
        filedValueHead = f;
    }
    public void ZoomOut()
    {
        filed = false;
        StartCoroutine(FiledSet());
        filedValue = 60;
    }

    public void AwhileShake(float f)
    {
        if (!shake)
        {
            aWhileShake = true;
            aWhileShakeCurrentTime = 0.0f;
            shakef = 0.1f;
            aWhileShakeTime = f;
        }
    }

    IEnumerator DfSet()
    {
        while (dfcurrent < 32)
        {
            dfcurrent += 100 * Time.deltaTime;
            df.aperture.value = dfcurrent;
            yield return null;
        }
        df.aperture.value = 32;
    }

    IEnumerator FiledSet()
    {
        while (this.gameObject.GetComponent<Camera>().fieldOfView < 60)
        {
            this.gameObject.GetComponent<Camera>().fieldOfView += 20.0f * Time.deltaTime;

            yield return null;
        }
        this.gameObject.GetComponent<Camera>().fieldOfView = 60;
    }
}
