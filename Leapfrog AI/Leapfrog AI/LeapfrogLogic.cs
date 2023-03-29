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
            public int[] Leapfrog(int choice, int score, int button1Score, int button2Score)
            {
                if (choice == 0)
                {
                    score += button1Score;
                }
                else if (choice == 1)
                {
                    score += button2Score;
                }

                Tuple<int, int> buttonScores = CheckForIncrease(choice, button1Score, button2Score);
                button1Score = buttonScores.Item1;
                button2Score = buttonScores.Item2;

                int[] numbers = {score, button1Score, button2Score};

                return numbers;
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

            public double CalculateAverage(List<int> scores)
            {
                double avg = scores.Average(); // Calculates average of list

                return avg;
            }

        }

        

    }
}
