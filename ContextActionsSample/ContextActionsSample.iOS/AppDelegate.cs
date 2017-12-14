using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace ContextActionsSample.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            //指定したファイルのパスを取得する。
            var dbPath = GetLocalFilePath("culculate.db3");

            //この段階ではまだエラーになる。
            LoadApplication(new App(dbPath));

            return base.FinishedLaunching(app, options);
        }

        public static string GetLocalFilePath(string filename)
        {
            //指定されたファイルのパスを取得する。なければ作成してそのパスを返却する。
            var docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var libFolder = System.IO.Path.Combine(docFolder, "..", "Library", "Databases");

            if (!System.IO.Directory.Exists(libFolder))
            {
                System.IO.Directory.CreateDirectory(libFolder);
            }

            return System.IO.Path.Combine(libFolder, filename);
        }
    }
}