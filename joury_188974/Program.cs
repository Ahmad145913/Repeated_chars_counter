using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace joury_188974
{
    internal class Program
    {
        /* define a static variables and arrays to use in the whole program */
        static int[] xchar = new int[255];
        static int k = 0;
        static int max, min, count, lent;
        static int ascii;
        static string[] Questions;
        static char[] CorrectAnswers;
        static char[] UserAnswer;
        static int num_rand_created;
        static string[] type_of_quest;
        static int right_counter = 0;
        static int false_counter = 0;
        static int compare = 0;
        static int second_compare = 0;
        static char nchar = ' ';
        static void Main(string[] args)
        {
            Random rand = new Random();  //random var to use in the code
            int Quest_num;
            string ss = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789";  //number and alphabet string 
            Console.WriteLine("please enter the maximum number of questions: ");
            Quest_num = int.Parse(Console.ReadLine());  //read number of questions from the user
            Questions = new string[Quest_num];  //limit the range and the length of the arrays
            CorrectAnswers = new char[Quest_num];
            UserAnswer = new char[Quest_num];
            type_of_quest = new string[Quest_num];
            int quest_counter = 0;
            int j; string str;
            while (quest_counter < Quest_num)
            {
                Console.WriteLine("\nQuestion " + ++quest_counter + "\n----------------"); // the number of questions
                Console.WriteLine("Please enter an ineger value between 3 and 100 \n(The number of characters from which to enumerate the most or least repeated characters == Degree of difficulty)");
                num_rand_created = int.Parse(Console.ReadLine());  //read number of the user define the difficulty of the question
                j = 0;
                str = "";
                while (j < num_rand_created)
                {
                    int rand_arry_created = rand.Next(0, ss.Length - 1);
                    str += ss[rand_arry_created];
                    j++;
                }
                string[] mostLeast = { "Most", "Least" }; // array for the type of questions
                int ML = rand.Next(0, mostLeast.Length);  //random var between Most and Least
                string MLquest = "";
                MLquest += mostLeast[ML];
                type_of_quest[quest_counter - 1] = MLquest;
                char[] arry = str.ToCharArray();  //convert the str to a char array
                min = 0; count = 0;
                Console.WriteLine("What is the " + MLquest + " repeated character in the following characters:? ");  //write the quest with the type of question
                Console.WriteLine(str);
                Console.WriteLine("To ignore type <<ignore>>");
                Console.Write("#: ");
                string answer = Console.ReadLine();  // read the answer
                Questions[quest_counter - 1] = str;
                type_of_quest[quest_counter - 1] = MLquest;
                if (answer == "ignore" || answer == "")
                    UserAnswer[quest_counter - 1] = ' ';
                else
                    UserAnswer[quest_counter - 1] = Convert.ToChar(answer);
                if (MLquest == "Most")  //if type of question most 
                {
                    lent = str.Length;
                    for (int i = 0; i < 255; i++)  //for loop to give (0) initial value for the xchar array
                    {
                        xchar[i] = 0;
                    }
                    k = 0;
                    while (k < lent)
                    {
                        ascii = (int)str[k];
                        xchar[ascii] += 1;
                        k += 1;
                    }
                    max = min = 0;
                    for (k = 0; k < 255; k++)//for loop to find the most repeated character
                    {
                        if (k != 32)
                        {
                            if (xchar[k] > xchar[max])
                                max = k;
                        }
                    }
                    CorrectAnswers[quest_counter - 1] = (char)max;
                    if (UserAnswer[quest_counter - 1] == CorrectAnswers[quest_counter - 1])  //right answers counter + 1
                    {
                        right_counter++;
                    }
                    else  //  wrong answers counter + 1
                    {
                        false_counter++;
                    }
                }
                nchar = str[0];
                if (MLquest == "Least")  //if LEAST
                {
                    /*
                     for loop within another for loop to compater the appearance of the character and return the least repeated one
                     */
                    for (int i = 0; i < num_rand_created; i++)  
                    {
                        for (int k = 0; k < num_rand_created; k++)
                            if (str[i] == str[k])
                                compare++;
                        if (second_compare > compare)
                        {
                            second_compare = compare;
                            nchar = str[i];
                        }
                        else if (second_compare == compare || second_compare == 0 && compare == 1)
                        {
                            if ((int)nchar < (int)str[i])
                                nchar = str[i];
                        }
                    }
                    CorrectAnswers[quest_counter - 1] = nchar;
                    if (UserAnswer[quest_counter - 1] == CorrectAnswers[quest_counter - 1])
                    {
                        right_counter++;
                    }
                    else
                    {
                        false_counter++;
                    }
                }
            }
            string DisplayPanel = "";
            while (DisplayPanel != "exit") // while loop that can control the panel
            {
                Console.WriteLine("To get the number of Right answers, type 1");
                Console.WriteLine("To get the number of Wrong answers, type 2");
                Console.WriteLine("To View all the questions with correct and answered response ,type 3");
                Console.WriteLine("To exit, type exit");
                DisplayPanel = Console.ReadLine();
                Console.WriteLine();
                switch (DisplayPanel) // switch case statement to display the results
                {
                    case "1": Console.WriteLine("The number of right answers is : " + right_counter + "\n"); break;
                    case "2": Console.WriteLine("The number of wrong answers is : " + false_counter + "\n"); break;
                    case "3": Display_Result(Questions, type_of_quest, UserAnswer, CorrectAnswers); break;
                    case "exit": DisplayPanel = "exit"; break;
                }
            }

        }
        public static void Display_Result(string[] x, string[] y, char[] z, char[] w)
        {
            Console.WriteLine("Question\tType\t   User answers\t   Correct asnwers");
            Console.WriteLine("======================================================");
            for (int f = 0; f <= x.Length - 1; f++)
                Console.WriteLine(x[f] + "\t  \t" + y[f] + "\t  \t" + z[f] + "\t  \t" + w[f]);
        }
    }
}
