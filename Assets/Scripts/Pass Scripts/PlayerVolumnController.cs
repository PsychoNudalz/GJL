using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerVolumnController : MonoBehaviour
{
    [SerializeField] Volume volume;
    [Header("Vignette")]
    [SerializeField] Vector2 vignetteRange = new Vector2(0f, 0.55f);
    [SerializeField] AnimationCurve vignetteCurce;
    [SerializeField] float BVValue;
    Vignette v;
    [Header("ChromaticAberration")]
    [SerializeField] float decayTime_CA = 0.2f;
    [Space]
    ChromaticAberration ca;
    [Header("Lens Distortion")]
    [SerializeField] float decayTime_LD = 0.4f;
    [Range(0f, 1f)]
    [SerializeField] float maxDistortion = 0.7f;
    [SerializeField] AnimationCurve distortionCurve;
    [SerializeField] float timeNow_LD;
    [Space]
    LensDistortion ld;

    private void Start()
    {
        if (!volume)
        {
            volume = FindObjectOfType<Volume>();
        }
        //SetBloodVignette(true);
        SetBloodVignette(false);
        volume.profile.TryGet(out v);
        volume.profile.TryGet(out ca);
        volume.profile.TryGet(out ld);
    }

    private void FixedUpdate()
    {
        if (!volume)
        {
            volume = FindObjectOfType<Volume>();
        }
        else
        {
            UpdateCA();
            UpdateLD();
            UpdateBV();
        }
    }

    public void SetBloodVignette(bool b, float intensity = 0)
    {
        if (v != null)
        {
            v.active = b;
            v.intensity.overrideState = b;
            //v.intensity.value = vignetteRange.x + (vignetteRange.y - vignetteRange.x) * intensity;
            BVValue = vignetteRange.x + (vignetteRange.y - vignetteRange.x) * vignetteCurce.Evaluate(intensity);
        }
    }
    private void UpdateBV()
    {
        v.intensity.value = Mathf.Lerp(v.intensity.value, BVValue, 2 * Time.deltaTime);
    }

    public void PlayCD()
    {
        if (ca == null)
        {
            return;
        }
        ca.active = true;
        ca.intensity.overrideState = true;
        ca.intensity.value = 1;
    }
    void UpdateCA()
    {
        if (ca == null || ca.IsActive() == false)
        {
            return;
        }
        if (ca.intensity.value > 0)
        {
            ca.intensity.value -= 1 / decayTime_CA * Time.deltaTime;
            if (ca.intensity.value < 0)
            {
                ca.active = false;
            }
        }
    }

    public void PlayLD()
    {
        if (ld == null)
        {
            return;
        }
        ld.active = true;
        ld.intensity.overrideState = true;
        ld.intensity.value = maxDistortion;
        timeNow_LD = decayTime_LD;
    }
    void UpdateLD()
    {
        if (ld == null || ld.IsActive() == false)
        {
            return;
        }
        if (timeNow_LD > 0)
        {
            timeNow_LD -= Time.deltaTime;
            ld.intensity.value = distortionCurve.Evaluate(1 - (timeNow_LD / decayTime_LD)) * -maxDistortion;
            if (timeNow_LD < 0)
            {
                ld.active = false;
            }
        }
    }

}
