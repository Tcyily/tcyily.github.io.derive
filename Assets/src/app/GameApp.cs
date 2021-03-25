using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SimpleJSON;
using System;
using System.IO;
using System.Reflection;

public class GameApp
{
    public static Dictionary<string, GameObject> name_2_object_ = new Dictionary<string, GameObject>();
    public static Dictionary<string, Dictionary<int, dynamic>> DataConf_ = DataConf.data_;
    public static EventSystem eventSystem_ = EventSystem.instance_; 
    public static Dictionary<string, ControllerBase> ctrl_ = new Dictionary<string, ControllerBase>();
    public static Dictionary<string, ModelBase> md_ = new Dictionary<string, ModelBase>();
    /// <summary>
    /// 初始化所有md/ctl/data
    /// </summary>
    [RuntimeInitializeOnLoadMethod]
    private static void __init()
    {
        SceneManager.sceneLoaded += __SceneLoadedCallback;

        //全局管理器

        //表格
        __initdata();
        __InitFinnished();
    }

    /// <summary>
    /// 初始化表格数据
    /// </summary>
    private static void __initdata()
    {
        Dictionary<string, string> path = DataConf.path_;
        foreach(KeyValuePair<string, string> pair in path)
        {
            string res_path = pair.Value;
            string file_data = File.ReadAllText(res_path);
            dynamic json_data = JSON.Parse(file_data);
            if (json_data.IsArray)//1 sheet
            {
                Dictionary<int, dynamic> table = new Dictionary<int, dynamic>();
                string table_name = pair.Key;
                __SetAttribute(ref table, table_name, json_data);
                DataConf_[table_name] = table;
                return;
            }
            foreach(string table_name in json_data.Keys)//more sheets
            {
                Dictionary<int, dynamic> table = new Dictionary<int, dynamic>();
                __SetAttribute(ref table, table_name, json_data[table_name]);
                DataConf_[table_name] = table;
            }
        }
    }

    /// <summary>
    /// note: get instance in current assembly by type-name
    /// <para>当前程序集下通过反射实例化对象</para>
    /// </summary>
    /// <param name="type_name"></param>
    /// <returns></returns>
    private static dynamic __GetInstanceByName(string type_name)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        dynamic obj = assembly.CreateInstance(type_name);
        return obj;
    }

    /// <summary>
    /// note: line data into table struct(line_id map line_class)
    /// <para>行数据注入表格，表格数据结构为line_id - line_class</para>
    /// </summary>
    /// <param name="table"></param>
    /// <param name="table_type_name"></param>
    /// <param name="table_data"></param>
    private static void __SetAttribute(ref Dictionary<int, dynamic> table, string table_type_name, JSONNode table_data)
    {
        for(int line_idx = 0; line_idx < table_data.Count; line_idx++)
        {
            JSONNode line_data = table_data[line_idx];
            dynamic line = __GetInstanceByName(table_type_name);
            FieldInfo[] fieldInfos = line.GetType().GetFields();
            foreach (dynamic field_info in fieldInfos)
            {
                string attr_name = field_info.Name;
                dynamic attr_value = line_data[attr_name];
                if (attr_value)
                {
                    //TODO:装箱 拆箱
                    field_info.SetValue(line, Convert.ChangeType(attr_value.ToString(), field_info.FieldType));
                }
            }
            table[table.Count + 1] = line; //TODO:以行号为key？修改为以行中第一列为key？
        }
    }
    private static void __InitFinnished()
    {
        AsyncOperation aop = SceneManager.LoadSceneAsync("GameScene");
        aop.allowSceneActivation = true;
    }

    private static void __SceneLoadedCallback(Scene scene, LoadSceneMode sceneType)
    {
        GameObject camera = GameObject.Find("Main Camera");
        camera.GetComponent<Camera>().cullingMask &= ~(1 << (int)DefineConst.Layer_Name.UI);
    }
}
