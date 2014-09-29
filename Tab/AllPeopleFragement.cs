
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;//for thread
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Net;
using Android.Graphics;
using Java.IO;
using Java.Util;
using Android.Animation;//for animation

namespace Tab
{
	class AllPeopleFragement: Fragment
	{  string data="All people";
		TextView sampleTextView;
		EditText personNameEditText;
		List<string> all_web=new List<string>(){"Google ",
			"Twitter",
			"Windows",
			"Bing",
			"Itunes",
			"Wordpress",
			"Drupal"};
		List<int> all_imageId =new List<int>(){Resource.Drawable.sample_0,
			Resource.Drawable.sample_1,
			Resource.Drawable.sample_2,
			Resource.Drawable.sample_3,
			Resource.Drawable.sample_4,
			Resource.Drawable.sample_5,
			Resource.Drawable.sample_6,};
		private Contact contact;
		public AllPeopleFragement(Contact contact){
			this.contact = contact;
		}
		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{ 	
			base.OnCreateView (inflater, container, savedInstanceState);
			var view = inflater.Inflate (Resource.Layout.Tab, container, false);
			sampleTextView = view.FindViewById<TextView> (Resource.Id.sampleTextView); 
			personNameEditText = view.FindViewById<EditText> (Resource.Id.peopleName);
			var imageView1 = view.FindViewById<ImageView> (Resource.Id.imageView1);
			String status = Android.OS.Environment.ExternalStorageState;
			if (Android.OS.Environment.MediaMounted.Equals (status)) {
				Toast.MakeText (Activity.ApplicationContext, "External", ToastLength.Short).Show ();
			}
			sampleTextView.Text=data;
//			var connectivityManager = (ConnectivityManager)Activity.GetSystemService (Activity.ConnectivityService);
//			var activeConnection = connectivityManager.ActiveNetworkInfo;
//			if ((activeConnection != null) && activeConnection.IsConnected) {
//				string urlAddress = "http://images.nationalgeographic.com/wpf/media-live/photos/000/138/overrides/save-the-ocean-tips_13821_600x450.jpg";
//				//http://api.openweathermap.org/data/2.5/weather?q=london
//				//http://images.nationalgeographic.com/wpf/media-live/photos/000/138/overrides/save-the-ocean-tips_13821_600x450.jpg
//				HttpWebRequest request = (HttpWebRequest)WebRequest.Create (urlAddress);
//				HttpWebResponse response = (HttpWebResponse)request.GetResponse ();
//				if (response.StatusCode == HttpStatusCode.OK) {
//					Stream receiveStream = response.GetResponseStream ();
////					StreamReader readStream = null;
////					if (response.CharacterSet == null)
////						readStream = new StreamReader (receiveStream);
////					else
////						readStream = new StreamReader (receiveStream, Encoding.GetEncoding (response.CharacterSet));
////					data = readStream.ReadToEnd ();
////										byte[] b;
////										using (BinaryReader br = new BinaryReader(receiveStream))
////										{
////											b = br.ReadBytes((int)receiveStream.Length);
////										}
////										Bitmap decodedByte = BitmapFactory.DecodeByteArray(b, 0, b.Length); 
////										imageView1.SetImageBitmap(decodedByte);
////					sampleTextView.Text = data;
////					response.Close ();
////					readStream.Close ();
//				}
//			}
//			HandleXML a = new HandleXML (data);
//			a.parseXMLAndStoreIt ();
			
				

			CustomList adapter = new CustomList( Activity, all_web, contact.All_ImageId,contact,0);
			ListView list=(ListView)view.FindViewById(Resource.Id.list);
			list.Adapter = adapter;
			//row click to change intend to personal info intent, NO LONGER NEED BECAUSE OF TOUCHLISTENER
//			list.ItemClick+= delegate(object sender, AdapterView.ItemClickEventArgs args) {
//				Toast.MakeText(Activity.ApplicationContext, 
//					list.GetChildAt(args.Position).FindViewById<TextView>(Resource.Id.txt).Text  ,
//					ToastLength.Short).Show();
//				Intent person_profile=new Intent(Activity,typeof(PersonProfileActivity));
//				person_profile.PutExtra("web",list.GetChildAt(args.Position).FindViewById<TextView>(Resource.Id.txt).Text  );
//				person_profile.PutExtra("imageId",(int)list.GetChildAt(args.Position).FindViewById<ImageView>(Resource.Id.img).Tag);  
//				StartActivityForResult(person_profile,0);
//			};
			//row long click show add to favorite button, NO LONGER NEED BECAUSE OF TOUCHLISTENER
//			list.ItemLongClick+= delegate(object sender, AdapterView.ItemLongClickEventArgs args) {
//				View view2 = args.View;
//				Button btn=view2.FindViewById<Button>(Resource.Id.btn);
//				btn.HasTransientState=true;
//				view2.HasTransientState = true;
//				Display d =Activity.WindowManager.DefaultDisplay;
//				DisplayMetrics metrics = new DisplayMetrics();
//				d.GetMetrics(metrics);
//				int widthPixels = metrics.WidthPixels;
//				btn.Visibility= 0;
//				System.Console.WriteLine(btn.Width+"  wide");
//				ValueAnimator animator = ValueAnimator.OfFloat(new[] { 1f, 0f });
//				animator=ValueAnimator.OfFloat(new []{ widthPixels,btn.Width*(float)1.5 });
//				animator.SetDuration(1000);
//				animator.Update += (o, animatorUpdateEventArgs) =>{
//					btn.Visibility= 0;
//					btn.SetX((float)animatorUpdateEventArgs.Animation.AnimatedValue);
//				};
//				btn.Click+= delegate(object ob, EventArgs e) {
//					contact.FavoriteWeb.Add(list.GetChildAt(args.Position).FindViewById<TextView>(Resource.Id.txt).Text  );
//					contact.FavoriteImageId.Add( (int)list.GetChildAt(args.Position).FindViewById<ImageView>(Resource.Id.img).Tag);
//				};
//				animator.AnimationEnd += delegate{
//					view2.SetX(0);
//				};
//				animator.Start();
//			};

			// find people name edit text
			personNameEditText.TextChanged += delegate(object sender, Android.Text.TextChangedEventArgs e) {
				sampleTextView.Text= e.Text+"";
				adapter = new CustomList( Activity, search_function(all_web,e.Text+""), contact.All_ImageId,e.Text+"",contact,0);
				list.Adapter=adapter;

			};
			return view;
		}
		public override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);
			if (resultCode == Result.Ok) {
				contact.FavoriteWeb.Add( data.GetStringExtra("web") );
				contact.FavoriteImageId.Add( data.GetIntExtra("imageId",0) );
			}
		}
		private List<String> search_function(List<String> people,String search_str){
			List<String> temp = new List<string>();
			for (int i = 0; i<people.Count; i++) {
				if (people[i].Contains (search_str)) {
					temp.Add (people [i]);
				}
			}
			return temp;
		}

	}
}


