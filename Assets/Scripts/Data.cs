using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{   //                      d  m
    string dayFileLocal = "07.08.2023 12:06:05";// день создания файла Local 
    string dayFileUTC =   "07.08.2023 09:06:05";// день создания файла UTC 
    string mail = "qwerty@gmail";

    string local;
    string UTC;

    //void Start()
    //{
    //}
    //void Update()
    //{
    //}

    private string CleanData(string _time)// приводит дату и время в порядок
    {
        string _t = _time;
        string _s = "";

        for (int i = 0; i < _t.Length; i++)//ставим ':'
        {
            if (_t[i] == '.' || _t[i] == ' ')
            {
                _s = _s + ':';
            }
            else
            {
                _s = _s + _t[i];
            }
        }

        _t = _s;
        _s = "/";

        for (int i = 0; i < _t.Length; i++)// удаляем начальные '0'
        {
            if (i == 0 && _t[i] != '0')
            {
                _s = _s + _t[i];
            }
            else if (i > 0)
            {
                if (_t[i] == ':')
                {
                    _s = _s + _t[i];
                    i++;
                    if (_t[i] != '0')
                    {
                        _s = _s + _t[i];
                    }
                }
                else if (_t[i] != ':')
                {
                    _s = _s + _t[i];
                }
            }
        }

        return _s;
    }
    //**************************************************
    public string GetLogin()
    {
        string _dayFileLocal = CleanData(dayFileLocal);// приводит дату и время в порядок
        string _dayFileUTC = CleanData(dayFileUTC);// приводит дату и время в порядок
        string _login = "/" + mail;

        local = DateTime.Now.ToString();
        UTC = DateTime.UtcNow.ToString();

        local = CleanData(local);// приводит дату и время в порядок
        UTC = CleanData(UTC);// приводит дату и время в порядок

        return _login + _dayFileLocal + _dayFileUTC + local + UTC;
    }
}   
