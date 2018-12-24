using System;
using Foundation;
using UIKit;

namespace AstronomyPictureOfTheDay.Sample.IOS
{
    public partial class ViewController : UIViewController
    {
        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();
            var nasa = new AstronomyPictureOfTheDay.NasaPictureOfTheDay();

            var pic = await nasa.GetTodaysPictureAsync("RRFPuCBEyEvZg6DeIiWnJDjGP3t15MKgX3nGVHfV");

            if (pic.Success)
            {
                lblTitle.Text = pic.pictureOfTheDay.title;
                lblDate.Text = $"Image for {pic.pictureOfTheDay.date}";
                UIImage img = null;
                string uri = pic.pictureOfTheDay.url;
                using (var url = new NSUrl(uri))
                using (var data = NSData.FromUrl(url))
                img = UIImage.LoadFromData(data);
                lblDescription.Text = pic.pictureOfTheDay.explanation;
                imgPicOfDay.Image = img;
            }
            else
            {
                lblTitle.Text = "An error has occurred";
            }
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}
