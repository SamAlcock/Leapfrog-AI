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
            Hillclimber hillclimber = new();

            hillclimber.Hillclimb(2500);

            Console.ReadLine();

        }
        class Solution
        {
            int numOfDirections = 5;
            int numOfInstructions = 1000;

            public List<int> GenerateSolution() // Generate a random array of directions
            {
                List<int> instructions = new();
                Random rand = new();

                for (int i = 0; i < numOfInstructions; i++)
                {
                    instructions.Add(rand.Next(0, numOfDirections));
                }

                return instructions;
            }
            public int FindConstraints(List<int> instructions)
            {
                /*
                 * - Loop through every item
                 * - If direction is the same as previous (excluding staying still)
                 * - Add 1 constraint
                 * 
                 * Stay still = 0, Forward = 1, Backward = 2, Left = 3, Right = 4
                 * 
                 * if 1 and 2, 3 and 4, 2 and 1, 4 and 3
                 */

                int constraints = 0;

                for (int i = 0; i < numOfInstructions; i++)
                {
                    if (i != 0)
                    {
                        if (instructions[i] == 1 && instructions[i - 1] == 2 || instructions[i] == 2 && instructions[i - 1] == 1 || instructions[i] == 3 && instructions[i - 1] == 4 || instructions[i] == 4 && instructions[i - 1] == 3)
                        {
                            constraints++;
                        }
                    }

                }
                // Console.WriteLine("There are " + constraints + " constraints");
                return constraints;
            }
        }
        class Mutate
        {


            public void RuinRecreate() // Random resetting?
            {

            }

            public List<int> SessionReplace(int numOfInstructions, List<int> parent) // Swap mutation?
            {
                Random rand = new();

                List<int> child = new List<int>(parent);

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

                return child;
            }

        }

        class Hillclimber
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

                // Initialise random solution
                List<int> parent_instructions = solution.GenerateSolution();
                int parent_constraints = solution.FindConstraints(parent_instructions);

                // Loop for Niter 
                for (int i = 0; i < Niter; i++)
                {
                    // Console.WriteLine("Iteration: " + i);

                    // Mutate
                    List<int> child_instructions = mutate.SessionReplace(parent_instructions.Count, parent_instructions);
                    // Evaluate
                    int child_constraints = solution.FindConstraints(child_instructions);

                    // Pick the next parent
                    if (child_constraints < parent_constraints)
                    {
                        parent_instructions = child_instructions.ToList();
                        parent_constraints = child_constraints;
                    }

                    Console.WriteLine("There are " + parent_constraints + " constraints");
                }

                Console.WriteLine(String.Join(", ", parent_instructions));
                Console.WriteLine("Constraints: " + parent_constraints);




            }
        }

    }

    
}