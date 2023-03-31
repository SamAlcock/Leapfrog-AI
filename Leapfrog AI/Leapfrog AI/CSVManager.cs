using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leapfrog_AI
{
    public class CSVManager
    {
        public List<int> Data = new();

        public void Main()
        {

            string path = "leapfrog.csv"; // Location of .csv file

            var participant = new List<ParticipantEntry>(); // Create a list of all of participants entries

            bool exists = CheckIfFileExists(path); // Check if file exists - have to do this now instead of where the 'if (!exists)' is as the file already exists by then

            for (int i = 1; i < Data.Count/2; i++) // Instantiate for data entries
            {
                // Assign this data to a new instance of ParticipantEntry
                participant.Add(new ParticipantEntry { MaxIterations = (int)Data[0], AvgScore = (int)Data[1], ExploreExploit = (int)Data[i + 2], Inputs = (int)Data[i + 252]});
            }

            using FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write); // Allow appending to keep already recorded data
            using StreamWriter sw = new StreamWriter(fs);

            if (!exists) // Create the column headings if the file didn't exist previously
            {
                sw.WriteLine("Maximum Iterations,Average Score,Explore/Exploit,Inputs");
            }

            InputData(participant, sw);
        }

        void InputData(List<ParticipantEntry> participant, StreamWriter writer) // Inputs every instances attribute in the correct format
        {
            for (int i = 0; i < participant.Count; i++)
            {
                writer.WriteLine(participant[i].MaxIterations + "," + participant[i].AvgScore + "," + participant[i].ExploreExploit + "," + participant[i].Inputs);
            }
        }

        bool CheckIfFileExists(string path) // Checks if file exists before writing 
        {
            if (File.Exists(path))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public class ParticipantEntry // Definitions for each entries attributes
        {
            public int MaxIterations { get; set; }

            public int AvgScore { get; set; }
            public int ExploreExploit { get; set; }
            public int Inputs { get; set; }
            

        }
    }
}
