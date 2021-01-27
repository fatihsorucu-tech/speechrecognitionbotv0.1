using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Net.Mail;
using System.Net;


using System.Net.Mail;namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Choices commands = new Choices();
            commands.Add(new string[] { "send email", "check whatsapp", "open google", "tell me what time it is" });
            GrammarBuilder s = new GrammarBuilder();
            s.Append(commands);
            Grammar gr = new Grammar(s);
            recEngine.LoadGrammarAsync(gr);
            recEngine.SetInputToDefaultAudioDevice();
            recEngine.SpeechRecognized += recEngine_SpeechRecognized;

            
        }
        void recEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text)
            {
                case "send email":
                    Email(e.Result.Text);
                    break;
                case "check whatsapp":
                    System.Diagnostics.Process.Start("https://web.whatsapp.com/");
                    break;
                case "open google":
                    System.Diagnostics.Process.Start("https://web.whatsapp.com/");
                    break;
                case "tell me what time it is":
                    break;
            }

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            recEngine.RecognizeAsyncStop();
            button1.Enabled = false;
        }

        private void btnenable_Click(object sender, EventArgs e)
        {
            recEngine.RecognizeAsync(RecognizeMode.Multiple);
            button1.Enabled = true;
        }
        public static void Email(string htmlString)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("FromMailAddress");
                message.To.Add(new MailAddress("ToMailAddress"));
                message.Subject = "Test";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = htmlString;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("FromMailAddress", "password");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception) { }
        }  

    }
}
