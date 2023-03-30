using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Leapfrog_AI
{
    internal class LeapfrogLogic
    {
        public class Task
        {
            public Tuple<int[], List<int>> Leapfrog(int choice, int score, int button1Score, int button2Score, int prevChoice, List<int> exploreExploit)
            {
                if (choice == 0)
                {
                    score += button1Score;
                }
                else if (choice == 1)
                {
                    score += button2Score;
                }
                exploreExploit = DetermineChoice(choice, prevChoice, exploreExploit);
                Tuple<int, int> buttonScores = CheckForIncrease(choice, button1Score, button2Score);
                button1Score = buttonScores.Item1;
                button2Score = buttonScores.Item2;

                int[] numbers = {score, button1Score, button2Score, choice};

                return Tuple.Create(numbers, exploreExploit);
            }

            Tuple<int, int> CheckForIncrease(int choice, int button1Score, int button2Score)
            {
                Random random = new();
                int prob = 1; // Probability of buttons jumping
                int randy = random.Next(8);

                if (randy < prob)
                {
                    if (choice == 0 && button2Score < button1Score) // if button 1 pressed, and its score is higher than button 2
                    {
                        button2Score += 20;
                    }
                    else if (choice == 1 && button1Score < button2Score)
                    {
                        button1Score += 20;
                    }
                }

                return Tuple.Create(button1Score, button2Score);
            }

            List<int> DetermineChoice(int choice, int prevChoice, List<int> exploreExploit)
            {
                /* To figure out whether choice was explore or exploit:
                 * High button + stay = exploit
                 * High button + leave = explore
                 * Low button + stay = exploit
                 * Low button + leave = explore
                 */

                if (choice == prevChoice)
                {
                    exploreExploit.Add(0);
                }
                else if (choice != prevChoice)
                {
                    exploreExploit.Add(1);
                }
                

                return exploreExploit;
            }

            public double CalculateAverage(List<int> scores)
            {
                double avg = scores.Average(); // Calculates average of list

                return avg;
            }

        }

        

    }
}
