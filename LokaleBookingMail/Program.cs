using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Model;

using Limilabs.Client.IMAP;
using Limilabs.Client.POP3;
using Limilabs.Client.SMTP;
using Limilabs.Mail;
using Limilabs.Mail.MIME;
using Limilabs.Mail.Fluent;
using Limilabs.Mail.Headers;

namespace LokaleBookingMail
{
    class Program
    {
        static void Main()
        {
            loopCheckDb();
        }

        private static void loopCheckDb()
        {
            while (true)
            {
                using (var ctx = new Context())
                {
                    var bookings = from b in ctx.Bookings select b;

                    

                    foreach (var book in bookings.Where(b => b.SendtMail == false))
                    {
                        //Bruger bruger = book.Bruger;
                        sendMail(book.Bruger.Fornavn, book.Bruger.Efternavn, book.Bruger.Mail, book.StartTidspunkt.ToString("dd-MM-yyyy"), book.SlutTidspunkt.ToString("dd-MM-yyyy"), book.Lokale.LokaleNavn);

                        Booking books = (from s in ctx.Bookings
                                         where s.BookingID == book.BookingID
                                         select s).FirstOrDefault();
                        books.SendtMail = true;

                        ctx.SaveChanges();
                    }
                }

                Thread.Sleep(5000);
            }
        }

        private static void inbox()
        {
            using (Imap imap = new Imap())
            {
                imap.ConnectSSL("imap.gmail.com");
                imap.UseBestLogin("h3elev@gmail.com", "awesomepass");

                imap.SelectInbox();

                List<long> uids = imap.Search(Flag.Unseen);
                foreach (long uid in uids)
                {
                    var eml = imap.GetMessageByUID(uid);
                    IMail mail = new MailBuilder().CreateFromEml(eml);

                    MessageBox.Show(mail.Subject);
                    Console.WriteLine(mail.Subject);
                    Console.WriteLine(mail.Text);
                }
                imap.Close();
            }
        }

        private static void sendMail(string fornavn, string efternavn, string mail, string start, string slut, string lokale)
        {
            MailBuilder builder = new MailBuilder();
            builder.From.Add(new MailBox("h3elev@gmail.com", "LokaleBookingSystem"));
            builder.To.Add(new MailBox(mail, fornavn + " " + efternavn));
            builder.Subject = "lokalebooking";
            builder.Text = "Du har booket lokalet "+ lokale +" fra "+ start +" til " + slut;
            IMail email = builder.Create();

            using (Smtp smtp = new Smtp())
            {
                smtp.Connect("smtp.gmail.com");  // or ConnectSSL for SSL
                smtp.UseBestLogin("h3elev@gmail.com", "awesomepass");

                ISendMessageResult result = smtp.SendMessage(email);
                if (result.Status == SendMessageStatus.Success)
                {
                    // Message was sent.
                    MessageBox.Show("mail sent");
                }

                smtp.Close();
            }

        }

    }
}
