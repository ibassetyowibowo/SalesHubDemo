namespace KSO_SalesHub.Models.Common
{

	public class Email
	{
		public Email()
		{
			attachments = new List<MailAttach>();
		}
		public string subject { get; set; }
		public List<string> to { get; set; }
		public List<string> cc { get; set; }
		public string bodyEmailHTML { get; set; }
		public List<MailAttach> attachments { get; set; }
	}

	public partial class MailAttach
	{
		public string fileName { get; set; }
		public string fileByte { get; set; }
	}


	public class BundleMail
	{

	}

}
