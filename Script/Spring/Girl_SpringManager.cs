﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girl_SpringManager : MonoBehaviour
{

    //Kobayashi
    // DynamicRatio is paramater for activated level of dynamic animation 
    public float dynamicRatio = 1.0f;

    //Ebata
    public float stiffnessForce;
    public AnimationCurve stiffnessCurve;
    public float dragForce;
    public AnimationCurve dragCurve;
    public Girl_SpringBone[] springBones;

    void Start()
    {
        UpdateParameters();
    }

    void Update()
    {
#if UNITY_EDITOR
        //Kobayashi
        if (dynamicRatio >= 1.0f)
            dynamicRatio = 1.0f;
        else if (dynamicRatio <= 0.0f)
            dynamicRatio = 0.0f;
        //Ebata
        UpdateParameters();
#endif
    }

    private void LateUpdate()
    {
        //Kobayashi
        if (dynamicRatio != 0.0f)
        {
            for (int i = 0; i < springBones.Length; i++)
            {
                if (dynamicRatio > springBones[i].threshold)
                {
                    springBones[i].UpdateSpring();
                }
            }
        }
    }

    private void UpdateParameters()
    {
        UpdateParameter("stiffnessForce", stiffnessForce, stiffnessCurve);
        UpdateParameter("dragForce", dragForce, dragCurve);
    }

    private void UpdateParameter(string fieldName, float baseValue, AnimationCurve curve)
    {
        Debug.Log("CURVE：" + curve.keys.Length);
        var start = curve.keys[0].time;
        var end = curve.keys[curve.length - 1].time;
        //var step	= (end - start) / (springBones.Length - 1);

        var prop = springBones[0].GetType().GetField(fieldName, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

        for (int i = 0; i < springBones.Length; i++)
        {
            //Kobayashi
            if (!springBones[i].isUseEachBoneForceSettings)
            {
                var scale = curve.Evaluate(start + (end - start) * i / (springBones.Length - 1));
                prop.SetValue(springBones[i], baseValue * scale);
            }
        }
    }
}
