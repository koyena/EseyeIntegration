using System;
using System.Net;



namespace EseyeWeb
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Class1
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
            
            String cname, cval,iccid;
            Cookie ck = new Cookie();
            EseyeWeb.eseye.OpenTigrilloService eseye = new EseyeWeb.eseye.OpenTigrilloService();
            EseyeWeb.eseye.ICCFullInformation iccfullinfo = new EseyeWeb.eseye.ICCFullInformation();
            EseyeWeb.eseye.PageSelector pgselector = new EseyeWeb.eseye.PageSelector();
            EseyeWeb.eseye.GPRSActivity[] gprsactivity = new EseyeWeb.eseye.GPRSActivity[100];
            EseyeWeb.eseye.ICCSummaryInformation iccsummary;
            EseyeWeb.eseye.GPRSSummary[] gprssum = new EseyeWeb.eseye.GPRSSummary[100];
            eseye.Proxy = setupProxy("172.16.4.21:8080", "xxxx", "xxxx");
            // The folowing line is for ignoring the SSL certificate error 
            ServicePointManager.ServerCertificateValidationCallback +=(sender, cert, chain, sslPolicyErrors) => true;
			EseyeWeb.eseye.AuthenticationInformation authinfo = new EseyeWeb.eseye.AuthenticationInformation();
			authinfo.emailAddress="xxxx.yyyy@abc.com";
			authinfo.password="ccc";
			authinfo.portalId="6b57a762-1510-42aa-0057-4948f67d63cc";
			authinfo.portfolioId= null;
            eseye.CookieContainer = new System.Net.CookieContainer();
            
			String strg = eseye.login(authinfo);
            foreach (Cookie cookie in eseye.CookieContainer.GetCookies(new Uri("https://siam.eseye.com")))
            {
                //Console.WriteLine("Name = {0} ; Value = {1} ; Domain = {2}", cookie.Name, cookie.Value,
                                  //cookie.Domain);
                cname = cookie.Name;
                cval = cookie.Value;
                ck=cookie;
            }
            eseye.CookieContainer.Add(ck);
           // strg = eseye.logout();
            // Console.WriteLine( eseye.CookieContainer.GetCookies(new Uri("https://siam.eseye.com")).ToString());
            iccid = "8944538523005646479";
            pgselector.startItems = 1;
            pgselector.numItems = 100;
            iccfullinfo = eseye.getICCFullInfo(iccid,authinfo);
            Console.WriteLine("IMSI = {0}  Mobile Number = {1}", iccfullinfo.SIMInfo.IMSI.ToString(),iccfullinfo.SIMInfo.DataMSISDN.ToString());
            gprsactivity = eseye.getGPRSActivity(iccid, pgselector, null,authinfo);
            gprssum = eseye.getICCNFXData(iccid, null, pgselector, authinfo);
            //for (int i = 0; i <= gprsactivity.Length; i++)
            //{
            //    Console.WriteLine("GPRS usage {0}", gprsactivity[i]);
                        //}

            iccsummary = eseye.getICCSummaryInfo(iccid, authinfo);
            Console.WriteLine("Last Connected           {0}", iccsummary.lastConnected);
            Console.WriteLine("Last Seen                {0}", iccsummary.lastSeen);
            Console.WriteLine("This month data usage    {0} bytes ", iccsummary.dataThisMonth);
            Console.WriteLine("SMS usage                {0} nos ", iccsummary.smsThisMonth);


            strg = eseye.logout();

            
            
            
            Console.WriteLine(strg);


		}

        public static  WebProxy setupProxy(string proxyaddress, string username, string password)
        {
            WebProxy wp = new WebProxy(proxyaddress);
            wp.Credentials = new NetworkCredential(username, password);
            return wp;
        
        }
       
	}
}
