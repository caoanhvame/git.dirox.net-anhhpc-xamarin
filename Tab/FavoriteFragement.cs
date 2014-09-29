
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS; 
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Android.Animation;

namespace Tab
{
	public class FavoriteFragement : Fragment
	{
		Contact contact;
		public FavoriteFragement(Contact contact){
			this.contact = contact;
		}
		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			base.OnCreateView (inflater, container, savedInstanceState);
			var view = inflater.Inflate (Resource.Layout.Tab, container, false);
			ISharedPreferences prefs = Activity.ApplicationContext.GetSharedPreferences ("shared", FileCreationMode.WorldReadable);
			Console.WriteLine(prefs.GetString ("number_of_times_accessed", "aa"));

			CustomList adapter = new CustomList( Activity, contact.FavoriteWeb, contact.FavoriteImageId,contact,1);
			ListView list=(ListView)view.FindViewById(Resource.Id.list);

			list.Adapter = adapter;
			//long click to delete, NO LONGER NEED DUE TO TOUCHLISTENER
//			list.ItemLongClick+= delegate(object sender, AdapterView.ItemLongClickEventArgs e) {
//
//				View view2 = e.View;
//				view2.HasTransientState = true;
//
//				ValueAnimator animator = ValueAnimator.OfFloat(new[] { 1f, 0f });
//				animator.SetDuration(1000);
//				animator.Update += (o, animatorUpdateEventArgs) =>{
//					view2.Alpha = (float)animatorUpdateEventArgs.Animation.AnimatedValue;
//				};
//
//				animator.AnimationEnd += delegate{
//					view2.Alpha = 1f;
//					contact.FavoriteWeb.RemoveAt(e.Position);
//					contact.FavoriteImageId.RemoveAt(e.Position);
//					adapter=new CustomList( Activity, contact.FavoriteWeb, contact.FavoriteImageId,contact,1);
//					list.Adapter=adapter
//				;
//				};
//				animator.Start();
//
//			};


			return view;
		}
	}
}

