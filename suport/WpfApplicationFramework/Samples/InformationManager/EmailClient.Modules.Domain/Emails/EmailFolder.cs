using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Waf.InformationManager.Common.Domain;
using System.Runtime.Serialization;

namespace Waf.InformationManager.EmailClient.Modules.Domain.Emails
{
    [DataContract]
    public class EmailFolder : ValidationModel
    {
        [DataMember] private readonly ObservableCollection<Email> emails;


        public EmailFolder()
        {
            this.emails = new ObservableCollection<Email>();
        }


        public ICollection<Email> Emails { get { return emails; } }

        internal IEmailDeletionService EmailDeletionService { get; set; }


        public void AddEmail(Email email)
        {
            emails.Add(email);
        }

        public void RemoveEmail(Email email)
        {
            EmailDeletionService.DeleteEmail(this, email);
        }
    }
}
