namespace Leapfrog_AI
{
    class Program
    {

        /* - AI makes a random decision (button 1 or 2)
           - Is given score added to total
           - Runs through whole trial
           - Compared to previous ouput (its parent)
           - Best output becomes new parent
           - Data saved to .csv - score, button pressed
           - Loops (multiple AI - to avoid a specific tactic?, multiple generations)
           - Make data into graph - should be a bell curve? Optimal score should be top of curve, extreme luck scores at the end */


        static void Main(string[] args)
        {
            Program.Hillclimber hillclimber = new();

            hillclimber.Hillclimb(2500); // Increase to get more chance to mutate

            Console.ReadLine();
        }

        class Solution
        {
            int numOfInputs = 2;
            int numOfInstructions = 1000;

            public List<int> GenerateSolution() // Generate a random array of inputs
            {
                List<int> instructions = new();
                Random rand = new();

                for (int i = 0; i < numOfInstructions; i++)
                {
                    instructions.Add(rand.Next(0, numOfInputs));
                }

                return instructions;
            }
            public double FindConstraints(List<int> instructions)
            {
                int maxTrials = 250;
                
                Program program = new Program();

                List<int> outputs = new List<int>();

                LeapfrogLogic.Task task = new();

                for (int h = 0; h < 200; h++) // To get an average score - increase to reduce randomness/luck
                {
                    int button1Score = 10;
                    int button2Score = 20;
                    int scoreTotal = 0;
                    for (int i = 0; i < maxTrials; i++) // For every trial
                    {
                        int choice = instructions[i];
                        int[] numbers = task.Leapfrog(choice, scoreTotal, button1Score, button2Score); // Get relevant numbers

                        scoreTotal = numbers[0];
                        button1Score = numbers[1];
                        button2Score = numbers[2];
                    }
                    outputs.Add(scoreTotal);
                }

                double avg = task.CalculateAverage(outputs);

                return avg;
            }
            

        }
        class Mutate
        {
            public List<int> RuinRecreate(int numOfInstructions) // Random resetting?
            {
                List<int> child = new();
                Random rand = new();

                for (int i = 0; i < numOfInstructions; i++)
                {
                    child.Add(rand.Next(0, 2));
                }

                return child;
            }

            public List<int> SessionReplace(int numOfInstructions, List<int> parent) // Swap mutation?
            {
                Random rand = new();

                List<int> child = new List<int>(parent);

                for (int i = 0; i < 25; i++)
                {
                    int temp;
                    int location = rand.Next(0, numOfInstructions);
                    int location2 = rand.Next(0, numOfInstructions);

                    while (location == location2)
                    {
                        location2 = rand.Next(0, numOfInstructions);
                    }

                    temp = location;
                    child[location] = child[location2];
                    child[location2] = child[temp];
                }
                

                return child;
            }

        }

        public class Hillclimber
        {
            public void Hillclimb(int Niter)
            {
                /* 
                 * HILLCLIMBER 
                 * - Compare child and parent fittness
                 * - Best fittness becomes next parent
                 */

                Solution solution = new();
                Mutate mutate = new();

                Console.WriteLine("Initialising...");
                // Initialise random solution
                List<int> parent_instructions = solution.GenerateSolution();
                double parent_constraints = solution.FindConstraints(parent_instructions);

                // Loop for Niter 
                for (int i = 0; i < Niter; i++)
                {
                    // Mutate
                    List<int> child_instructions = mutate.SessionReplace(parent_instructions.Count, parent_instructions);

                    // List<int> child_instructions = mutate.RuinRecreate(parent_instructions.Count); Ruin recreate mutation
                    // Evaluate
                    double child_constraints = solution.FindConstraints(child_instructions);

                    // Pick the next parent
                    if (child_constraints > parent_constraints)
                    {
                        parent_instructions = child_instructions.ToList();
                        parent_constraints = child_constraints;

                        Console.WriteLine("New best average: " + parent_constraints);
                    }

                }

                Console.WriteLine(String.Join(", ", parent_instructions));
                Console.WriteLine("Average: " + parent_constraints);




            }
        }

    }

    
}