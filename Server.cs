/*
 * Created by SharpDevelop.
 * User: s4
 * Date: 2013/06/08
 * Time: 10:48
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;

namespace ss
{
	/// <summary>
	/// Description of ServerSetting.
	/// </summary>
	/// 
	public class Server
	{
		public static readonly string PATH1 = "serverList1.xml";
		public static readonly string PATH2 = "serverList2.xml";
		
		public string name;
		public string HostName;
		public ConsoleLog consoleLog;
		public WorkFlowServiceLog wfserviceLog;
		public bool isEtc;
		
		
		public string StartUpCheck(){
			return consoleLog.GetStartUptime();
		}
		public string[] StartUpErrorCheck(SearchWord searchWord){
			return consoleLog.GetErrorStringList(searchWord);
		}
		
		public string WfServiceLogCheck(){
			return wfserviceLog.Check();
		}
		
		public Server()
		{
			name = "HTSELDB1";
			HostName = "\\192.168.101.26\\";
			consoleLog = new ConsoleLog(name, System.IO.Directory.GetCurrentDirectory());
			wfserviceLog = new WorkFlowServiceLog();
			isEtc = true;
		}
	}
}
