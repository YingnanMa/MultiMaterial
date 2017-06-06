﻿using System.Linq;using UnityEditor;using UnityEngine;namespace UnityLabs.Cinema{    public class MultiMaterialEditorUtilities    {        public static void UpdateMaterials(MultiMaterialData multiMaterialData, MaterialEditor controlMat)        {            if (multiMaterialData.materialArray.Length < 1 && controlMat == null || controlMat.target == null)            {                return;            }            SetCheckMaterialShaders(multiMaterialData, controlMat.target as Material);            var matSerial = new SerializedObject(controlMat.target);            var controlProps = matSerial.GetIterator();            var matObjs = multiMaterialData.materialArray.Select(material => new SerializedObject(material)).ToList();            foreach (var matObj in matObjs) { matObj.Update();}            while (controlProps.NextVisible(true))            {                foreach (var matObj in matObjs)                {                    var prop = matObj.FindProperty(controlProps.propertyPath);                    if (prop != null)                    {                        switch (prop.propertyType)                        {                            case SerializedPropertyType.AnimationCurve:                                break;                            case SerializedPropertyType.ArraySize:                                break;                            case SerializedPropertyType.Boolean:                                prop.boolValue = controlProps.boolValue;                                break;                            case SerializedPropertyType.Bounds:                                break;                            case SerializedPropertyType.Character:                                break;                            case SerializedPropertyType.Color:                                prop.colorValue = controlProps.colorValue;                                break;                            case SerializedPropertyType.Enum:                                break;                            case SerializedPropertyType.ExposedReference:                                break;                            case SerializedPropertyType.FixedBufferSize:                                break;                            case SerializedPropertyType.Generic:                                break;                            case SerializedPropertyType.Gradient:                                break;                            case SerializedPropertyType.Float:                                prop.floatValue = controlProps.floatValue;                                break;                            case SerializedPropertyType.Integer:                                break;                            case SerializedPropertyType.String:                                prop.stringValue = controlProps.stringValue;                                break;                            case SerializedPropertyType.Rect:                                break;                            case SerializedPropertyType.Quaternion:                                break;                            case SerializedPropertyType.Vector2:                                break;                            case SerializedPropertyType.Vector3:                                break;                            case SerializedPropertyType.Vector4:                                break;                            case SerializedPropertyType.ObjectReference:                                if (controlProps.objectReferenceValue != null &&                                     controlProps.objectReferenceValue.GetType().IsAssignableFrom(typeof(Texture)))                                    prop.objectReferenceValue = controlProps.objectReferenceValue;                                break;                            case SerializedPropertyType.LayerMask:                                break;                        }                    }                }            }            foreach (var matObj in matObjs) { matObj.ApplyModifiedPropertiesWithoutUndo();}        }        public static void SetCheckMaterialShaders(MultiMaterialData multiMaterialData, Material mat)        {            var matSerial = new SerializedObject(mat);            var shaderSerial = matSerial.FindProperty("m_Shader");            foreach (var material in multiMaterialData.materialArray)            {                var targetMatSerial = new SerializedObject(material);                var targetShader = targetMatSerial.FindProperty("m_Shader");                if (shaderSerial != targetShader)                {                    targetMatSerial.Update();                    targetShader.objectReferenceValue = shaderSerial.objectReferenceValue;                    targetMatSerial.ApplyModifiedPropertiesWithoutUndo();                }            }        }    }}