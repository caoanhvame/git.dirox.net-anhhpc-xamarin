using System;
using System.Xml;
using System.IO;
using System.Text;// StringBuilder
using Android.Content.Res;
namespace Tab
{
	public class HandleXML 
	{
		private String country = "county";
		private String temperature = "temperature";
		private String humidity = "humidity";
		private String pressure = "pressure";
		private String xmlString = null;
		public HandleXML(String xmlString){
			this.xmlString = xmlString;
		}
		public String getCountry(){
			return country;
		}
		public String getTemperature(){
			return temperature;
		}
		public String getHumidity(){
			return humidity;
		}
		public String getPressure(){
			return pressure;
		}

		public void parseXMLAndStoreIt() {

			XmlReaderSettings setting = new XmlReaderSettings();
			setting.DtdProcessing = DtdProcessing.Ignore;
			StringBuilder output = new StringBuilder();
			using (XmlReader reader = XmlReader.Create(new StringReader(xmlString),setting))
			{ 	
					// Parse the file and display each of the nodes.
					while (reader.Read())
					{
						switch (reader.NodeType)
						{
						case XmlNodeType.Element:
							break;
						case XmlNodeType.Text:
							Console.WriteLine (reader.Value);
							break;
						case XmlNodeType.XmlDeclaration:
						case XmlNodeType.ProcessingInstruction:
							break;
						case XmlNodeType.Comment:
							break;
						case XmlNodeType.EndElement:
							//Console.WriteLine (reader.Name);
							break;
						}
					}
				
			}
		}
}
}

