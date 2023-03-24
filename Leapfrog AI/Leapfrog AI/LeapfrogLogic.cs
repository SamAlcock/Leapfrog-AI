using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leapfrog_AI
{
    internal class LeapfrogLogic
    {
        static void Main(string[] args)
        {
            int maxTrials = 250;
            int button1Score = 10;
            int button2Score = 20;
            int scoreTotal = 0;
            int choice = 0;
            Program program = new Program();


            int[] inputs = program.choices;

            Task task = new();


            for (int i = 0; i < maxTrials; i++)
            {
                choice = inputs[i];
                int[] numbers = task.Leapfrog(choice, scoreTotal, button1Score, button2Score);

                scoreTotal = numbers[0];
                button1Score = numbers[1];
                button2Score = numbers[2];
            }

        }

        class Task
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
                Random random = new Random();
                int prob = 1; // Probability of buttons jumping
                int randy = random.Next(8);

                if (randy < prob)
                {
                    if (choice == 0 && button2Score < button1Score) 
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
        }

        

    }
}
