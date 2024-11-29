using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace HedgehokTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] colors = { "red", "green", "blue" };
            int[] population = new int[3];

            bool isValidCombination = false;

            int neededColony, smallerColony, largerColony;
            int roundCounter = 0;
            int total = 0;

            total = GetPopulationInput(colors, population, total);

            if (!IsValidTotalPopulation(total))
            {
                return;
            };

            Display(population);

            neededColony = GetValidColorInput();

            (smallerColony, largerColony) = GetSmallerAndLargerColonies(neededColony, population);

            while (population[smallerColony] + population[largerColony] != 0)
            {
                if (IsImpossibleToContinue(population, smallerColony, largerColony))
                {
                    isValidCombination = false;
                    break;
                };

                if (population[smallerColony] == 0 && population[smallerColony] != population[largerColony])
                {
                    population[neededColony] -= 1;
                    population[largerColony] -= 1;
                    population[smallerColony] += 2;
                    roundCounter++;
                }
                else
                {
                    population[neededColony] += 2;
                    population[smallerColony] -= 1;
                    population[largerColony] -= 1;
                    roundCounter++;
                };
                isValidCombination = true;
            };

            Display(population);
            int result = isValidCombination ? roundCounter : -1;
            Console.WriteLine($"result:\t\t\t{result}");
        }


        static int GetPopulationInput(string[] colors, int[] population, int total)
        {
            for (int i = 0; i < population.Length; i++)
            {
                bool isValidInput = false;
                while (!isValidInput)
                {
                    try
                    {
                        Console.Write($"{colors[i]}({i}) hedgehogs:\t");
                        int input = int.Parse(Console.ReadLine());
                        if (input >= 0)
                        {
                            population[i] = input;
                            total += input;
                            isValidInput = true;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input.\nPlease enter a non-negative integer.");
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid input.\nPlease enter a valid integer.");
                    }
                }
            }

            return total;
        }

        static bool IsValidTotalPopulation(int total)
        {
            if (total < 1 || total > int.MaxValue)
            {
                Console.WriteLine($"Invalid total population.\nThe sum must be between 1 and {int.MaxValue}.");
                return false;
            }
            return true;
        }

        // check for impossibility of fulfilling the condition
        static bool IsImpossibleToContinue(int[] population, int smallerColony, int largerColony)
        {
            return population[smallerColony] + population[largerColony] == 1;
        }

        static int GetValidColorInput()
        {
            int neededColony = -1;
            bool isValidColor = false;

            while (!isValidColor)
            {
                Console.Write($"needed color:\t\t");
                try
                {
                    neededColony = int.Parse(Console.ReadLine());

                    if (neededColony >= 0 && neededColony <= 2)
                    {
                        isValidColor = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input.\nPlease enter a number between 0 and 2.");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input.\nPlease enter a valid integer.");
                }
            }

            return neededColony;
        }

        static (int, int) GetSmallerAndLargerColonies(int neededColony, int[] population)
        {
            int smallerColony, largerColony;
            switch (neededColony)
            {
                case 0:
                    if (population[1] != population[2])
                    {
                        smallerColony = population[1] < population[2] ? 1 : 2;
                        largerColony = population[1] > population[2] ? 1 : 2;
                    }
                    else
                    {
                        smallerColony = 1;
                        largerColony = 2;
                    }
                    break;
                case 1:
                    if (population[0] != population[2])
                    {
                        smallerColony = population[0] < population[2] ? 0 : 2;
                        largerColony = population[0] > population[2] ? 0 : 2;
                    }
                    else
                    {
                        smallerColony = 0;
                        largerColony = 2;
                    }
                    break;
                case 2:
                    if (population[0] != population[1])
                    {
                        smallerColony = population[0] < population[1] ? 0 : 1;
                        largerColony = population[0] > population[1] ? 0 : 1;
                    }
                    else
                    {
                        smallerColony = 0;
                        largerColony = 1;
                    }
                    break;
                default:
                    throw new ArgumentException("invalid colony index");
            }
            return (smallerColony, largerColony);
        }

        static void Display(int[] values)
        {
            Console.Write($"hedgehog population:\t");
            foreach (int value in values)
            {
                Console.Write($"{value}; ");
            }
            Console.WriteLine();
        }
    }
}
