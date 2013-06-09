/*
 * Created by SharpDevelop.
 * User: s4
 * Date: 2013/06/08
 * Time: 11:03
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;

namespace ss
{
	/// <summary>
	/// Description 
	/// of Util.
	/// </summary>
	public class Util
	{
		private Util()
		{
		}
		
		public static void SaveSettings(Server[] _settings, string path){
			//
			//XMLファイルに保存する
System.Xml.Serialization.XmlSerializer serializer1 =
    new System.Xml.Serialization.XmlSerializer(typeof(Server[]));
System.IO.FileStream fs1 =
    new System.IO.FileStream(path, System.IO.FileMode.Create);
serializer1.Serialize(fs1, _settings);
fs1.Close();
		}
		
		public static void ResotreSettings(ref Server[] _settings, string path){
			//
			
//保存した内容を復元する
System.Xml.Serialization.XmlSerializer serializer2 =
    new System.Xml.Serialization.XmlSerializer(typeof(Server[]));
System.IO.FileStream fs2 =
    new System.IO.FileStream(path, System.IO.FileMode.Open);

_settings = (Server[]) serializer2.Deserialize(fs2);
fs2.Close();
		}
		
				public static void ResotreSettings(ref SearchWord _settings){
			//
			
//保存した内容を復元する
System.Xml.Serialization.XmlSerializer serializer2 =
    new System.Xml.Serialization.XmlSerializer(typeof(SearchWord));
System.IO.FileStream fs2 =
    new System.IO.FileStream(SearchWord.PATH, System.IO.FileMode.Open);

_settings = (SearchWord) serializer2.Deserialize(fs2);
fs2.Close();
		}
	}
}
