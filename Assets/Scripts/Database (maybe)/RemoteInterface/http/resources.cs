/*

    Daniel Vaughn
    UNCW VR_Research_Team

    resources.cs

*/

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using UnityEngine;

public class contentType
{

    private static Dictionary<string, string> type_lookup = new Dictionary<string, string>
    {
        { ".png", "image/png" },
        { ".jpg", "image/jpeg" },
        { ".jpeg", "image/jpeg" },
        { ".gif", "image/gif" },
        { ".js", "text/javascript" },
        { ".htm", "text/html" },
        { ".html", "text/html" },
        { ".css", "text/css" },
        { ".svg", "image/svg+xml" },
        { ".ico", "image/x-icon" }  
    };

    public static string getType(string req)
    {
        return type_lookup[req];
    }

}

public class resources
{
    public static Dictionary<string, string[]> resource_dict = new Dictionary<string, string[]>();
    private static string bottom_level_path = @"\Assets\Scripts\RemoteInterface\http\resources";
    private static string top_level_path, full_path;
    private static List<string> files = new List<string>();
    public static Dictionary<string, int> restricted_resources = new Dictionary<string, int>();

    public static void init()
    {
        top_level_path = Directory.GetCurrentDirectory();
        full_path = top_level_path + bottom_level_path;
        getFilesInDirectory();
        buildRestrictedList();
    }

    private static void buildRestrictedList()
    {

        restricted_resources.Add("/vasc", 2);

    }

    private static void fileFinder(string[] sub_dirs)
    {
        foreach (string sub in sub_dirs)
        {
            files.AddRange(Directory.GetFiles(sub));
            if (Directory.GetDirectories(sub).Length > 0)
            {
                fileFinder(Directory.GetDirectories(sub));
            }
        }
    }

    private static void getFilesInDirectory()
    {
        string[] sub_dirs = Directory.GetDirectories(full_path);
        files.AddRange(Directory.GetFiles(full_path));

        fileFinder(sub_dirs);

        foreach (string f in files)
        {
            string name = f.Substring(full_path.Length);
            resource_dict.Add(
                name.Replace(@"\", "/"),
                new string[] {
                    (full_path + name),
                    (new FileInfo(full_path + name).Extension)
                }
            );
        }

        //  root file mapping
        resource_dict.Add("/", new string[] { full_path + "/home/index.html", ".html" });
        resource_dict.Add("/login", new string[] { full_path + "/home/auth/authenticate.html", ".html" });
        resource_dict.Add("/vasc", new string[] { full_path + "/home/control/control.html", ".html" });

    }

}
