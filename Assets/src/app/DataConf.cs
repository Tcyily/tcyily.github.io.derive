using System.Collections;
using System.Collections.Generic;
public class DataConf //: Dictionary<string, int>
{
    public static DataConf instance_;
    public DataConf() { }
    public static DataConf GetInstance()
    {
        if (instance_ == null)
            instance_ = new DataConf();
        return instance_;
    }

    public static Dictionary<string, Dictionary<int, dynamic>> data_ = new Dictionary<string, Dictionary<int, dynamic>>();
    public Dictionary<int, dynamic> this[string key_name]
    {
        get { return data_[key_name]; }
        set { data_[key_name] = value; }
    }

    /// <summary>
    /// Key充当path_ 与 data_ 之间的映射
    /// the Key Mapping between path_ and data_
    /// </summary>
    public static readonly Dictionary<string, string> path_ = new Dictionary<string, string>
    {
        { "NavicatResource", @"Assets\res\data\conf\NavicatResource.json"}
    };
}