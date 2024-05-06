using System;
namespace eMKParty.BackOffice.Support.Application.DTOs
{
	public class EmailRequestDto
	{
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string From { get; set; }
    }
}