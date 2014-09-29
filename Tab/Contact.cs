using System;
using System.Collections.Generic;
namespace Tab
{
	public class Contact
	{
		List<string> all_web=new List<string>(){"Google ",
			"Twitter",
			"Windows",
			"Bing",
			"Itunes",
			"Wordpress",
			"Drupal"};
		private List<int> all_imageId =new List<int>(){Resource.Drawable.sample_0,
			Resource.Drawable.sample_1,
			Resource.Drawable.sample_2,
			Resource.Drawable.sample_3,
			Resource.Drawable.sample_4,
			Resource.Drawable.sample_5,
			Resource.Drawable.sample_6,};
		private List<string> favoriteWeb = new List<string> (){
			"Google ",
			"Twitter",
			"Windows",
			"Bing",
			"Itunes"
		};
		private List<int> favoriteImageId =new List<int>(){Resource.Drawable.sample_0,
			Resource.Drawable.sample_1,
			Resource.Drawable.sample_2,
			Resource.Drawable.sample_3,
			Resource.Drawable.sample_4
		};
		public Contact ()
		{
		}
		public List<string> All_Web{
			get { return all_web; }  // Getter
			set { all_web = value; } // Setter
		}

		public List<int> All_ImageId{
			get { return all_imageId; }  // Getter
			set { all_imageId = value; } // Setter
		}
		public List<string> FavoriteWeb {
			get {
				return this.favoriteWeb;
			}
			set {
				favoriteWeb = value;
			}
		}

		public List<int> FavoriteImageId {
			get {
				return this.favoriteImageId;
			}
			set {
				favoriteImageId = value;
			}
		}
	}
}

