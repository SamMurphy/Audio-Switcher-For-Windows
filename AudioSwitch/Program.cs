using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SetDefaultAudioEndpoint;

namespace AudioSwitch
{
    class Program
    {
        private static List<string> audioDevices = new List<string>();
        static void Main(string[] args)
        {
            SetDefaultAudioEndpoint.WindowsSound sound = new SetDefaultAudioEndpoint.WindowsSound();
            bool switched = false;

            if (args.Length > 0)
            {
                if (args[0].EndsWith(".txt"))
                {
                    ReadAudioDevices(args[0]); // If txt file is an argument then load audio devices from file
                }
            }
            if (args.Length == 0 || audioDevices.Count <= 0) // If no file then load default devices
            {
                audioDevices.Add("Headphones");
                audioDevices.Add("Speakers");
            }


            string path = System.Windows.Forms.Application.StartupPath + @"\currentDeviceID.txt";
            
            int i = GetLastSetDevice(path) + 1;
            while (!switched)
            {
                if (i > audioDevices.Count - 1) i = 0; // reset i
                try
                {
                    sound.SetDefaultAudioPlaybackDevice(audioDevices[i]); // Try and set it to Headphones
                    sound.Dispose();
                    Console.WriteLine("SWITCHED TO " + audioDevices[i].ToUpper());
                    switched = true;
                    System.IO.File.WriteAllText("currentDeviceID.txt", i.ToString());
                }
                catch (Exception e) // If you can't then try set it to Speakers
                {
                    //Console.WriteLine("EXECPTION:\n");
                    //Console.WriteLine(e.Message);
                }
                finally
                {
                    i++;
                }
            }
            System.Threading.Thread.Sleep(500);

           
        }

        static void ReadAudioDevices(string fileName)
        {
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            string line;
            int counter = 0;
            while((line = file.ReadLine()) != null)
            {
                audioDevices.Add(line);
                counter++;
            }
            file.Close();
        }

        static int GetLastSetDevice(string fileName)
        {
            int deviceNumber = 0;

            try
            {
                bool success = false;
                System.IO.StreamReader file = new System.IO.StreamReader(fileName);
                string line;
                int counter = 0;
                while ((line = file.ReadLine()) != null)
                {
                    success = Int32.TryParse(line, out deviceNumber);
                    counter++;
                }
                file.Close();
                if (success && deviceNumber < audioDevices.Count) return deviceNumber;
            }
            catch (Exception e)
            {

            }
            return deviceNumber;
        }
    }
}
