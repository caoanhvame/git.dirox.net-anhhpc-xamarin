
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
using Android.Text;// for Spannable
using Android.Graphics;
using Android.Text.Style;
using Android.Util;
using Android.Animation;


namespace Tab
{
	[Activity (Label = "ListAdapter")]			
	public class CustomList: BaseAdapter{
		private Activity context;
		private List<string> web;
		private List<int> imageId;
		private String search_str="";
		private Contact contact;
		//indicate what tab are using this adater, 0=AllPeopleFragement, 1=FavoriteFragement
		private int status;
		public CustomList(Activity context,List<string>  web, List<int> imageId,Contact contact,int status) {
			this.context = context;
			this.web = web;
			this.imageId = imageId;
			this.contact = contact;
			this.status = status;
		}
		public CustomList(Activity context,List<string>  web, List<int> imageId,String str,Contact contact,int status) {
			this.context = context;
			this.web = web;
			this.imageId = imageId;
			this.search_str = str;
			this.contact = contact;
			this.status = status;
		}

		public override int Count {
			get { return web.Count; }
		}

		public override Java.Lang.Object GetItem (int position)
		{
			return null;
		}

		public override long GetItemId (int position)
		{
			return 0;
		}


		public override View GetView(int position, View view, ViewGroup parent) {
			LayoutInflater inflater = context.LayoutInflater;
			View rowView=inflater.Inflate(Resource.Layout.list_row, null, true);
			TextView txtTitle = (TextView) rowView.FindViewById(Resource.Id.txt);
			ImageView imageView = (ImageView) rowView.FindViewById(Resource.Id.img);
			int found_pos = web [position].IndexOfAny (search_str.ToCharArray ());
			if (found_pos!= -1) {
				SpannableString spannable = new SpannableString (web [position]);
				spannable.SetSpan (new ForegroundColorSpan (Color.Red), found_pos, 
					found_pos+search_str.Length, Android.Text.SpanTypes.ExclusiveExclusive);

				txtTitle.SetText (spannable, TextView.BufferType.Spannable);
			} else {
				txtTitle.Text = web [position];
			}
			imageView.SetImageResource(imageId[position]);
			imageView.Tag =imageId [position];
			float _startX=0;
			rowView.Touch += delegate(object sender, View.TouchEventArgs touchEventArgs) {
				string message="";
				switch (touchEventArgs.Event.Action )
				{
				case MotionEventActions.Down:
					message = "Touch Begins";
					_startX=touchEventArgs.Event.GetX();
					break;
				case MotionEventActions.Move:
					message = "Touch Ends";
					break;
				case MotionEventActions.Up:

					if(_startX==touchEventArgs.Event.GetX()){
						Intent person_profile=new Intent(context,typeof(PersonProfileActivity));
						person_profile.PutExtra("web",txtTitle.Text );
						person_profile.PutExtra("imageId",imageId[position]);  
						if(status==0){
							person_profile.PutExtra("status",0);
						}else{
							person_profile.PutExtra("status",1);
						}

						context.StartActivityForResult(person_profile,0);
//						rowView.SetBackgroundResource (Android.Resource.Drawable.MenuitemBackground);
					}else if(_startX> touchEventArgs.Event.GetX()){
						Button btn=rowView.FindViewById<Button>(Resource.Id.btn);
						btn.HasTransientState=true;
						if(status==1){
							btn.Text="Delete";
							Android.Widget.TableRow.LayoutParams lpView = new Android.Widget.TableRow.LayoutParams(Android.Widget.TableRow.LayoutParams.WrapContent, Android.Widget.TableRow.LayoutParams.WrapContent);
							btn.LayoutParameters=lpView;
						}
						rowView.HasTransientState = true;
						Display d =context.WindowManager.DefaultDisplay;
						DisplayMetrics metrics = new DisplayMetrics();
						d.GetMetrics(metrics);
						int widthPixels = metrics.WidthPixels;
						btn.Visibility= 0;
						System.Console.WriteLine(btn.Width+"  wide");
						ValueAnimator animator = ValueAnimator.OfFloat(new[] { 1f, 0f });
						animator=ValueAnimator.OfFloat(new []{ widthPixels,widthPixels-(float)btn.Width});
						animator.SetDuration(1000);
						animator.Update += (o, animatorUpdateEventArgs) =>{
							btn.Visibility= 0;
							btn.SetX((float)animatorUpdateEventArgs.Animation.AnimatedValue);
						};
						animator.AnimationEnd += delegate{
							rowView.SetX(0);
						};
						animator.Start();
						if(status==0){
						btn.Click+= delegate(object ob, EventArgs e) {
								contact.FavoriteWeb.Add(txtTitle.Text  );
								contact.FavoriteImageId.Add( imageId[position]);
								btn.Visibility= Android.Views.ViewStates.Gone;
								Toast.MakeText (context.ApplicationContext, "ADDED", ToastLength.Short).Show ();
						};
					
						}else{

							btn.Click+= delegate(object obj, EventArgs e) {
								rowView.HasTransientState = true;

								ValueAnimator row_animator = ValueAnimator.OfFloat(new[] { 1f, 0f });
								row_animator.SetDuration(1000);
								row_animator.Update += (o, animatorUpdateEventArgs) =>{
									rowView.Alpha = (float)animatorUpdateEventArgs.Animation.AnimatedValue;
								};
								row_animator.AnimationEnd += delegate{
									rowView.Alpha = 1f;
									contact.FavoriteWeb.RemoveAt(position);
									contact.FavoriteImageId.RemoveAt(position);
									NotifyDataSetChanged();
									btn.Visibility= Android.Views.ViewStates.Gone;
									Toast.MakeText (context.ApplicationContext, "DELETED", ToastLength.Short).Show ();
								};
								row_animator.Start();
							};
						}
					}
					break;
				default:
					message = string.Empty;
					break;
				}

				System.Console.WriteLine(message);

			};
			return rowView;
		}
	}
}



