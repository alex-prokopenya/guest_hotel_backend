using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuestService.Notifications
{
    class EmailMessage
    {
        public EmailMessage()
        { }

        private string _subject;

        public string Subject
        {
            get
            {
                return _subject;
            }

            set
            {
                _subject = value;
            }
        }

        private string _body;

        public string Body
        {
            get
            {
                return _body;
            }

            set
            {
                _body = value;
            }
        }
    }
}
