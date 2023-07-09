using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    Vector3 vector3;
    float speed = 0.01f;
    int lensSpeed = 4;//  0.01 4-знака для обрезки
    string strV3 = "";// string-vector3

    string button = "N";// 

    //void Start()
    //{
        
    //}
    void Update()
    {
        RunRight();
    }
    //void RunRightTest()
    //{
    //    if (button == "R")// RoomManager
    //    {
    //        button = "N";

    //        vector3 = transform.position;
    //        vector3.x += speed;
    //        transform.position = vector3;

    //        #region// ---- strV3 = s.Remove(4);// выбивает ошибки ----
    //        //Debug.Log(vector3.x);
    //        //string s = Convert.ToString(vector3.x);
    //        //try
    //        //{//strV3 = s.Remove(4);// выбивает ошибки
    //        //    Debug.Log(s);
    //        //    for (int i = 0; i < 4; i++)
    //        //    { strV3 += s[i]; }////////
    //        //}
    //        //catch (Exception ex) { Debug.Log(s + " / " + ex); }////////
    //        ////Debug.Log(strV3);
    //        //strV3 = "";
    //        #endregion

    //    }
    //}
    void RunRight()
    {//         2:2      /    id    /    кнопка    /    vector
        if (button == "R")// RoomManager
        {
            button = "N";

            vector3 = transform.position;
            vector3.x += speed;
            transform.position = vector3;
        }
    }
    string VectorRun(string _buttonVector) 
    {
        string speedStr = Convert.ToString(speed);// speed
        strV3 = "";// string-vector3
        vector3 = transform.position;

        if (_buttonVector == "R")// Button_R
        {
            string v3x = Convert.ToString(vector3.x);// string-vector3
            if (v3x[0] != '-')// не отрицательное значение
            {
                //Debug.Log("1/string v3x = " + v3x);
                if(v3x.Length == 1)// от ошибки если меньше 4-х знаков. 0f
                { v3x = v3x + ".00"; strV3 = v3x; }
                else if (v3x.Length == 3)
                { v3x = v3x + "0"; strV3 = v3x; }
                else if (v3x.Length == 4)
                { strV3 = v3x; }
                else if (v3x.Length > 4)
                {
                    for (int i = 0; i < lensSpeed; i++)
                    { strV3 += v3x[i]; }
                }
                //Debug.Log("2/string v3x = " + v3x);
            }
            else if (v3x[0] == '-')// отрицательное значение
            {
                //Debug.Log("1/string v3x = " + v3x);
                if (v3x.Length == 1 + 1)// от ошибки если меньше 4-х знаков. 0f
                { v3x = v3x + ".00"; strV3 = v3x; }
                else if (v3x.Length == 3 + 1)
                { v3x = v3x + "0"; strV3 = v3x; }
                else if (v3x.Length == 4 + 1)
                { strV3 = v3x; }
                else if (v3x.Length > 4 + 1)
                {
                    for (int i = 0; i < lensSpeed + 1; i++)
                    { strV3 += v3x[i]; }// ошибка на 0.0f !!!!!!!!!!!!!!!!!!!!!
                }
                //Debug.Log("2/string v3x = " + v3x); 
            }
            //Debug.Log("strV3 = " + strV3);// данная точка по vector3.x
        }
        //return ReplacementOnPoint(strV3 + ":" + speedStr);// замена запятой на точку
        return (strV3 + ":" + speedStr);// без замены запятой
    }
    string ReplacementOnPoint(string _vector)// замена запятой на точку
    {
        string _v = "";

        for (int i = 0; i < _vector.Length; i++)
        {
            if (_vector[i] != ',')
            {
                _v += _vector[i];
            }
            if (_vector[i] == ',') 
            {
                _v += '.';
            }
        }
        return _v;
    }
    //***********************************************************
    //public string GetVectorRun()
    //{
    //    string _s = VectorRun();
    //    return _s;
    //}
    public void SetRun(string _button)// RoomManager
    {
        button = _button;
    }
    public string GetVectorRun(string _buttonVector)// ClientSocketManager <- Button_R
    {
        string _s = VectorRun(_buttonVector);
        return _s;
    }
    public void SetTransformPoint(float _transform, float _point)// RoomManager <- vector3
    {
        //Debug.Log("SetTransformPoint = " + _transform + " & " + _point);
    }
}
