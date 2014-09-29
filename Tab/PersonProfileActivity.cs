
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Tab
{
	[Activity (Label = "PersonProfileActivity")]			
	public class PersonProfileActivity : Activity
	{
		private ImageView ava;
		private TextView extra;
		private Button favorite;

		private List<string> web = new List<string> ();
		List<int> imageId = new List<int> ();
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			Title = "Anything";
			SetContentView (Resource.Layout.PersonProfile);
			ava = FindViewById<ImageView> (Resource.Id.userProfileAva);
			extra = FindViewById<TextView> (Resource.Id.userProfileExtra);
			favorite = FindViewById<Button> (Resource.Id.userProfileFavorite);
			extra.Text = "Like ...";
			Intent i = Intent;
			ava.SetImageResource(i.GetIntExtra ("imageId",0));
			if (i.GetIntExtra ("status", 0)==1) {
				favorite.Text = "Delete";
			}
			extra.Text=i.GetStringExtra ("web");
			favorite.Click += delegate(object sender, EventArgs e) {
				Intent intent=new Intent();  
				intent.PutExtra("web",extra.Text);  
				intent.PutExtra("imageId", i.GetIntExtra("imageId",0));
				intent.PutExtra("status",i.GetIntExtra("status",0));
				SetResult(Result.Ok,intent);  

				Finish();//finishing activity  
			};
		}
	}
}

