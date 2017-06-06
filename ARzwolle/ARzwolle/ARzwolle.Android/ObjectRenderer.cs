using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Wikitude.Architect;
using Wikitude.Common.Permission;


namespace ARzwolle.Droid
{
    //public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    [Activity(Label = "ObjectRenderer", Icon = "@drawable/logoZwolle", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class ObjectRenderer : Activity, IPermissionManagerPermissionManagerCallback
    {
        

        protected override void OnCreate(Bundle bundle)
        {
            //TabLayoutResource = Resource.Layout.Tabbar;
            //ToolbarResource = Resource.Layout.Toolbar; 

            base.OnCreate(bundle);

            //global::Xamarin.Forms.Forms.Init (this, bundle);
            //LoadApplication (new ARzwolle.App ());


            //rename main activity.axmls
            SetContentView(Resource.Layout.Main_activity);

            // check if the permissions are accepted
            ArchitectView.PermissionManager.CheckPermissions(this, new string[] { Android.Manifest.Permission.Camera }, PermissionManager.WikitudePermissionRequest, this);
            
        }

        public void PermissionsDenied(string[] p0)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetMessage("Something went wrong");
        }

        public void PermissionsGranted(int p0)
        {
            string TargetName = Intent.GetStringExtra("Target_Name");
            Intent Wactivity = new Android.Content.Intent(this, typeof(WikitudeActivity));
            Wactivity.PutExtra("targetname", TargetName);
            StartActivity(Wactivity);
        }

        public void ShowPermissionRationale(int requestCode, string[] permissions)
        {
            ArchitectView.PermissionManager.PositiveRationaleResult(requestCode, permissions);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            int[] result = new int[grantResults.Length];
            for (int i = 0; i < grantResults.Length; i++)
            {
                result[i] = grantResults[i] == Permission.Granted ? 0 : -1;
            }
            ArchitectView.PermissionManager.OnRequestPermissionsResult(requestCode, permissions, result);
        }


    }
}

