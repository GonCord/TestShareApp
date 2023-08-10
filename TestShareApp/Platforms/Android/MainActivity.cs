using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;

namespace TestShareApp;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
[IntentFilter(new[] { Intent.ActionSend }, Categories = new[] { Intent.CategoryDefault }, DataMimeType = "image/*")]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {


        try
        {
            base.OnCreate(savedInstanceState);
            Stream? inputStream = null;
            Intent intent = Intent;

            var action = intent.Action;
            var type = intent.Type;
            var categories = intent.Categories.ToList();


            if (Intent.ActionSend.Equals(action) && type != null)
            {
                if (type.StartsWith("image/"))
                {
                    var filePath = intent?.ClipData?.GetItemAt(0);
                    if (filePath != null)
                    {
                        inputStream = ContentResolver!.OpenInputStream(filePath.Uri);
                    }

                    if (inputStream != null)
                    {
                        using (var reader = new StreamReader(inputStream))
                        {
                            var content = reader.ReadToEnd();
                        }
                    }
                    inputStream.Close();
                    inputStream.Dispose();
                }
            }

            //MoveTaskToBack(false);

        }
        catch (Exception e)
        {

            Console.WriteLine($"***********  ===> Exception catched! {e.Message} - {e.InnerException.Message}  - {e.Source}");
        }

    }
}
