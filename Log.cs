/*
 * Created by SharpDevelop.
 * User: s4
 * Date: 2013/06/08
 * Time: 1:58
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;


namespace ss
{
	/// <summary>
	/// Description of Log.
	/// </summary>
	public  class Log
	{
		protected FileInfo m_srcFile;
		protected FileInfo m_destFile;
		
		public bool Copy(){
			//
			if(m_destFile.Exists)
				return true;
			
			if(!m_destFile.Directory.Exists)
				m_destFile.Directory.Create();
			
			FileInfo ret = m_srcFile.CopyTo(m_destFile.FullName, true);
			return ret.Exists;
		}
		
		public ArrayList Check(SearchWord word){
			//
			System.IO.StreamReader sr = new System.IO.StreamReader( m_destFile.FullName,  System.Text.Encoding.GetEncoding("shift_jis"));
			ArrayList foundList = new ArrayList();

			while (sr.Peek() > -1)
			{
    			string stringLine = sr.ReadLine();

    			foreach(string bw in word.BlackList){
    				//
    				if(stringLine.IndexOf(bw)<0){
    					//
    					goto continue_next_list;	
    				}
    				
    				//found
    				foreach(string ww in word.WhiteList){
    					//
    					if(stringLine.IndexOf(ww)>=0){
    						//
    						goto continue_next_list;
    					}
    				}
    				
    				//found
    				foundList.Add(stringLine);
    				
    				continue_next_list:	;
    			}
    			
			}

			sr.Close();
			
			return foundList;
		}
		
		public string Search(string regexPattern){
			//
			Regex r = new Regex(regexPattern, RegexOptions.IgnoreCase);
			System.IO.StreamReader sr = new System.IO.StreamReader( m_destFile.FullName,  System.Text.Encoding.GetEncoding("shift_jis"));

			
			try{
			while (sr.Peek() > -1){
				//
				string s = sr.ReadLine();
				if(r.IsMatch(s)){
					//
					Match m = r.Match(s);
					return m.Groups["target"].Value;
				}
			}
			return "-";
			}finally{
				//
				sr.Close();
			}

		}

		protected Log(){
			//
		}
		
		public Log(string srcDir, string srcFilename, string destDir)
		{
			m_srcFile = new FileInfo(srcDir + srcFilename);
			m_destFile = new FileInfo(destDir + "\\" + m_srcFile.Name);
		
		}
	}
	
	public class ConsoleLog : Log{
		//
		public string DIR = "D$\\HITU022H\\PASLDATD\\";
		public string FILE = "aaaSV.txt";
		public string STARTUPPATTERN = @"[(>\s+(?<target>.*)\s+なんとか)|(eee\s+<?(target>.*)\s+)]";
		public bool isStartUp;
		public bool isError;
		
		public ConsoleLog(string server_name, string destDir) : base(){
			//
			this.m_srcFile = new FileInfo(DIR + FILE.Replace("{SERVER_NAME}", server_name));
			this.m_destFile = new FileInfo(destDir + "\\" + m_srcFile.Name);
		}
		public ConsoleLog() : base(){
			//
			this.m_srcFile = new FileInfo(DIR + FILE.Replace("{SERVER_NAME}", "default"));
			this.m_destFile = new FileInfo(Directory.GetCurrentDirectory() + "\\e" + m_srcFile.Name);
		}
		
		public void Init(){
			//
			this.m_srcFile = new FileInfo(DIR + FILE);
			this.m_destFile = new FileInfo(Directory.GetCurrentDirectory() + "\\" + System.DateTime.Now.ToString("yyyyMMdd") +    "\\" + m_srcFile.Name);
		}
		
		public string GetStartUptime(){
			//
			if(!isStartUp)
				return "";
			Init();
			Copy();

			return Search(STARTUPPATTERN);
		}
		public string[] GetErrorStringList(SearchWord searchWord){
			if(!isError)
				return new string[0];
			Init();
			Copy();
			return (string[])this.Check(searchWord).ToArray(typeof(string));
		}
		
		
	}
	
	public class WorkFlowServiceLog : Log{
		//
		public  string DIR = "D$\\TEMP";
		public  string FILE = "HitkotWFService.log";
		public bool isEnabled ;
		public SearchWord searchWord;
		
		public WorkFlowServiceLog(string destDir) : base(){
			this.m_srcFile = new FileInfo(DIR + FILE);
			this.m_destFile = new FileInfo(destDir + "\\" + m_srcFile.Name);
			isEnabled = true;
		}
		
		public WorkFlowServiceLog() : base(){
			this.m_srcFile = new FileInfo(DIR + FILE);
			this.m_destFile = new FileInfo(Directory.GetCurrentDirectory() + "\\" + m_srcFile.Name);
			isEnabled = false;

		}
		
		public void Init(){
			//
			this.m_srcFile = new FileInfo(DIR + FILE);
			this.m_destFile = new FileInfo(Directory.GetCurrentDirectory() + "\\" + System.DateTime.Now.ToString("yyyyMMdd") +    "\\" + m_srcFile.Name);

		}
		public string Check(){
			if(!isEnabled)
				return "";
			Init();
			Copy();
			return Check(this.searchWord).Count.ToString();
		}
	}
	
	public class SearchWord{
		//
		public static readonly string PATH = "blackList.xml";
		
		[System.Xml.Serialization.XmlArrayItem(typeof(string))]
		private ArrayList m_blackList;
		[System.Xml.Serialization.XmlArrayItem(typeof(string))]
		private ArrayList m_whiteList;
		
		public ArrayList WhiteList{
			get { return m_whiteList;}
		}
		public ArrayList BlackList{
			get { return m_blackList;}
		}
		
		public override string ToString(){
			//
			System.Text.StringBuilder builder = new System.Text.StringBuilder();
			//regex?
			return "";
		}
		
		public SearchWord(){
			m_blackList = new ArrayList();
			m_whiteList = new ArrayList();
		}
	}
}
