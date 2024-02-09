using System.Diagnostics;
using System.Reflection;
using Microsoft.Maui.Controls.Handlers.Items;
using ObjCRuntime;
using UIKit;

namespace MountFuji;

public class Program
{
    // This is the main entry point of the application.
    static void Main(string[] args)
    {
        // TODO: REMOVE ME BEFORE RELEASE!!!!
        // there is a bug in JetBrains Rider, the debugger will not
        // attach unless we sleep here!
        //if (Debugger.IsAttached) Thread.Sleep(4000);
        
    
        //
        // if you want to use a different Application Delegate class from "AppDelegate"
        // you can specify it here.
        UIApplication.Main(args, null, typeof(AppDelegate));
    }
}