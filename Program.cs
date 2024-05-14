//Andrei Fridrikhsen 
using System;
using System.Diagnostics.Metrics;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;

class QuizGame 
{
    static int scores = 0;// player scores, +5 points each question
    static string? input = "";
    static int lives = 2;
    static bool correct; // checks if the answer is correct
    static List<int> incorrectAnswers = new List<int>();
    //int counter = 0;
    static int questionNumber = 0;// keeps track of the number of questions
    
    static string[] validInputs = {"start","yes", "no", "a", "b", "c", "exit"};
    static void Main (string[] args)
    {
     do 
     {
       ResetGame();
       HandleStart();
       showQuestions(); 
     }
     while(input!=validInputs[6]);// keep playing while input is not exit
       // display a message when the game ends 
       Console.WriteLine("Have a great day!");

    }

    // resets game settings for a new session
    static void ResetGame()
    {
       scores = 0;
       lives = 2;
       incorrectAnswers.Clear();
       questionNumber = 0;
       input = ""; 
    }
    

    static void HandleStart ()
    {
        do 
        {
        Console.WriteLine("Type 'start' to start the game or 'exit' to quit");
        input = Console.ReadLine();
        if (input!=null)// if there's input convert to lowercase
        {
            input = input.ToLower();
        }
        }
        while(input!=validInputs[0] && input!=validInputs[6]);// while input isnt start print the message and get input

        
    }
  
    static void showQuestions() 
    {
        if(input!=validInputs[6])// if the user didn't type 'exit'
        {
             foreach (var question in  Cards.questions)
             {      
                    Console.WriteLine(question.Key);// ask question
                    questionNumber++;
                    input = Console.ReadLine();
                    if (input!= null)// if user gives input convert it to lower 
                    {
                        input = input.ToLower();
                    }
                    if (input!= question.Value.ToLower())// check if the answer is incorrect
                    {   
                        lives--;// if the answer is incorrect lose 1 life 
                        if(lives == 1)
                        {
                        Console.WriteLine("Incorrect. You have " +lives + " life left");
                        }else
                        {
                        Console.WriteLine("Incorrect. You have " +lives + " lives left");
                        }
                        correct = false;
                        // keep track of incorrect questions
                        
                        incorrectAnswers.Add(questionNumber); 
                        
                        
                       

                    }else  
                    {
                        Console.WriteLine("Correct");
                        correct = true;
                        // add +5 scores
                        scores+=5;
                       
                        
                    }
                    correct = false; // set the value to false for the next question
                   // if no more lives are left , the game's over, display the score
                    if (lives==0)
                        {
                          break;// breaks the for each loop that shows the questions
                        }
                   
                     
             }
             // game is over 
              Console.WriteLine("The game is over. Your score is " + Convert.ToString(scores));
             // ask if the user wishes to see the explanations
             if(incorrectAnswers.Count>0)
             {
             do
             {  
                Console.WriteLine("Do you wish to see explanations for " + incorrectAnswers.Count + " incorrrect answers. Type 'yes' or 'no'");
                input = Console.ReadLine();
                if (input!=null)// if there's input convert to lowercase
                   {
                    input = input.ToLower();
                   }
                
             }
             while (input!=validInputs[1] && input!=validInputs[2]); // yes or no
             if (input == validInputs[1])// if user choses yes
             {
                        for (int i = 0; i<incorrectAnswers.Count; i++)// shows explanations for all the incorrect answers
                        {
                            Console.WriteLine(Convert.ToString(i+1) + ".) " + Cards.explanations[incorrectAnswers[i]-1]);// subtract 1 since question number starts at 1
                        }
             }
             }else// if no incorrect answers and counter  is 0
             {
                  Console.WriteLine("Congratulations!!! You've answered all the questions correctly!!!");   
             }

    }
    }
}

class Cards
{
   public static Dictionary<string, string> questions = new Dictionary<string, string>// Dictionary that stores questions, answers and explanations 
   {
        {"What is the default access modifier for a class member if no access modifier is specified? A. public   B. private","B"},//1
        {"Which of the following is a nullable type in C#? A. int? B. int", "A"},//2
        {"Which of these is a correct way to declare an array in C#? A. int[] array = new int[5]; B. int array = new int[5]", "A"},//3
        {"What is Polymorphism in C#? A. The process of inheriting from multiple base classes B. the ability of a program to store different types of objects in the same array.", "A"},//4
        {"In C#, what keyword is used to override a base class method? A. virtual B. override", "B"},//5
        {"What is the purpose of a try-catch block in C#? A. To loop through a collection of items  B. To handle exceptions and errors during runtime", "B"},//6
        {"What is the purpose of an if statement in C#? A. To conditionally execute a block of code B. To loop through a collection of items", "A"},//7
        {"What is the difference between an array and a List in C#? A. Arrays can have methods, but Lists cannot B. Arrays are always of a fixed size, Lists are dynamically sized. C. Arrays can store multiple data types, but Lists cannot", "B"}, //8
        {"What is the default value of a boolean data type in C# when it is declared but not initialized? A. null B. true C. false", "C"},//9
        {"What is the purpose of namespace in c#? A. To organize classess and prevent name collisions.  B. To provide a collection of methods that can be used globally.","A"}//10
      

        
   };
   public static string[] explanations = 
    {
    "In c# the default access modifier is private which means that the member cannot be accessed outside of this class",// 1 question explanation
    "nullable type in c# is declared by adding a question mark after the value type",//2 
    "An array is declared by specifying the type of its elements, followed by square brackets, then initializing it with a new keyword and specifying the size of the array",//3
    "Polymorphism in C# is a concept that allows methods to have the same name but behave differently based on the objects they are acting upon. This typically involves overriding methods in derived classes",//4
    "The 'override' keyword is used in a derived class to modify or extend the behavior of an inherited method from the base class. This is crucial for implementing polymorphism.",//5
    "A try-catch block is used to handle exceptions, which are runtime errors that may occur during the execution of the program. The try block contains the code that might throw an exception, while the catch block contains code that handles the exception",//6
    "The if-else statement in C# is used to conditionally execute a block of code. It evaluates a boolean expression: if the expression is true, the if block is executed; otherwise, the else block (if present) is executed. This allows the programmer to control the flow of the program based on conditions",//7
    "In c#, lists unlike arrays that have fixed size are dynamic and can grow and shrink in size if needed",//8
    "In c#, the default value of an uninitialized boolean is false",//9
    "In c#, namespaces prevent name conflicts when, for example, 2 classes have the same name"//10


   };
}
