using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SendMail
{
    using eMeL_Mail;
    using eMeL_Csv;

    public class Program
    {
        static int Main(string[] args)
        {
            Console.WriteLine("*** Send e-mail *** (c) eMeL, www.emel.hu *** freeware ****\n");

            if (((args.Length != 1) && (args.Length < 3)) || ((args.Length > 0) && ((args[0] == "-h") || (args[0] == "-H") || (args[0] == "-?"))))
            {
                Console.WriteLine("usage: SendMail.exe emailAddr subject body [attachmentFilenames] \n" +
                                  "       where subject & body is text or filename.\n");
                Console.WriteLine("...press any key to continue...");
                Console.ReadKey();
                return 1;
            }

            //

            try
            {
                if (args.Length == 1)
                {
                    string csvFilename = args[0];

                    if (! File.Exists(csvFilename))
                    {
                        throw new Exception("The CSV filename isn't exists!");
                    }

                    var    csv         = new CsvDocument();
                    var    csvEncoding = Encoding.UTF8;                                                                 // no parameter for this yet
                    string csvText     = File.ReadAllText(csvFilename, csvEncoding);

                    csv.LoadCsv(csvText);                                                                               // csv[0, 0] = "hello";  csv.RowCount; csv.ColumnCount;

                    if (! csv.HasHeader)
                    {
                        throw new Exception("Threre isn't header in CSV file!");
                    }

                    if (csv.RowCount < 1)
                    {
                        throw new Exception("Threre aren't data rows in CSV file!");
                    }

                    //

                    var sendMails = new SendMails(csv.HeaderColumns);

                    for (int rowIndex = 0; rowIndex < csv.RowCount; rowIndex++)
                    {
                        sendMails.Send(csv[rowIndex]);
   
                        string emailTo = csv[rowIndex, sendMails.toColumn];

                        Console.WriteLine($"  > An email to {emailTo} is sent!");
                    }
                }
                else
                {
                    string emailAddr = args[0];
                    string subject = args[1];
                    string body = args[2];

                    List<string> attachmentFilenames = null;

                    if (args.Length > 3)
                    {
                        attachmentFilenames = new List<string>();

                        for (int i = 3; i < args.Length; i++)
                        {
                            if (!File.Exists(args[i]))
                            {
                                Console.WriteLine($"Error! The attachment filename is invalid! [{args[i]}] \n");
                            }

                            attachmentFilenames.Add(args[i]);
                        }
                    }

                    //

                    Send(emailAddr, subject, body, attachmentFilenames);
                }
            }
            catch (Exception e)
            {
                Console.Beep();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.WriteLine("eMail send is unsuccesfull!");
                Console.BackgroundColor = ConsoleColor.White;
                Console.WriteLine(e.ToString());
                Console.ResetColor();
                Console.WriteLine("...press any key to continue...");
                Console.ReadKey();
                return 2;
            }

            Console.WriteLine("eMail send was succesfull!");
            return 0;
        }

        private static void Send(string email, string subject, string body, IEnumerable<string> attachmentFilenames = null)
        {                   
            Console.Write($"Send email to {email} ");
            int left = Console.CursorLeft;
            int top  = Console.CursorTop;
            Console.WriteLine();

            if (File.Exists(subject))
            {
                subject = File.ReadAllText(subject);
            }

            if (File.Exists(body))
            {
                body = File.ReadAllText(body);
            }

            var sendMail = new SendMail();

            Console.CursorLeft = left;
            Console.CursorTop  = top;
            Console.WriteLine($"from {sendMail.sendMailParameter.from}");

            sendMail.Send(email, subject, body, attachmentFilenames);
        }              
    }
}
